using System.Text.Json;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using Drkb.UniversalBot.Domain.Entity;

namespace Drkb.UniversalBot.Infrastructure.Services;

public class VkKeyboardFactory: IVkKeyboardFactory
{
    private string Generation(List<Category> categories, bool withCoBack, bool inline)
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

        if (withCoBack)
        {
            buttons.Add(new object[]
            {
                new
                {
                    action = new
                    {
                        type = "callback",
                        label = "На главное меню",
                        payload = JsonSerializer.Serialize(new { command = "back" }),
                    }
                }
            });
        }
        
        var keyboard = new
        {
            one_time = false,
            inline,
            buttons
        };
        
        return JsonSerializer.Serialize(keyboard);
    }

    public string GetVkKeyboardWithBack(List<Category> categories)
    {
        return Generation(categories, true, true);
    }

    public string GetVkKeyboard(List<Category> categories)
    {
        return Generation(categories, false, false);
    }
}