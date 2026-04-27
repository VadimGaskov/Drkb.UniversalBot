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
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.CreateCategory;

public class CreateCategoryHandler: IRequestHandler<CreateCategoryCommand, Result>
{
    private readonly ICreateCategoryPort _categoryPort;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<CreateCategoryHandler> _logger;
    
    
    public CreateCategoryHandler(
        ICreateCategoryPort categoryPort,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint,
        ILogger<CreateCategoryHandler> logger)
    {
        _categoryPort = categoryPort;
        _unitOfWork = unitOfWork;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Запрошено создание категории. Name: {Name}, ParentCategoryId: {ParentCategoryId}, FilesCount: {FilesCount}",
            request.NameCategory,
            request.ParentCategoryId,
            request.FileIds.Count);
        
        var parentCategory = await _categoryPort.GetCategoryByIdAsync(request.ParentCategoryId, cancellationToken);
        if (request.ParentCategoryId is not null && parentCategory is null)
        {
            _logger.LogError(
                "Создание категории отклонено: родительская категория не найдена. ParentCategoryId: {ParentCategoryId}, Name: {Name}",
                request.ParentCategoryId,
                request.NameCategory);
            return Result.NotFound("Parent category doesn't exist");
        }

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
        {
            await _publishEndpoint.Publish(new CategoryCreatedEvent(category.Id, request.UploadContextId,
                request.FileIds), cancellationToken);
            _logger.LogInformation(
                "Отправлено событие привязки файлов к категории. CategoryId: {CategoryId}, UploadContextId: {UploadContextId}, FilesCount: {FilesCount}",
                category.Id,
                request.UploadContextId,
                request.FileIds.Count);
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Категория успешно создана. CategoryId: {CategoryId}, Name: {Name}", category.Id, category.Title);
        
        return Result.Success();
    }
}