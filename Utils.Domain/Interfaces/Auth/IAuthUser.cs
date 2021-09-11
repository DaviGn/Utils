using System.Security.Claims;

namespace Utils.Domain.Interfaces.Auth
{
    public interface IAuthUser
    {
        Claim[] GetClaims(string role);
    }
}
