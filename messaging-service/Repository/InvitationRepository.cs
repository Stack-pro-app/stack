using messaging_service.Data;
using messaging_service.models.domain;
using messaging_service.Models.Domain;
using messaging_service.Models.Dto.Others;
using messaging_service.Producer;
using messaging_service.Repository.Interfaces;
using messaging_service.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace messaging_service.Repository
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly AppDbContext _context;
        private readonly IRabbitMQProducer _pr;
        private readonly INotificationService _ns;
        public InvitationRepository(AppDbContext context,IRabbitMQProducer pr,INotificationService ns)
        {
            _context = context;
            _pr = pr;
            _ns = ns;
        }

        public async Task<Invitation> FindInvitationByToken (string token)
        {
            Invitation res = await _context.Invitations.FirstOrDefaultAsync(i => i.Token == token) ?? throw new ValidationException("Invitation not found");
            return res;
        }
            

        public async Task AcceptInvitation(string token)
        {
            Invitation res = await FindInvitationByToken(token);
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == res.UserId) ?? throw new ValidationException("User not found");
            UserWorkspace uw = new()
            {
                WorkspaceId = res.WorkspaceId,
                UserId = res.UserId
            };
            _context.UsersWorkspaces.Add(uw);
            // Send to PM Service
            PMUser pMUser = new()
            {
                workspaceId = res.WorkspaceId,
                authId = user.AuthId
            };
            _pr.SendToQueue(pMUser,"workspaceJoin");
            _context.Remove(res);
            await _context.SaveChangesAsync();
            await _ns.SendJoiningWorkspaceNotif(user.Id,res.WorkspaceId);
        }

        public async Task CreateInvitation(Invitation inv)
        {
            await _context.Invitations.AddAsync(inv);
            await _context.SaveChangesAsync();
        }

        public async Task CreateInvitation(int workspaceId, string authId)
        {
            User res = await _context.Users.FirstOrDefaultAsync(u => u.AuthId == authId)?? throw new ValidationException("User not found");
            Invitation inv = new()
            {
                WorkspaceId = workspaceId,
                UserId = res.Id,
            };
            await CreateInvitation(inv);
        }

        public async Task DeleteInvitation(string token)
        {
            Invitation res = await FindInvitationByToken(token);
            _context.Remove(res);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Invitation>> GetInvitations(int userId)
        {
            IEnumerable<Invitation> res = await _context.Invitations.Where(i => i.UserId == userId).ToListAsync();
            return res;
        }

        public async Task<IEnumerable<Invitation>> GetInvitations(string authId)
        {
            User res = await _context.Users.FirstOrDefaultAsync(u => u.AuthId == authId) ?? throw new ValidationException("User not found");
            IEnumerable<Invitation> invs = await _context.Invitations.Where(i => i.UserId == res.Id).ToListAsync();
            return invs;
        }
    }
}
