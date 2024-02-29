using messaging_service.models.domain;

namespace messaging_service.Repository.Interfaces
{
    public interface IMemberRepository
    {
        bool CreateMember(Member member);
        bool DeleteMemberById(int memberId);
        bool DeleteMemberByUserChannel(int userId,int channelId);
        IEnumerable<Member> GetMembersByChannel(int channelId);
        IEnumerable<Member> GetMembersByUser(int userId);
    }
}
