using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Saritasa.UnrealEstate.Web
{
    public class AuthOptions
    {
        public const string Issuer = "UnrealEstate";

        public const string Audience = "https://localhost:44331/";

        const string Key = "mysupersecret_secretkey!123";

        public const int Lifetime = 100;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
