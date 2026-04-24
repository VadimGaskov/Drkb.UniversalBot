using Drkb.UniversalBot.Application.Dtos;
using Drkb.UniversalBot.Application.Dtos.Messenger;
using Drkb.UniversalBot.Application.Interfaces.Messagers;

namespace Drkb.UniversalBot.Infrastructure.Messengers.Vk;

public class VkMessageService: IBotService
{
    private readonly IVkApiClient _vkApiClient;

    public VkMessageService(IVkApiClient vkApiClient)
    {
        _vkApiClient = vkApiClient;
    }

    public async Task SendAnswerMessageEventAsync(AnswerCallbackContext callbackContext,
        CancellationToken cancellationToken = default)
    {
        if (callbackContext.EventId is null)
            throw new InvalidOperationException(nameof(callbackContext));
        
        
        await _vkApiClient.AnswerMessageEventAsync(callbackContext.EventId, callbackContext.SenderId, callbackContext.ConversationId, cancellationToken);
    }
    
    public async Task SendCompositeAsync(SendMessageContext message, CancellationToken cancellationToken = default)
    { 
        var text = string.Empty;
        if (message.HasText)
            text = $"{message.Text}";
        else if (message.HasKeyboard && !message.HasFiles)
            text = "Клавиатура:";
        else if (!message.HasKeyboard && !message.HasFiles)
            text = "Прошу прощения не могу обработать этот запрос, свяжитесь с колл-центром";
        
        var attachmentString = message.HasFiles
            ? await UploadFilesAsync(message.ConversationId, message.FilesUrl, cancellationToken)
            : null;

        await _vkApiClient.SendMessageAsync(
            message.ConversationId,
            text,
            message.HasKeyboard ? message.Keyboard : null,
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