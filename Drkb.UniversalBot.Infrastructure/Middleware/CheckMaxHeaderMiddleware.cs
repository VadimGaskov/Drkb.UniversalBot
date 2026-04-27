using Drkb.UniversalBot.Infrastructure.Option;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Drkb.UniversalBot.Infrastructure.Middleware;

public class CheckMaxHeaderMiddleware(RequestDelegate next, ILogger<CheckMaxHeaderMiddleware> logger, IOptions<MaxOption> option)
{
    
    public async Task InvokeAsync(HttpContext context)
    {
        var maxOption = option.Value;
        
        if (context.Request.Path.StartsWithSegments("/api/max/webhook"))
        {
            if (string.IsNullOrWhiteSpace(context.Request.Headers["X-Max-Bot-Api-Secret"]) ||
                context.Request.Headers["X-Max-Bot-Api-Secret"] != maxOption.Secret)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }
        }
        await next(context);
    }
}