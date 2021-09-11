using Microsoft.IdentityModel.Tokens;
using System;

namespace Utils.Domain.Interfaces.Auth
{
    public interface ITokenSettings
    {
        string Audience { get; }
        string Issuer { get; }
        SigningCredentials SigningCredentials { get; }
        SecurityKey SecurityKey { get; }

        DateTime Expires { get; }
        DateTime NotBefore { get; }
    }
}
