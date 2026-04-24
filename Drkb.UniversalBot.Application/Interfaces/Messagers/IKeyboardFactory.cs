using Drkb.UniversalBot.Domain.Entity;

namespace Drkb.UniversalBot.Application.Interfaces.Messagers;

public interface IKeyboardFactory
{
    string GetMainKeyboard(List<Category> categories);
    string GetKeyboard(List<Category> categories);
}