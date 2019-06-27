using System;
using System.Text;

namespace TextEncrypter
{
    class Cipher
    {
        private readonly string text;
        private StringBuilder sb;
        public Cipher(string t) => text = t;
        public string Encrypt()
        {
            sb = new StringBuilder();
            string final;
            int count = 0;
            foreach(char c in text)
            {
                if (!Char.IsWhiteSpace(c))
                {
                    switch (count)
                    {
                        case 0:
                            sb.Append(Shifter(true, c, 3));
                            break;
                        case 1:
                            sb.Append(Shifter(true, c, 2));
                            break;
                        case 2:
                            sb.Append(Shifter(true, c, 1));
                            break;
                        case 3:
                            sb.Append(Shifter(true, c, 3));
                            break;
                        default:
                            sb.Append(Shifter(true, c, 4));
                            count = 0;
                            break;
                    }
                    count++;
                } else
                {
                    sb.Append(" ");
                }
            }
            final = sb.ToString();
            return final;
        }
        public string Decrypt()
        {
            sb = new StringBuilder();
            string final;
            int count = 0;
            foreach (char c in text)
            {
                if (!Char.IsWhiteSpace(c))
                {
                    switch (count)
                    {
                        case 0:
                            sb.Append(Shifter(false, c, 3));
                            break;
                        case 1:
                            sb.Append(Shifter(false, c, 2));
                            break;
                        case 2:
                            sb.Append(Shifter(false, c, 1));
                            break;
                        case 3:
                            sb.Append(Shifter(false, c, 3));
                            break;
                        default:
                            sb.Append(Shifter(false, c, 4));
                            count = 0;
                            break;
                    }
                    count++;
                } else
                {
                    sb.Append(" ");
                }
            }
            final = sb.ToString();
            return final;
        }
        private char Shifter(bool encrypt, char letter, int shiftnum)
        {
            char c;
            if (encrypt)
            {
                if (Char.IsUpper(letter))
                {
                    if(letter + shiftnum > 'Z')
                    {
                        c = (char)(letter + shiftnum - 'Z' + 'A');
                    } else
                    {
                        c = (char)(letter + shiftnum);
                    }
                } else
                {
                    if (letter + shiftnum > 'z')
                    {
                        c = (char)(letter + shiftnum - 'z' + 'a');
                    }
                    else
                    {
                        c = (char)(letter + shiftnum);
                    }
                }
            }
            else
            {
                if (Char.IsUpper(letter))
                {
                    if (letter - shiftnum < 'A')
                    {
                        c = (char)(letter - shiftnum + 'Z' - 'A');
                    }
                    else
                    {
                        c = (char)(letter - shiftnum);
                    }
                }
                else
                {
                    if (letter - shiftnum < 'a')
                    {
                        c = (char)(letter - shiftnum + 'z' - 'a');
                    }
                    else
                    {
                        c = (char)(letter - shiftnum);
                    }
                }
            }
            return c;
        }
    }
}
