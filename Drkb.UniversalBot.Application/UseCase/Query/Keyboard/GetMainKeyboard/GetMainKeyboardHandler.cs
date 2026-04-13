using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetMainKeyboard;

public class GetMainKeyboardHandler: IRequestHandler<GetMainKeyboardQuery, string>
{
    private readonly IMainKeyboardQuery _mainKeyboardQuery;
    private readonly IVkKeyboardFactory _vkKeyboardFactory;

    public GetMainKeyboardHandler(IMainKeyboardQuery mainKeyboardQuery, IVkKeyboardFactory vkKeyboardFactory)
    {
        _mainKeyboardQuery = mainKeyboardQuery;
        _vkKeyboardFactory = vkKeyboardFactory;
    }

    public async Task<string> Handle(GetMainKeyboardQuery request, CancellationToken cancellationToken)
    {
        var result = await _mainKeyboardQuery.ExecuteAsync(request, cancellationToken);
        return _vkKeyboardFactory.GetVkKeyboard(result);
    }
}