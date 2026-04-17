using Drkb.UniversalBot.Application.Interfaces.S3;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetDataCategoryById;

public class GetDataCategoryByIdHandler: IRequestHandler<GetDataCategoryByIdQuery, GetRequestDataDto?>
{
    private readonly IGetDataCategoryByIdQuery _getDataCategoryByIdQuery;
    private readonly IVkKeyboardFactory _vkKeyboardFactory;
    private readonly IS3Service _s3Service;

    public GetDataCategoryByIdHandler(IGetDataCategoryByIdQuery getDataCategoryByIdQuery, IVkKeyboardFactory vkKeyboardFactory, IS3Service s3Service)
    {
        _getDataCategoryByIdQuery = getDataCategoryByIdQuery;
        _vkKeyboardFactory = vkKeyboardFactory;
        _s3Service = s3Service;
    }

    public async Task<GetRequestDataDto?> Handle(GetDataCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _getDataCategoryByIdQuery.ExecuteAsync(request, cancellationToken);
        if (response is null)
            return null;
        
        var keyboards = _vkKeyboardFactory.GetVkKeyboardWithBack(response.Categories);
        var messageText = response.Text;
        var filesUrl = await _s3Service.GetAllUrls(response.CategoryId);
        
        return new GetRequestDataDto {Keyboard = keyboards, Name = messageText, Value = response.Value, FilesUrl = filesUrl};
    }
}