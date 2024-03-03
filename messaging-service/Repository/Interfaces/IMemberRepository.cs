using messaging_service.models.domain;

namespace messaging_service.Repository.Interfaces
{
    public interface IMemberRepository
    {
        Task<bool> CreateMemberAsync(Member member);
        Task<bool> DeleteMemberByIdAsync(int memberId);
        Task<bool> DeleteMemberByUserChannelAsync(int userId, int channelId);
        Task<IEnumerable<Member>> GetMembersByChannelAsync(int channelId);
        Task<IEnumerable<Member>> GetMembersByUserAsync(int userId);
    }
}
