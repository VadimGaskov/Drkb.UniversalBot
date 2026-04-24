using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Interfaces.Ports;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.UpdateSeq;

public class ReorderCategoryHandler: IRequestHandler<ReorderCategoryCommand, Result>
{
    private readonly IReorderCategoryPort _reorderCategoryPort;
    private readonly IUnitOfWork _unitOfWork;

    public ReorderCategoryHandler(IReorderCategoryPort reorderCategoryPort, IUnitOfWork unitOfWork)
    {
        _reorderCategoryPort = reorderCategoryPort;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ReorderCategoryCommand request, CancellationToken cancellationToken)
    {
        var duplicateIds = request.ReorderCategories
            .GroupBy(x => x.Id)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateIds.Count > 0)
            return Result.BadRequest("В запросе есть повторяющиеся категории");
        
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
        return Result.Success();
    }
}