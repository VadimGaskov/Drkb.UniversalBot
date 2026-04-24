namespace Drkb.UniversalBot.Application.Interfaces;

public interface IS3Service
{
    Task<List<string>> GetAllUrls(Guid relatedId);
}