using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;
using Drkb.UniversalBot.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace Drkb.UniversalBot.Infrastructure.Services;

public class LocalFileStorage: IFileStorage
{
    private readonly string _rootPath;
    
    public LocalFileStorage(IOptions<LocalFileStorageOptions> options)
    {
        _rootPath = options.Value.RootPath;
    }
    
    public async Task<StoredFileResult> SaveAsync(AppFile file, CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_rootPath);

        var extension = Path.GetExtension(file.FileName);
        var storedFileName = $"{Guid.NewGuid()}{extension}";

        var subFolder = Path.Combine(DateTime.UtcNow.ToString("yyyy/MM"));
        var folderPath = Path.Combine(_rootPath, subFolder);

        Directory.CreateDirectory(folderPath);

        var fullPath = Path.Combine(folderPath, storedFileName);

        await File.WriteAllBytesAsync(fullPath, file.Content, cancellationToken);

        return new StoredFileResult
        {
            FileName = file.FileName,
            RelativePath = Path.Combine(folderPath, storedFileName).Replace("\\", "/"),
            ContentType = file.ContentType,
            Length = file.Length
        };
    }
}