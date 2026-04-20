using Drkb.UniversalBot.Domain.Entity;

namespace Drkb.UniversalBot.Application.Interfaces.VkIntegration;

public interface IVkKeyboardFactory
{
    string GetVkMainKeyboard(List<Category> categories);
    string GetVkKeyboard(List<Category> categories);
}