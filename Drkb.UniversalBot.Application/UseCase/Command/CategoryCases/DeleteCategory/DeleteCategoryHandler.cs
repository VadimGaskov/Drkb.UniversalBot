using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.DeleteCategory;

public class DeleteCategoryHandler: IRequestHandler<DeleteCategoryCommand, Result>
{
    private readonly IDeleteCategoryPort _deleteCategoryPort;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryHandler(IDeleteCategoryPort deleteCategoryPort, IUnitOfWork unitOfWork)
    {
        _deleteCategoryPort = deleteCategoryPort;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        if (!await _deleteCategoryPort.CheckExistsAsync(request.CategoryId, cancellationToken))
            return Result.NotFound("Category does not exist");

        await _deleteCategoryPort.DeleteAsync(request.CategoryId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}