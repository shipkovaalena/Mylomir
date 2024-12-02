using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mylomir
{
    public static class Validation
    {
        public static bool Login(char c)
        {
            return char.IsDigit(c) ||
                char.IsControl(c) ||
                c >= 'a' && c <= 'z';
        }
        public static bool Password(char c)
        {
            return char.IsDigit(c) ||
                char.IsControl(c) ||
                c >= 'a' && c <= 'z' ||
                c >= 'A' && c <= 'Z' ||
                "!@#$%?(){}[]*/.^+,".Contains(c);
        }
        public static bool Name(char c)
        {
            return char.IsControl(c) ||
                c >= 'а' && c <= 'я' ||
                c >= 'А' && c <= 'Я' ||
                "-".Contains(c);
        }
        public static bool RusLetter(char c)
        {
            return char.IsControl(c) ||
                c >= 'а' && c <= 'я' ||
                c >= 'А' && c <= 'Я';
        }
        public static bool Digit(char c)
        {
            return char.IsControl(c) ||
                char.IsDigit(c);
        }
        public static bool Article(char c)
        {
            return char.IsControl(c) ||
                char.IsDigit(c) ||
                c >= 'A' && c <= 'Z';
        }
    }
}
