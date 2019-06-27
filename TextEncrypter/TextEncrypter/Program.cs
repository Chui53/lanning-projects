using System;

namespace TextEncrypter
{
    class Program
    {
        static void Main(string[] args)
        {
            string text;
            Cipher c;
            Console.WriteLine("The Lanning Cipher");
            while (true)
            {
                Console.Write("Encrypt or Decrypt(e/d)?: ");
                string choice = Console.ReadLine();
                if (choice == "e")
                {
                    Console.Write("Enter text to Encrypt: ");
                    text = Console.ReadLine();
                    c = new Cipher(text);
                    Console.WriteLine("Encrypted Text: " + c.Encrypt());
                }
                else if (choice == "d")
                {
                    Console.Write("Enter text to Decrypt: ");
                    text = Console.ReadLine();
                    c = new Cipher(text);
                    Console.WriteLine("Decrypted Text: " + c.Decrypt());
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }
        }
    }
}
