using messaging_service.Data;
using messaging_service.Repository.Interfaces;
using messaging_service.models.domain;
using Microsoft.EntityFrameworkCore;

namespace messaging_service.Repository

{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateMemberAsync(Member member)
        {
            try
            {
                _context.Members.Add(member);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Membership: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteMemberByIdAsync(int memberId)
        {
            try
            {
                var memberToDelete = await _context.Members.FirstOrDefaultAsync(x => x.Id == memberId) ?? throw new InvalidOperationException("Member not found.");
                _context.Members.Remove(memberToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Deleting Membership: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteMemberByUserChannelAsync(int userId, int channelId)
        {
            try
            {
                var memberToDelete = await _context.Members.FirstOrDefaultAsync(x => x.UserId == userId && x.ChannelId == channelId) ?? throw new InvalidOperationException("Member not found.");
                _context.Members.Remove(memberToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Deleting Membership: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Member>> GetMembersByChannelAsync(int channelId)
        {
            try
            {
                var memberships = await _context.Members.Where(x => x.ChannelId == channelId).ToListAsync();
                return memberships;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get Members: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Member>> GetMembersByUserAsync(int userId)
        {
            try
            {
                var memberships = await _context.Members.Where(x => x.UserId == userId).ToListAsync();
                return memberships;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get Members: {ex.Message}");
                throw;
            }
        }
    }
}
