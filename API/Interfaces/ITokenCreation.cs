
using API.Entities;

namespace API.Interfaces
{
    public interface ITokenCreation
    {
        public string CreateToken(AppUser user);
    }

    
}