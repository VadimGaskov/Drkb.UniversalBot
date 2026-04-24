using System.Net.Http.Json;
using System.Text.Json;
using Drkb.UniversalBot.Application.Interfaces.Messagers;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration.ResponesDtos;
using Drkb.UniversalBot.Infrastructure.Option;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace Drkb.UniversalBot.Infrastructure.Messengers.Vk;

public class VkApiClient: IVkApiClient
{
    private readonly HttpClient _httpClient;
    private readonly VkOptions _options;
    private readonly ILogger<VkApiClient> _logger;

    public VkApiClient(
        HttpClient httpClient,
        IOptions<VkOptions> options,
        ILogger<VkApiClient> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<VkSendMessageResponse> SendMessageAsync(
        long peerId,
        string? message,
        string? keyboard,
        string? files,
        CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?>
        {
            ["peer_id"] = peerId.ToString(),
            ["message"] = message,
            ["random_id"] = Random.Shared.Next(1, int.MaxValue).ToString(),
            ["access_token"] = _options.AccessToken,
            ["keyboard"] = keyboard,
            ["attachment"] = files,
            ["v"] = _options.ApiVersion
        };
        
        const string baseUrl = "https://api.vk.com/method/messages.send";
        var url = QueryHelpers.AddQueryString(baseUrl, query!);

        using var response = await _httpClient.PostAsync(url, null, cancellationToken);
        response.EnsureSuccessStatusCode();

        var payload = await response.Content.ReadFromJsonAsync<VkApiResponse<int>>(
            cancellationToken: cancellationToken);

        if (payload is null)
            throw new InvalidOperationException("VK returned empty response.");

        if (payload.Error is not null)
        {
            _logger.LogError("VK API error {Code}: {Message}",
                payload.Error.ErrorCode,
                payload.Error.ErrorMessage);

            throw new InvalidOperationException(
                $"VK API error {payload.Error.ErrorCode}: {payload.Error.ErrorMessage}");
        }

        return new VkSendMessageResponse() {MessageId = payload.Response}
               ?? throw new InvalidOperationException("VK response does not contain 'response'.");
    }
    
    public async Task<string> GetDocumentUploadUrlAsync(
        long peerId,
        CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?>
        {
            ["peer_id"] = peerId.ToString(),
            ["access_token"] = _options.AccessToken,
            ["v"] = "5.199"
        };

        const string methodUrl = "https://api.vk.com/method/docs.getMessagesUploadServer";
        var url = QueryHelpers.AddQueryString(methodUrl, query!);

        using var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        var result = JsonSerializer.Deserialize<VkUploadServerResponse>(json);

        if (result?.Response?.UploadUrl is null)
            throw new InvalidOperationException($"VK did not return upload_url. Response: {json}");

        return result.Response.UploadUrl;
    }
    
    public async Task<string> UploadDocumentAsync(
        string uploadUrl,
        string fileUrl,
        CancellationToken cancellationToken = default)
    {
        await using var fileStream = await _httpClient.GetStreamAsync(fileUrl, cancellationToken);

        using var form = new MultipartFormDataContent();
        using var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        
        var fileName = Path.GetFileName(new Uri(fileUrl).AbsolutePath);
        if (string.IsNullOrWhiteSpace(fileName))
            fileName = "file";
        
        form.Add(fileContent, "file", fileName);

        using var response = await _httpClient.PostAsync(uploadUrl, form, cancellationToken);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        var result = JsonSerializer.Deserialize<VkUploadedDocumentResponse>(json);

        if (string.IsNullOrWhiteSpace(result?.File))
            throw new InvalidOperationException($"VK upload server did not return file token. Response: {json}");

        return result.File;
    }
    
    public async Task<VkDocumentInfo> SaveDocumentAsync(
        string fileToken,
        string? title = null,
        CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?>
        {
            ["file"] = fileToken,
            ["title"] = title,
            ["access_token"] = _options.AccessToken,
            ["v"] = "5.199"
        };

        const string methodUrl = "https://api.vk.com/method/docs.save";
        var url = QueryHelpers.AddQueryString(methodUrl, query!);

        using var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        var result = JsonSerializer.Deserialize<VkDocsSaveResponse>(json);

        var doc = result?.Response.Doc;
        if (doc is null)
            throw new InvalidOperationException($"VK docs.save returned empty response. Response: {json}");

        return doc;
    }
    
    public async Task AnswerMessageEventAsync(
        string eventId,
        long userId,
        long peerId,
        CancellationToken cancellationToken = default)
    {
        var values = new Dictionary<string, string>
        {
            ["event_id"] = eventId,
            ["user_id"] = userId.ToString(),
            ["peer_id"] = peerId.ToString(),
            ["access_token"] = _options.AccessToken,
            ["v"] = _options.ApiVersion
        };

        using var content = new FormUrlEncodedContent(values);

        using var response = await _httpClient.PostAsync(
            "https://api.vk.com/method/messages.sendMessageEventAnswer",
            content,
            cancellationToken);

        var responseText = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(
                $"VK API returned HTTP {(int)response.StatusCode}: {responseText}");

        if (responseText.Contains("\"error\"", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException($"VK API error: {responseText}");
    }
}