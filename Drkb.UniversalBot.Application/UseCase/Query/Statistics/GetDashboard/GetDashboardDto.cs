namespace Drkb.UniversalBot.Application.UseCase.Query.Statistics.GetDashboard;

public record PopularCategory
{
    public string Name { get; set; }
    public int Count { get; set; }
}

public record ActiveWeek
{
    public string Date { get; init; }
    public int CountUsers { get; init; }
    public int CountMessages { get; init; }
}

public record GetDashboardDto
{
    public int CountUsers { get; init; }
    public int CountMessages { get; init; }
    public int CountCategories { get; init; }
    public int ActiveUsersToday { get; init; }
    public List<ActiveWeek> ActiveWeeks { get; init; }
    public List<PopularCategory> PopularCategories { get; set; }
}