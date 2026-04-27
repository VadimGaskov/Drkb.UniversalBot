using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.Max.InitWebhook;

public class InitWebhookHandler: IRequestHandler<InitWebhookCommand, Result>
{
    private readonly IInitWebhookService _initWebhookService;

    public InitWebhookHandler(IInitWebhookService initWebhookService)
    {
        _initWebhookService = initWebhookService;
    }

    public async Task<Result> Handle(InitWebhookCommand request, CancellationToken cancellationToken)
    {
        await _initWebhookService.ProcessAsync();
        return Result.Success();
    }
}