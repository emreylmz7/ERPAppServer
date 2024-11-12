namespace ERPServer.Domain.Repositories;

public interface IUserRepository
{
    Task<Guid?> GetCurrentUserId();
}
