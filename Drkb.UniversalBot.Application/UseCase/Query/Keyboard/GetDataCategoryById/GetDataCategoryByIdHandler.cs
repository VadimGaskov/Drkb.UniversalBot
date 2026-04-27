using Drkb.UniversalBot.Application.Interfaces.S3;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetDataCategoryById;

public class GetDataCategoryByIdHandler: IRequestHandler<GetDataCategoryByIdQuery, GetRequestDataDto?>
{
    private readonly IGetDataCategoryByIdQuery _getDataCategoryByIdQuery;
    private readonly IVkKeyboardFactory _vkKeyboardFactory;
    private readonly IS3Service _s3Service;
    private readonly ILogger<GetDataCategoryByIdHandler> _logger;

    public GetDataCategoryByIdHandler(
        IGetDataCategoryByIdQuery getDataCategoryByIdQuery,
        IVkKeyboardFactory vkKeyboardFactory,
        IS3Service s3Service,
        ILogger<GetDataCategoryByIdHandler> logger)
    {
        _getDataCategoryByIdQuery = getDataCategoryByIdQuery;
        _vkKeyboardFactory = vkKeyboardFactory;
        _s3Service = s3Service;
        _logger = logger;
    }

    public async Task<GetRequestDataDto?> Handle(GetDataCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрошены данные категории для ответа пользователю. CategoryId: {CategoryId}", request.CategoryId);
        var response = await _getDataCategoryByIdQuery.ExecuteAsync(request, cancellationToken);
        if (response is null)
        {
            _logger.LogError("Данные категории не найдены. CategoryId: {CategoryId}", request.CategoryId);
            return null;
        }
        
        var keyboards = _vkKeyboardFactory.GetVkKeyboard(response.Categories);
        var categoryName = response.Name;
        var categoryValue = response.Value;
        var filesUrl = await _s3Service.GetAllUrls(response.CategoryId);
        _logger.LogInformation(
            "Данные категории успешно сформированы. CategoryId: {CategoryId}, Name: {CategoryName}, Вложений: {FilesCount}",
            response.CategoryId,
            categoryName,
            filesUrl.Count);
        
        return new GetRequestDataDto {Keyboard = keyboards, Name = categoryName, Value = categoryValue, FilesUrl = filesUrl};
    }
}