using Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;

namespace Drkb.UniversalBot.Application.Interfaces;

public interface IFileStorage
{
    Task<StoredFileResult> SaveAsync(
        AppFile file,
        CancellationToken cancellationToken = default);
}