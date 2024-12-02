using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Mylomir
{
    public class GetHash
    {
        public static string  GetHashPassword(string password)
        {
            using (var sh = SHA256.Create())
            {
                var shbyte = sh.ComputeHash(Encoding.UTF8.GetBytes(password));
                password = BitConverter.ToString(shbyte).Replace("-", "").ToLower();
            }
            return password;
        }
    }
}
