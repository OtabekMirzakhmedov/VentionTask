using VentionTask.Data;

namespace VentionTask.Interfaces
{
    public interface IUserRepository
    {
        Task AddOrUpdateUsersAsync(IEnumerable<User> users);
        Task<List<User>> GetUsersAscending(int limit);
    }
}
