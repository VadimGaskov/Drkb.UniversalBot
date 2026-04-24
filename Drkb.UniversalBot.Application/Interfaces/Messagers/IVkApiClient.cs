using Drkb.UniversalBot.Application.Interfaces.VkIntegration.ResponesDtos;

namespace Drkb.UniversalBot.Application.Interfaces.Messagers;

public interface IVkApiClient
{
    Task<VkSendMessageResponse> SendMessageAsync(
        long peerId,
        string message,
        string? keyboard,
        string? files,
        CancellationToken cancellationToken = default);

    Task<string> GetDocumentUploadUrlAsync(long peerId, CancellationToken cancellationToken = default);
    Task<string> UploadDocumentAsync(string uploadUrl, string fileUrl, CancellationToken cancellationToken = default);
    Task<VkDocumentInfo> SaveDocumentAsync(string fileToken, string? title, CancellationToken cancellationToken = default);
    Task AnswerMessageEventAsync(string eventId, long userId, long peerId,
        CancellationToken cancellationToken = default);
}