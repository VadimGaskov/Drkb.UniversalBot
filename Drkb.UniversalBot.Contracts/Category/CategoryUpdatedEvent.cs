using MassTransit;

namespace Drkb.UniversalBot.Contracts.Category;

[EntityName("category.updated.v1")]
public record CategoryUpdatedEvent(
    Guid CategoryId,
    Guid UploadContextId,
    List<Guid>? FileIds = null
);