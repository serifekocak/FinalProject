using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encryption
{
    // JWT'yi oluşturmak için gerekli olan security key ve algoritmayı birleştirir.
    public class SigningCredientialHelper
    {
        public static SigningCredentials CreateCredentials (SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
