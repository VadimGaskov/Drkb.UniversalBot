using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using Drkb.UniversalBot.Contracts.Category;
using MediatR;
using MessageBroker.Abstractions.Interfaces.Publisher;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.UpdateCategory;

public class UpdateCategoryHandler: IRequestHandler<UpdateCategoryCommand, Result>
{
    private readonly IUpdateCategoryPort _updateCategoryPort;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMessageBrokerPublisher _messageBrokerPublisher;
    public UpdateCategoryHandler(IUpdateCategoryPort updateCategoryPort, IUnitOfWork unitOfWork, IMessageBrokerPublisher messageBrokerPublisher)
    {
        _updateCategoryPort = updateCategoryPort;
        _unitOfWork = unitOfWork;
        _messageBrokerPublisher = messageBrokerPublisher;
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
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        await _messageBrokerPublisher.PublishAsync(new CategoryUpdatedEvent(category.Id, request.UploadContextId,
            request.FileIds), cancellationToken);
        return Result.Success();
    }
}