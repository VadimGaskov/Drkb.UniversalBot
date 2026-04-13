using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using Drkb.UniversalBot.Domain.Entity;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;

public class CreateStructureOfMessageHandler: IRequestHandler<CreateMessageStructureCommand, Result>
{
    private readonly ICreateStructureOfMessageDataProvider _structureOfMessageDataProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;
    
    public CreateStructureOfMessageHandler(ICreateStructureOfMessageDataProvider structureOfMessageDataProvider, IUnitOfWork unitOfWork, IFileStorage fileStorage)
    {
        _structureOfMessageDataProvider = structureOfMessageDataProvider;
        _unitOfWork = unitOfWork;
        _fileStorage = fileStorage;
    }

    public async Task<Result> Handle(CreateMessageStructureCommand request, CancellationToken cancellationToken)
    {
        var category = await _structureOfMessageDataProvider.GetCategoryByIdAsync(request.CategoryId, cancellationToken);
        if (category is null)
            return Result.NotFound("No category found");
        
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
                    messageStructure.Title = item.Title;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            entities.Add(messageStructure);
        }
        await _structureOfMessageDataProvider.AddAsync(entities, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}