using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;
using Drkb.UniversalBot.Domain.Entity;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.CreateCategory;

public class CreateCategoryHandler: IRequestHandler<CreateCategoryCommand, Result>
{
    private readonly ICreateCategoryDataProvider _categoryDataProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;
    
    public CreateCategoryHandler(ICreateCategoryDataProvider categoryDataProvider, IUnitOfWork unitOfWork, IFileStorage fileStorage)
    {
        _categoryDataProvider = categoryDataProvider;
        _unitOfWork = unitOfWork;
        _fileStorage = fileStorage;
    }

    public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        
        var parentCategory = await _categoryDataProvider.GetCategoryByIdAsync(request.ParentCategoryId, cancellationToken);

        var category = new Category()
        {
            Title = request.NameCategory,
            ParentCategory = parentCategory,
        };
        await _categoryDataProvider.AddCategoryAsync(category, cancellationToken);
        
        var entities = new List<MessageStructure>();
        foreach (var item in request.Payloads)
        {
            StoredFileResult? storedFile = null;
            var messageStructure = new MessageStructure();
            switch (item.TypeField)
            {
                case TypeField.File:
                    storedFile = await _fileStorage.SaveAsync(item.File, cancellationToken);
                    messageStructure.Seq = item.Seq;
                    messageStructure.TypeField = item.TypeField;
                    messageStructure.ContentType = storedFile.ContentType;
                    messageStructure.Category = category;
                    messageStructure.OriginalFileName = storedFile.FileName;
                    messageStructure.StoredFilePath = storedFile.RelativePath;
                    break;
                case TypeField.Text:
                    messageStructure.Category = category;
                    messageStructure.Value = item.Value;
                    messageStructure.TypeField = item.TypeField;
                    messageStructure.Seq = item.Seq;
                    messageStructure.Title = item.Name;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            entities.Add(messageStructure);
        }
        await _categoryDataProvider.AddMessageStructuresDataAsync(entities, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}