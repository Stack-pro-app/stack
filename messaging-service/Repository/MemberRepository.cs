using messaging_service.Data;
using messaging_service.Repository.Interfaces;
using messaging_service.models.domain;

namespace messaging_service.Repository

{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;
        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool CreateMember(Member member)
        {
        try{
            _context.Members.Add(member);
            _context.SaveChanges();
            return true;
        }
            catch(Exception ex) {
                Console.WriteLine($"Error creating Membership: {ex.Message}");
                throw;
            }

        }

        //Deletes member by Id

        public bool DeleteMemberById(int memberId)
        {
            try
            {
                var memberToDelete = _context.Members.FirstOrDefault(x => x.Id == memberId)?? throw new InvalidOperationException("Member not found.");
                _context.Members.Remove(memberToDelete);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Deleting Membership: {ex.Message}");
                throw;
            }
        }
        //Deletes Member By UserId & ChannelId
        public bool DeleteMemberByUserChannel(int userId,int channelId)
        {
            try
            {
                var memberToDelete = _context.Members.FirstOrDefault(x => x.UserId == userId && x.ChannelId == channelId) ?? throw new InvalidOperationException("Member not found.");
                _context.Members.Remove(memberToDelete);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Deleting Membership: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Member> GetMembersByChannel(int channelId)
        {
            try
            {
                IEnumerable<Member> memberships = _context.Members.Where(x => x.ChannelId == channelId).ToList();
                return memberships;

            }
            catch(Exception ex )
            {
                Console.WriteLine($"Failed to get Members: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Member> GetMembersByUser(int userId)
        {
            try
            {
                IEnumerable<Member> memberships = _context.Members.Where(x => x.UserId == userId).ToList();
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
