using AutoMapper;
using messaging_service.Data;
using messaging_service.models.domain;
using messaging_service.models.dto.Detailed;
using messaging_service.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace messaging_service.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(AppDbContext context,IMapper mapper,ILogger<UserRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
        }

        public async Task<bool> DeleteUserAsync(string authId)
        {
                var userToDelete = await _context.Users.FirstOrDefaultAsync(x => x.AuthId == authId) ?? throw new ValidationException("User not found.");
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
                return true;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
                var target = await _context.Users.FirstOrDefaultAsync(x => x.AuthId == user.AuthId);
                if (target == null) throw new ValidationException("Invalid User");
                target.Name = user.Name;
                target.Email = user.Email;
                await _context.SaveChangesAsync();
                return true;
        }

        public async Task<User> GetUserAsync(string authId)
        {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.AuthId == authId) ?? throw new ValidationException("User not found.");
                return user;
        }

        public async Task<User> GetUserAsync(int id)
        {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id) ?? throw new ValidationException("User not found.");
                return user;
        }

        public async Task<IEnumerable<User>> GetUsersByChannelAsync(int channelId)
        {
                IEnumerable<User> usersByChannel = await _context.Members
                    .Where(member => member.ChannelId == channelId)
                    .Include(m=>m.User)
                    .Select(m=>m.User)
                    .ToListAsync();
                _logger.LogInformation("repo working");
                return usersByChannel;
        }

        public async Task<IEnumerable<UserDetailDto>> GetUsersByWorkspaceAsync(int workspaceId)
        {
                IEnumerable<User> users = await _context.UsersWorkspaces.Where(u => u.WorkspaceId == workspaceId).Include(uw=>uw.User).Select(uw=>uw.User).ToListAsync();
                IEnumerable<UserDetailDto> usersDetails = _mapper.Map<IEnumerable<User>,IEnumerable<UserDetailDto>>(users);
                return usersDetails;
        }

        public async Task<IEnumerable<string>> AddUsersToWorkspace(int workspaceId, ICollection<int> usersId)
        {
                var IsValidWorkspace = await _context.Workspaces.FirstOrDefaultAsync(w => w.Id == workspaceId);
                if (IsValidWorkspace == null) throw new ValidationException("Workspace Invalid");
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
        public async Task RemoveUserFromWorkspace(int workspaceId, int userId)
        {
                UserWorkspace result = await _context.UsersWorkspaces.FirstOrDefaultAsync(w => w.WorkspaceId == workspaceId && w.UserId == userId) ?? throw new ValidationException("The User is already not a member");
                _context.UsersWorkspaces.Remove(result);
                await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email) ?? throw new ValidationException("User not found");
                return user;
        }

        public async Task<IEnumerable<Workspace>> SetLoginAndGetWorkspaces(string authId)
        {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.AuthId == authId) ?? throw new ValidationException("User not found");
                user.Last_login = DateTime.Now;
                await _context.SaveChangesAsync();
                IEnumerable<Workspace> workspaces = await _context.UsersWorkspaces.Where(uw => uw.UserId == 1).Include(uw => uw.Workspace).Select(uw=>uw.Workspace).ToListAsync();
                return workspaces;
        }
    }
}