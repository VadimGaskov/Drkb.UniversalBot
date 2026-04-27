using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using Drkb.UniversalBot.Contracts.Category;
using MassTransit;
using MediatR;
using MessageBroker.Abstractions.Interfaces.Publisher;
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.UpdateCategory;

public class UpdateCategoryHandler: IRequestHandler<UpdateCategoryCommand, Result>
{
    private readonly IUpdateCategoryPort _updateCategoryPort;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<UpdateCategoryHandler> _logger;
    
    public UpdateCategoryHandler(
        IUpdateCategoryPort updateCategoryPort,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint,
        ILogger<UpdateCategoryHandler> logger)
    {
        _updateCategoryPort = updateCategoryPort;
        _unitOfWork = unitOfWork;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Запрошено обновление категории. CategoryId: {CategoryId}, Name: {Name}, ParentCategoryId: {ParentCategoryId}",
            request.Id,
            request.NameCategory,
            request.ParentCategoryId);

        var category = await _updateCategoryPort.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (category is null)
        {
            _logger.LogError("Обновление категории отклонено: категория не найдена. CategoryId: {CategoryId}", request.Id);
            return Result.NotFound("Category doesn't exist");
        }

        category.Title = request.NameCategory;
        category.Value = request.Value;
        if (request.ParentCategoryId is null)
            category.ParentCategoryId = null;
        else
        {
            category.ParentCategory = await _updateCategoryPort.GetByIdAsync(request.ParentCategoryId.Value, cancellationToken: cancellationToken);
            if (category.ParentCategory is null)
            {
                _logger.LogError(
                    "Обновление категории отклонено: родительская категория не найдена. CategoryId: {CategoryId}, ParentCategoryId: {ParentCategoryId}",
                    request.Id,
                    request.ParentCategoryId);
                return Result.NotFound("Parent category doesn't exist");
            }
        }
        _updateCategoryPort.Update(category);
        
        await _publishEndpoint.Publish(new CategoryUpdatedEvent(category.Id, request.UploadContextId,
            request.FileIds), cancellationToken);
        _logger.LogInformation(
            "Отправлено событие обновления категории. CategoryId: {CategoryId}, UploadContextId: {UploadContextId}, FilesCount: {FilesCount}",
            category.Id,
            request.UploadContextId,
            request.FileIds.Count);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Категория успешно обновлена. CategoryId: {CategoryId}", category.Id);
        
        return Result.Success();
    }
}