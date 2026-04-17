using Drkb.UniversalBot.Application.Interfaces.S3;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration.ResponesDtos;

namespace Drkb.UniversalBot.Infrastructure.Services.VkIntegration;

public class VkMessageService: IVkMessageService
{
    private readonly IVkApiClient _vkApiClient;
    private readonly IS3Service _serviceS3;

    public VkMessageService(IVkApiClient vkApiClient, IS3Service serviceS3)
    {
        _vkApiClient = vkApiClient;
        _serviceS3 = serviceS3;
    }
    
    public Task SendTextAsync(long peerId, string text, CancellationToken cancellationToken = default)
        => SendCompositeAsync(peerId, new VkSendMessageRequest { Value = text }, cancellationToken);

    public Task SendKeyboardAsync(long peerId, string text, string keyboardPayload, CancellationToken cancellationToken = default)
        => SendCompositeAsync(peerId, new VkSendMessageRequest { Value = text, KeyboardPayload = keyboardPayload }, cancellationToken);
    
    public async Task SendAnswerMessageEventAsync(string eventId, long userId, long peerId, CancellationToken cancellationToken = default) 
        => await _vkApiClient.AnswerMessageEventAsync(eventId, userId, peerId, cancellationToken);
    
    public async Task SendCompositeAsync(long peerId, VkSendMessageRequest message, CancellationToken cancellationToken = default)
    {
        var text = string.Empty;
        if (message.HasValue && !string.IsNullOrWhiteSpace(text))
            text = $"{message.Name}:\n{message.Value}";
        else if (message.HasKeyboard && !message.HasFiles)
            text = "Клавиатура:";
        else if (!message.HasKeyboard && !message.HasFiles)
            text = "Прошу прощения не могу обработать этот запрос, свяжитесь с колл-центром";
        
        var attachmentString = message.HasFiles
            ? await UploadFilesAsync(peerId, message.FilesUrl, cancellationToken)
            : null;

        await _vkApiClient.SendMessageAsync(
            peerId,
            text,
            message.HasKeyboard ? message.KeyboardPayload : null,
            attachmentString,
            cancellationToken);
    }

    private async Task<string> UploadFilesAsync(long peerId, IReadOnlyList<string> filePaths, CancellationToken cancellationToken)
    {
        var attachments = new List<string>();

        foreach (var filePath in filePaths.Take(10))
        {
            var uploadServer = await _vkApiClient.GetDocumentUploadUrlAsync(peerId, cancellationToken);
            var uploadResponse = await _vkApiClient.UploadDocumentAsync(uploadServer, filePath, cancellationToken);
            var savedDoc = await _vkApiClient.SaveDocumentAsync(uploadResponse, "", cancellationToken);
            attachments.Add($"doc{savedDoc.OwnerId}_{savedDoc.Id}");
        }

        return string.Join(",", attachments);
    }

    
}