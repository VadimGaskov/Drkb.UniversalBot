using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.DeleteCategory;

public class DeleteCategoryHandler: IRequestHandler<DeleteCategoryCommand, Result>
{
    private readonly IDeleteCategoryPort _deleteCategoryPort;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteCategoryHandler> _logger;

    public DeleteCategoryHandler(IDeleteCategoryPort deleteCategoryPort, IUnitOfWork unitOfWork, ILogger<DeleteCategoryHandler> logger)
    {
        _deleteCategoryPort = deleteCategoryPort;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрошено удаление категории. CategoryId: {CategoryId}", request.CategoryId);

        if (!await _deleteCategoryPort.CheckExistsAsync(request.CategoryId, cancellationToken))
        {
            _logger.LogError("Удаление категории отклонено: категория не найдена. CategoryId: {CategoryId}", request.CategoryId);
            return Result.NotFound("Category does not exist");
        }

        await _deleteCategoryPort.DeleteAsync(request.CategoryId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Категория успешно удалена. CategoryId: {CategoryId}", request.CategoryId);
        return Result.Success();
    }
}