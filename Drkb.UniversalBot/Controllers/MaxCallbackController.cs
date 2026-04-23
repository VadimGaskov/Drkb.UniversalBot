using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Drkb.UniversalBot.Controllers;

[ApiController]
[Authorize]
[Route("api/max/callback")]
public class MaxCallbackController: ControllerBase
{
    
}