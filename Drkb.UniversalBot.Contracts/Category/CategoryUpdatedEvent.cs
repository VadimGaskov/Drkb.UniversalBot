using MessageBroker.Abstractions;

namespace Drkb.UniversalBot.Contracts.Category;

public record CategoryUpdatedEvent(
    Guid CategoryId,
    Guid UploadContextId,
    List<Guid>? FileIds = null
) : BaseIntegrationEvent;