using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using Drkb.UniversalBot.Domain.Entity;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.CreateCategory;

public class CreateCategoryHandler: IRequestHandler<CreateCategoryCommand, Result>
{
    private readonly ICreateCategoryDataProvider _createCategoryDataProvider;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryHandler(ICreateCategoryDataProvider createCategoryDataProvider, IUnitOfWork unitOfWork)
    {
        _createCategoryDataProvider = createCategoryDataProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var parentCategory = request.ParentCategoryId is null ? null : await _createCategoryDataProvider.GetByIdAsync(request.ParentCategoryId.Value, cancellationToken);

        var category = new Category()
        {
            Title = request.Title,
            ParentCategoryId = parentCategory?.Id
        };
        
        await _createCategoryDataProvider.AddAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}