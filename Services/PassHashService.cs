using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace KSVA2._0_WPF.Services
{
    public class PassHashService
    {
        public static string Hash(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static bool Verify(string password, string hash)
        {
            var hashedInput = Hash(password);
            return hashedInput == hash;
        }
    }
}
