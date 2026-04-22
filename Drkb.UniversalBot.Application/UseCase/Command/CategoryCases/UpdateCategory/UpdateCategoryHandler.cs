using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using Drkb.UniversalBot.Contracts.Category;
using MassTransit;
using MediatR;
using MessageBroker.Abstractions.Interfaces.Publisher;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.UpdateCategory;

public class UpdateCategoryHandler: IRequestHandler<UpdateCategoryCommand, Result>
{
    private readonly IUpdateCategoryPort _updateCategoryPort;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IPublishEndpoint _publishEndpoint;
    
    public UpdateCategoryHandler(IUpdateCategoryPort updateCategoryPort, IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
    {
        _updateCategoryPort = updateCategoryPort;
        _unitOfWork = unitOfWork;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _updateCategoryPort.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (category is null)
            return Result.NotFound("Category doesn't exist");

        category.Title = request.NameCategory;
        category.Value = request.Value;
        if (request.ParentCategoryId is null)
            category.ParentCategoryId = null;
        else
        {
            category.ParentCategory = await _updateCategoryPort.GetByIdAsync(request.ParentCategoryId.Value, cancellationToken: cancellationToken);
        }
        _updateCategoryPort.Update(category);
        
        await _publishEndpoint.Publish(new CategoryUpdatedEvent(category.Id, request.UploadContextId,
            request.FileIds), cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}