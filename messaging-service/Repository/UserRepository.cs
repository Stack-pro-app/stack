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
        public UserRepository(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(string authId)
        {
            try
            {
                var userToDelete = await _context.Users.FirstOrDefaultAsync(x => x.AuthId == authId) ?? throw new ValidationException("User not found.");
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                var target = await _context.Users.FirstOrDefaultAsync(x => x.AuthId == user.AuthId);
                if (target == null) throw new ValidationException("Invalid User");
                target.Name = user.Name;
                target.Email = user.Email;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetUserAsync(string authId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.AuthId == authId) ?? throw new ValidationException("User not found.");
                return user;
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserDetailDto>> GetUsersByWorkspaceAsync(int workspaceId)
        {
            try
            {
                IEnumerable<User> users = await _context.UsersWorkspaces.Where(u => u.WorkspaceId == workspaceId).Include(uw=>uw.User).Select(uw=>uw.User).ToListAsync();
                IEnumerable<UserDetailDto> usersDetails = _mapper.Map<IEnumerable<User>,IEnumerable<UserDetailDto>>(users);
                return usersDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<string>> AddUsersToWorkspace(int workspaceId, ICollection<int> usersId)
        {
            try
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
            catch (Exception)
            {
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
                    UserWorkspace result = await _context.UsersWorkspaces.FirstOrDefaultAsync(w => w.WorkspaceId == workspaceId && w.UserId == Id) ?? throw new ValidationException("The User is already not a member");
                    _context.UsersWorkspaces.Remove(result);
                    results.Add("Succesfully deleted !");
                }
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                results.Add("Failed To delete !");
                throw;
            }
            return results;
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email) ?? throw new ValidationException("User not found");
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Workspace>> SetLoginAndGetWorkspaces(string authId)
        {
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.AuthId == authId) ?? throw new ValidationException("User not found");
                user.Last_login = DateTime.Now;
                await _context.SaveChangesAsync();
                IEnumerable<Workspace> workspaces = await _context.UsersWorkspaces.Where(uw => uw.UserId == 1).Include(uw => uw.Workspace).Select(uw=>uw.Workspace).ToListAsync();
                return workspaces;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}