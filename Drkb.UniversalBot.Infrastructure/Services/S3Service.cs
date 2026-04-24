using System.Net.Http.Json;
using Drkb.UniversalBot.Application.Dtos;
using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Infrastructure.Option;
using Microsoft.Extensions.Options;

namespace Drkb.UniversalBot.Infrastructure.Services;

public class S3Service: IS3Service
{
    private readonly HttpClient _httpClient;
    private readonly FileSaverOptions _options;

    public S3Service(HttpClient httpClient, IOptions<FileSaverOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<List<string>> GetAllUrls(Guid relatedId)
    {
        var response = await _httpClient.GetAsync($"{_options.GetAllFilesUrl}?relatedId={relatedId}");
        response.EnsureSuccessStatusCode();
        var responseS3 = await response.Content.ReadFromJsonAsync<List<GetAllByFilesUrlDto>>();
        
        if(responseS3 == null)
            return [];
        
        return responseS3.Select(x => x.Url).ToList();
    }
}