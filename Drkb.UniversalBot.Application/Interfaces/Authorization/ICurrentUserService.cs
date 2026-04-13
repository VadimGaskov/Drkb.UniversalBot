namespace Drkb.UniversalBot.Application.Interfaces.Authorization;

public interface ICurrentUserService
{
    public Guid UserId { get; }
    public List<string> Rights { get; }
    public List<Guid> Departments { get; }
}