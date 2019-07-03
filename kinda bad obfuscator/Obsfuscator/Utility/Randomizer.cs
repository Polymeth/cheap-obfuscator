using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinda_bad_obfuscator.Obsfuscator.Utility
{
    public class Randomizer
    {
        private static Random random = new Random();
        private static Random lenght = new Random();

        public string GetAlphanumericRandom()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            return new string(Enumerable.Repeat(chars, lenght.Next(3, 16)).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
