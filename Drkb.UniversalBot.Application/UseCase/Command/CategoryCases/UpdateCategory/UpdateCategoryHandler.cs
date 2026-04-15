using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.UpdateCategory;

public class UpdateCategoryHandler: IRequestHandler<UpdateCategoryCommand, Result>
{
    private readonly IUpdateCategoryDataProvider _updateCategoryDataProvider;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryHandler(IUpdateCategoryDataProvider updateCategoryDataProvider, IUnitOfWork unitOfWork)
    {
        _updateCategoryDataProvider = updateCategoryDataProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _updateCategoryDataProvider.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (category is null)
            return Result.NotFound("Category doesn't exist");

        category.Title = request.Name;
        if (request.ParentCategoryId is null)
            category.ParentCategory = null;
        else
        {
            category.ParentCategory = await _updateCategoryDataProvider.GetByIdAsync(request.ParentCategoryId.Value, cancellationToken: cancellationToken);
        }
        _updateCategoryDataProvider.Update(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}