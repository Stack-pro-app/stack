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
                if (target == null) throw new InvalidOperationException("Invalid User");
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

        public async Task<IEnumerable<object>> GetUsersByWorkspaceAsync(int workspaceId)
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

        public async Task<IEnumerable<string>> AddUsersToWorkspace(int workspaceId, ICollection<int> usersId)
        {
            try
            {
                var IsValidWorkspace = await _context.Workspaces.FirstOrDefaultAsync(w => w.Id == workspaceId);
                if (IsValidWorkspace == null) throw new InvalidOperationException("Workspace Invalid");
                List<string> results = new List<string>();
                foreach (var Id in usersId)
                {
                    var IsValidUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
                    if (IsValidUser == null)
                    {
                        results.Add("Failed To Add User with ID:" + Id);
                        continue;
                    }
                    UserWorkspace userWorkspace = new()
                    {
                        WorkspaceId = workspaceId,
                        UserId = Id,
                    };
                    Console.WriteLine($"User {userWorkspace.WorkspaceId}");

                    var result = await _context.UsersWorkspaces.AddAsync(userWorkspace);
                    if (result.State != EntityState.Added) { results.Add("Failed To Add User with ID:" + Id); }
                    else
                    {
                        results.Add("Successfully Added User with ID:" + Id);
                    }
                }
                await _context.SaveChangesAsync();
                return results;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Addin users to workspace: {ex.Message}");
                throw;
            }

        }
        public async Task<IEnumerable<string>> RemoveUserFromWorkspace(int workspaceId, ICollection<int> usersId)
        {
            List<String> results = new List<String>();
            try
            {
                foreach (var Id in usersId)
                {
                    UserWorkspace result = await _context.UsersWorkspaces.FirstOrDefaultAsync(w => w.WorkspaceId == workspaceId && w.UserId == Id) ?? throw new Exception("The User is already not a member");
                    _context.UsersWorkspaces.Remove(result);
                    results.Add("Succesfully deleted");
                }
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                results.Add("Failed To delete :" + ex.Message);
                throw;
            }
            return results;
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception("User not found");
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Workspace>> SetLoginAndGetWorkspaces(int authId)
        {
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.AuthId == authId) ?? throw new Exception("User not found");
                user.Last_login = DateTime.Now;
                IEnumerable<Workspace> workspaces = await _context.UsersWorkspaces.Where(uw => uw.UserId == user.Id).Join(
                        _context.Workspaces,
                        u => u.WorkspaceId,
                        w => w.Id,
                        (u, w) => w
                    ).ToListAsync();
                _context.SaveChanges();
                return workspaces;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}