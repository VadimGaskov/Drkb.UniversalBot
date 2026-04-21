using MassTransit;

namespace Drkb.UniversalBot.Contracts.Category;

[EntityName("category.created.v1")]
public record CategoryCreatedEvent(
    Guid CategoryId,
    Guid UploadContextId,
    List<Guid>? FileIds = null
);