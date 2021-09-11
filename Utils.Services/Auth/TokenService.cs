using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Utils.Domain.Interfaces.Auth;
using Utils.Domain.Models.Auth;

namespace Utils.Services.Auth
{
    public sealed class TokenService
    {
        private readonly ITokenSettings _tokenSettings;

        public TokenService(ITokenSettings tokenSettings)
        {
            _tokenSettings = tokenSettings;
        }

        public JwtSecurityToken GetToken(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(jwt);
        }

        public bool ValidateToken(JwtSecurityToken token)
        {
            return token.Issuer == _tokenSettings.Issuer && token.Audiences.Contains(_tokenSettings.Audience);
        }

        public Token GenerateToken(IAuthUser user, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(user.GetClaims(role)),
                Issuer = _tokenSettings.Issuer,
                Expires = _tokenSettings.Expires,
                Audience = _tokenSettings.Audience,
                NotBefore = _tokenSettings.NotBefore,
                SigningCredentials = _tokenSettings.SigningCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new Token(token, tokenHandler.WriteToken(token));
        }
    }
}
