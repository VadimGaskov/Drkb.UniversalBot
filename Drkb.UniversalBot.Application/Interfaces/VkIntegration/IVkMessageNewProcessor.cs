using Drkb.UniversalBot.Application.Interfaces.VkIntegration.CallbackDtos;

namespace Drkb.UniversalBot.Application.Interfaces.VkIntegration;

public interface IVkMessageNewProcessor
{
    Task ProcessAsync(VkMessageNewObject? message, CancellationToken ct);
}