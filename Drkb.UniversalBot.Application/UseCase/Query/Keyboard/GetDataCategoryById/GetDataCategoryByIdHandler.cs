using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.Interfaces.Messagers;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetDataCategoryById;

public class GetDataCategoryByIdHandler: IRequestHandler<GetDataCategoryByIdQuery, GetRequestDataDto?>
{
    private readonly IGetDataCategoryByIdQuery _getDataCategoryByIdQuery;
    private readonly IKeyboardFactory _keyboardFactory;
    private readonly IS3Service _s3Service;

    public GetDataCategoryByIdHandler(IGetDataCategoryByIdQuery getDataCategoryByIdQuery, IKeyboardFactory keyboardFactory, IS3Service s3Service)
    {
        _getDataCategoryByIdQuery = getDataCategoryByIdQuery;
        _keyboardFactory = keyboardFactory;
        _s3Service = s3Service;
    }

    public async Task<GetRequestDataDto?> Handle(GetDataCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _getDataCategoryByIdQuery.ExecuteAsync(request, cancellationToken);
        if (response is null)
            return null;
        
        var keyboards = _keyboardFactory.GetKeyboard(response.Categories);
        var categoryValue = response.Value;
        var filesUrl = await _s3Service.GetAllUrls(response.CategoryId);
        
        return new GetRequestDataDto {Keyboard = keyboards, Value = categoryValue, FilesUrl = filesUrl};
    }
}