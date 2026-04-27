namespace Drkb.UniversalBot.Application.Interfaces.Messagers;

public interface IMaxClient
{
    Task SendMessageAsync(
        string userId, 
        string? message, 
        string? keyboard,
        string? files, CancellationToken cancellationToken);

    Task<string> GetFileUploadUrlAsync(CancellationToken cancellationToken);
    
    Task<string> UploadFiles(string fileUrl, CancellationToken cancellationToken);
}