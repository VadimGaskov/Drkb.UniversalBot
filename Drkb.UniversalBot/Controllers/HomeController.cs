using System.Diagnostics;
using Drkb.UniversalBot.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Drkb.UniversalBot.Models;

namespace Drkb.UniversalBot.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IS3Service _s3Service;

    public HomeController(ILogger<HomeController> logger, IS3Service s3Service)
    {
        _logger = logger;
        _s3Service = s3Service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var r = await _s3Service.GetAllUrls(Guid.Parse("d2b75e04-4b63-440c-9163-fae1b0dc9dc7"));
        return Ok();
    }
}