using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        private const string characterSet = "@+._ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private static string[] hashedString = {
            "f894e71e1551d1833a977df952d0cc9de44a1f9669fbf97d51309a2c6574d5eaa746cdeb9ee1",
            "a5dfc771d280d33e567204c2b7f12a3b18bf3470c7ca102a33b6e48a0b49e999dc7d88f3e707",
            "3040596e98687c4d1730f3ac2fb2fe4f3e2fba56b508b77c09d5f5481ddcf2f06d8cea047c68",
            "a18aa13ef655143ae42ec22e66d55e6679d0f25c2aa9a0fe3e371c1c78d16b4fa24e50152344",
            "b4b92f4da49a321d3b56dac32d5a4be43a043529a4ae1310f8ce6156d91731f214e0a0b3deba",
            "1eb1c91e8022879e2e52cc779263f0d21cffd5acc468396b4556d357fdb2118f319e1605aac7",
            "e849f7cb2cd9a04322ebb39773345ff253b3aa09375da98f17812543ddbdb41fe4d2f1127fef",
            "95cc95337de5fdafe0324b2a6c7cfbd1375098b5499d"
        }; 

        public static string NestedHash(string text)
        {
            return HashMD5(HashMD5(text) + text + HashMD5(text));
        }

        public static string HashMD5(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        static void Main(string[] args)
        {
            string decrypted = "";

            foreach (var hash in hashedString)
            {
                foreach (var firstChar in characterSet)
                {
                    foreach (var secondChar in characterSet)
                    {
                        StringBuilder randomPairText = new StringBuilder();
                        randomPairText.Append(firstChar);
                        randomPairText.Append(secondChar);

                        Console.WriteLine(randomPairText);

                        string hashedPair = NestedHash(randomPairText.ToString());

                        if (hash.Contains(hashedPair, StringComparison.OrdinalIgnoreCase))
                        {
                            decrypted += randomPairText;
                        }

                        Console.WriteLine(hashedPair);
                    }
                }
            }

            Console.WriteLine("Decrypted: " + decrypted);
        }
    }
}