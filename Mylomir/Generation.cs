using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mylomir
{
    class Generation
    {
        public static string GenerationLogin()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            Random random = new Random();
            StringBuilder login = new StringBuilder();

            for (int i = 0; i <10; i++)
            {
                login.Append(chars[random.Next(chars.Length)]);
            }
           
            return login.ToString();
        }
        public static string GenerationPassword()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%?(){}[]*/.^+,";

            Random random = new Random();
            StringBuilder password = new StringBuilder();

            for (int i = 0; i < 8; i++)
            {
                password.Append(chars[random.Next(chars.Length)]);
            }
            return password.ToString();
        }
        public static string GenerationArticul()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            Random rand = new Random();
            StringBuilder articul = new StringBuilder();

            for( int i = 0; i < 6; i++)
            {
                articul.Append(chars[rand.Next(chars.Length)]);
            }
            return articul.ToString();
        }
    }
}
