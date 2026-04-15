using Drkb.UniversalBot.Application.Interfaces.VkIntegration.CallbackDtos;

namespace Drkb.UniversalBot.Application.Interfaces.VkIntegration;

public interface IVkMessageEventProcessor
{
    Task ProcessAsync(VkMessageEventObject? messageEvent, CancellationToken ct);
}