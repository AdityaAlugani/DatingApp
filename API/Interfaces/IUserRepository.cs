using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<IEnumerable<specialuserDto>> GetMembersAsync();
        Task<AppUser> GetUsersByIdAsync(int id);
        Task<AppUser?> GetUserByUsernameAsync(string username);
        Task<specialuserDto?> GetMemberByUsernameAsync(string username);

    }
}