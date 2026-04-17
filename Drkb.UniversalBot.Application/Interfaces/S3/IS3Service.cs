using Drkb.UniversalBot.Application.Dtos;

namespace Drkb.UniversalBot.Application.Interfaces.S3;

public interface IS3Service
{
    Task<List<string>> GetAllUrls(Guid relatedId);
}