using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.UpdateSeq;

public class ReorderCategoryHandler: IRequestHandler<ReorderCategoryCommand, Result>
{
    private readonly IReorderCategoryPort _reorderCategoryPort;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ReorderCategoryHandler> _logger;

    public ReorderCategoryHandler(IReorderCategoryPort reorderCategoryPort, IUnitOfWork unitOfWork, ILogger<ReorderCategoryHandler> logger)
    {
        _reorderCategoryPort = reorderCategoryPort;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> Handle(ReorderCategoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрошено изменение порядка категорий. КоличествоКатегорий: {CategoriesCount}",
            request.ReorderCategories.Count);

        var duplicateIds = request.ReorderCategories
            .GroupBy(x => x.Id)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateIds.Count > 0)
        {
            _logger.LogError(
                "Изменение порядка категорий отклонено: в запросе обнаружены повторяющиеся идентификаторы. ПовторяющиесяCategoryIds: {DuplicateCategoryIds}",
                duplicateIds);
            return Result.BadRequest("В запросе есть повторяющиеся категории");
        }
        
        var requestData = request.ReorderCategories
            .ToDictionary(r => r.Id, r => r.Order);

        var requestIds = request.ReorderCategories
            .Select(r => r.Id)
            .ToHashSet();
        
        var categories = await _reorderCategoryPort.GetCategoriesAsync(requestIds, cancellationToken);

        foreach (var category in categories)
        {
            category.Seq = requestData[category.Id];
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Порядок категорий успешно обновлен. ОбновленоКатегорий: {UpdatedCategoriesCount}", categories.Count);
        return Result.Success();
    }
}