using System.Text;
using System.Text.Json;
using Drkb.UniversalBot.Application.UseCase.Command.Max.InitWebhook;
using Drkb.UniversalBot.Infrastructure.Option;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Drkb.UniversalBot.Infrastructure.Messengers.Max;

public class InitWebhookService: IInitWebhookService
{
    private readonly HttpClient _httpClient;
    private readonly MaxOption _maxOption;

    public InitWebhookService(HttpClient httpClient, IOptions<MaxOption> maxOption)
    {
        _httpClient = httpClient;
        _maxOption = maxOption.Value;
    }

    public async Task ProcessAsync()
    {
        var payload = new
        {
            url = "https://a9c2a176-caf4-4275-b6c1-4635f726fda7.tunnel4.com/api/max/webhook",
            update_types = _maxOption.UpdateTypes,
            secret = _maxOption.Secret,
        };
        
        var json = JsonSerializer.Serialize(payload);
        
        var request = new HttpRequestMessage(HttpMethod.Post, _maxOption.SubscriptionUrl);
        request.Headers.Add("Authorization", $"{_maxOption.AccessToken}");
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.SendAsync(request);

        var responseBody = await response.Content.ReadAsStringAsync();
        
    }
}