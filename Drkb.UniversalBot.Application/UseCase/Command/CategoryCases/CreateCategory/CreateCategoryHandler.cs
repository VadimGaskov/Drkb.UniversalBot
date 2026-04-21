using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;
using Drkb.UniversalBot.Contracts.Category;
using Drkb.UniversalBot.Domain.Entity;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using MassTransit;
using MediatR;
using MessageBroker.Abstractions.Interfaces.Publisher;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.CreateCategory;

public class CreateCategoryHandler: IRequestHandler<CreateCategoryCommand, Result>
{
    private readonly ICreateCategoryPort _categoryPort;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublishEndpoint _publishEndpoint;
    
    private readonly IMessageBrokerPublisher _messageBrokerPublisher;
    
    public CreateCategoryHandler(ICreateCategoryPort categoryPort, IUnitOfWork unitOfWork, IMessageBrokerPublisher messageBrokerPublisher, IPublishEndpoint publishEndpoint)
    {
        _categoryPort = categoryPort;
        _unitOfWork = unitOfWork;
        _messageBrokerPublisher = messageBrokerPublisher;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        
        var parentCategory = await _categoryPort.GetCategoryByIdAsync(request.ParentCategoryId, cancellationToken);
        var seq = await _categoryPort.GetLastSeq(cancellationToken);
        var category = new Category()
        {
            Title = request.NameCategory,
            ParentCategory = parentCategory,
            Value = request.Value,
            Seq = seq + 1,
        };
        await _categoryPort.AddCategoryAsync(category, cancellationToken);
        
        if (request.FileIds.Count != 0)
            await _publishEndpoint.Publish(new CategoryCreatedEvent(category.Id, request.UploadContextId,
                request.FileIds), cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}