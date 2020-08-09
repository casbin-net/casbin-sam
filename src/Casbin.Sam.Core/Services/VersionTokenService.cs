using System.Security.Cryptography;

namespace Casbin.Sam.Core.Services
{
    public static class VersionTokenService
    {
        private static byte[] GenerateRandomKey()
        {
            var bytes = new byte[20];
            RandomNumberGenerator.Fill(bytes);
            return bytes;
        }

        public static string GenerateRandomVersionToken()
        {
            return Rfc3548Base32Service.ToBase32(GenerateRandomKey());
        }

        public static bool ValidateVersionToken(string exceptVersionToken, string actualVersionToken)
        {
            return string.Equals(exceptVersionToken, actualVersionToken);
        }
    }
}
