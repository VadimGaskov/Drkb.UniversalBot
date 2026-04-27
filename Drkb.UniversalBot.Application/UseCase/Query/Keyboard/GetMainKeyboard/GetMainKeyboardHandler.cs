using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetMainKeyboard;

public class GetMainKeyboardHandler: IRequestHandler<GetMainKeyboardQuery, string>
{
    private readonly IMainKeyboardQuery _mainKeyboardQuery;
    private readonly IVkKeyboardFactory _vkKeyboardFactory;
    private readonly ILogger<GetMainKeyboardHandler> _logger;

    public GetMainKeyboardHandler(IMainKeyboardQuery mainKeyboardQuery, IVkKeyboardFactory vkKeyboardFactory, ILogger<GetMainKeyboardHandler> logger)
    {
        _mainKeyboardQuery = mainKeyboardQuery;
        _vkKeyboardFactory = vkKeyboardFactory;
        _logger = logger;
    }

    public async Task<string> Handle(GetMainKeyboardQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрошено формирование главной клавиатуры");
        var result = await _mainKeyboardQuery.ExecuteAsync(request, cancellationToken);
        var keyboard = _vkKeyboardFactory.GetVkMainKeyboard(result);
        _logger.LogInformation("Главная клавиатура успешно сформирована. КоличествоКатегорий: {CategoriesCount}", result.Count);
        return keyboard;
    }
}