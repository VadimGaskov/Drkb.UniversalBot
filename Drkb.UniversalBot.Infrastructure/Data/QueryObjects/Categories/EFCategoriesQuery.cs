using Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategories;
using Drkb.UniversalBot.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Drkb.UniversalBot.Infrastructure.Data.QueryObjects.Categories;

public class EFCategoriesQuery: ICategoriesQuery
{
    private readonly BotDbContext _context;

    public EFCategoriesQuery(BotDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoriesDto>> ExecuteAsync(GetCategoriesQuery query, CancellationToken cancellationToken = default)
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        var lookup = categories.ToDictionary(x => x.Id);
        
        foreach (var category in categories)
        {
            if (category.ParentCategoryId.HasValue &&
                lookup.TryGetValue(category.ParentCategoryId.Value, out var parent))
            {
                category.ParentCategory = parent;
                parent.ChildrenCategories.Add(category);
            }
        }
        
        var rootCategories = categories
            .Where(x => x.ParentCategoryId == null)
            .ToList();
        
        return rootCategories
            .Select(x => MapCategoryTree(x, new HashSet<Guid>()))
            .ToList();
    }

    private CategoriesDto MapCategoryTree(Category category, HashSet<Guid> visited)
    {
        if (!visited.Add(category.Id))
            throw new InvalidOperationException(
                $"Обнаружен цикл в дереве категорий. CategoryId: {category.Id}");

        return new CategoriesDto
        {
            Id = category.Id,
            Title = category.Title,
            CategoryChildren = category.ChildrenCategories
                .Select(child => MapCategoryTree(child, new HashSet<Guid>(visited)))
                .ToList()
        };
    }
}