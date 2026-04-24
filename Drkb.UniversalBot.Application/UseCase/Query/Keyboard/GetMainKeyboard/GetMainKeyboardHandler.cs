using Drkb.UniversalBot.Application.Interfaces.Messagers;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetMainKeyboard;

public class GetMainKeyboardHandler: IRequestHandler<GetMainKeyboardQuery, string>
{
    private readonly IMainKeyboardQuery _mainKeyboardQuery;
    private readonly IKeyboardFactory _keyboardFactory;

    public GetMainKeyboardHandler(IMainKeyboardQuery mainKeyboardQuery, IKeyboardFactory keyboardFactory)
    {
        _mainKeyboardQuery = mainKeyboardQuery;
        _keyboardFactory = keyboardFactory;
    }

    public async Task<string> Handle(GetMainKeyboardQuery request, CancellationToken cancellationToken)
    {
        var result = await _mainKeyboardQuery.ExecuteAsync(request, cancellationToken);
        return _keyboardFactory.GetMainKeyboard(result);
    }
}