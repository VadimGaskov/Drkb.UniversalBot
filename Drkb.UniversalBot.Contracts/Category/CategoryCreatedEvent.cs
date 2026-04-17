using MessageBroker.Abstractions;

namespace Drkb.UniversalBot.Contracts.Category;

public record CategoryCreatedEvent(
    Guid CategoryId,
    Guid UploadContextId,
    List<Guid>? FileIds = null
) : BaseIntegrationEvent;