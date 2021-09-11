using Microsoft.IdentityModel.Tokens;

namespace Utils.Domain.Models.Auth
{
    public class Token
    {
        public Token(SecurityToken token, string jwt)
        {
            JWT = jwt;
            Issuer = token.Issuer;
            Validade = token.ValidTo.ToString("s") + "Z";
        }

        public string JWT { get; set; }
        public string Issuer { get; set; }
        public string Validade { get; set; }
    }
}
