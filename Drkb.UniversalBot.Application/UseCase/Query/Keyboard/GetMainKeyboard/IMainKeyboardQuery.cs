using Drkb.UniversalBot.Application.Interfaces.QueryObjects;
using Drkb.UniversalBot.Domain.Entity;

namespace Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetMainKeyboard;

public interface IMainKeyboardQuery: IQueryObject<GetMainKeyboardQuery, List<Category>>
{
    
}