using messaging_service.Data;
using messaging_service.models.domain;
using messaging_service.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace messaging_service.Repository
{
        public class UserRepository : IUserRepository
        {
            private readonly AppDbContext _context;

            public UserRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task<bool> CreateUserAsync(User user)
            {
                try
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating user: {ex.Message}");
                    throw;
                }
            }

            public async Task<bool> DeleteUserAsync(int authId)
            {
                try
                {
                    var userToDelete = await _context.Users.FirstOrDefaultAsync(x => x.AuthId == authId) ?? throw new InvalidOperationException("User not found.");
                    _context.Users.Remove(userToDelete);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting user: {ex.Message}");
                    throw;
                }
            }

            public async Task<bool> UpdateUserAsync(User user)
            {
                try
                {
                    var target = await _context.Users.FirstOrDefaultAsync(x => x.AuthId == user.AuthId);
                    target.Name = user.Name;
                    target.Email = user.Email;
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating user: {ex.Message}");
                    throw;
                }
            }

            public async Task<User> GetUserAsync(int authId)
            {
                try
                {
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.AuthId == authId) ?? throw new InvalidOperationException("User not found.");
                    return user;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting user: {ex.Message}");
                    throw;
                }
            }

            public async Task<IEnumerable<User>> GetUsersByChannelAsync(int channelId)
            {
                try
                {
                    var usersByChannel = await _context.Members
                        .Where(member => member.ChannelId == channelId)
                        .Join(
                            _context.Users,
                            member => member.UserId,
                            user => user.Id,
                            (member, user) => user
                        )
                        .ToListAsync();
                    return usersByChannel;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting users by channel: {ex.Message}");
                    throw;
                }
            }

            public async Task<IEnumerable<User>> GetUsersByWorkspaceAsync(int workspaceId)
            {
                try
                {
                    var users = await _context.UsersWorkspaces.Where(u => u.WorkspaceId == workspaceId).Join(
                            _context.Users,
                            u => u.UserId,
                            user => user.Id,
                            (u, user) => user
                        ).ToListAsync();
                    return users;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting users by workspace: {ex.Message}");
                    throw;
                }
            }
        }
    }