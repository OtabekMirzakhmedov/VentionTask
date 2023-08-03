using Microsoft.EntityFrameworkCore;
using VentionTask.Data;
using VentionTask.Interfaces;

namespace VentionTask.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddOrUpdateUsersAsync(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserName == user.UserName && u.UserIdentifier == user.UserIdentifier);

                if (existingUser == null)
                    _context.Users.Add(user);
                else
                {
                    existingUser.UserName = user.UserName;
                    existingUser.Age = user.Age;
                    existingUser.City = user.City;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    existingUser.Email = user.Email;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsersAscending(int limit)
        {
            if (limit <= 0)
                throw new ArgumentException("Limit must be a positive integer.", nameof(limit));

            var users = await _context.Users.OrderBy(u => u.UserName).Take(limit).ToListAsync();
            return users;
        }
    }
}
