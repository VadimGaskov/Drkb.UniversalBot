using System.Text.Json;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using Drkb.UniversalBot.Domain.Entity;

namespace Drkb.UniversalBot.Infrastructure.Services.VkIntegration;

public class VkKeyboardFactory: IVkKeyboardFactory
{
    private string Generation(List<Category> categories, bool inline)
    {
        var buttons = categories.Select(x => new object[]
        {
            new
            {
                action = new
                {
                    type = "callback",
                    label = x.Title,
                    payload = JsonSerializer.Serialize(new { command = "select_category", id = x.Id })
                }
            }
        }).ToList();
        
        var keyboard = new
        {
            one_time = false,
            inline,
            buttons
        };
        
        return JsonSerializer.Serialize(keyboard);
    }

    public string GetVkMainKeyboard(List<Category> categories)
    {
        return Generation(categories, false);
    }
    
    public string GetVkKeyboard(List<Category> categories)
    {
        return Generation(categories, true);
    }
}