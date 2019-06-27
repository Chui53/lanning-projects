using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEncrypter
{
    class Program
    {
        static void Main(string[] args)
        {
            string text;
            Cipher c;
            Console.WriteLine("Lanning Cipher");
            while (true)
            {
                Console.Write("Encrypt or Decrypt(e/d)?: ");
                if (Console.Read() == 'e')
                {
                    Console.Write("Enter text to Encrypt: ");
                    text = Console.ReadLine();
                    c = new Cipher(text);
                    c.Encrypt();
                }
                else if (Console.Read() == 'd')
                {
                    Console.Write("Enter text to Decrypt: ");
                    text = Console.ReadLine();
                    c = new Cipher(text);
                    c.Decrypt();
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }
        }
    }
}
