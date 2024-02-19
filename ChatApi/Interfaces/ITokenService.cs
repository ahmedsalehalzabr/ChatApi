using ChatApi.Entities;

namespace ChatApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
