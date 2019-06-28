using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        static void Main()
        {
            int input;
            Board board = new Board();
            Console.WriteLine("Welcome to Tic Tac Toe!");
            Console.WriteLine("You will be facing against an AI opponent who will be playing X.");
            Console.WriteLine("The AI will always go first.");
            Console.WriteLine("The board looks like this:");
            Console.WriteLine(
                "   |   |   \n" +
                " 1 | 2 | 3 \n" +
                "   |   |   \n" +
                "---+---+---\n" +
                "   |   |   \n" +
                " 4 | 5 | 6 \n" +
                "   |   |   \n" +
                "---+---+---\n" +
                "   |   |   \n" +
                " 7 | 8 | 9 \n" +
                "   |   |   ");
            Console.WriteLine("Each square is represented by a number.");
            Console.WriteLine("This is how you will input your moves.");
            while (true)
            {
                try
                {
                    Console.Write("Make a move (#): ");
                    input = Int32.Parse(Console.ReadLine());
                    board.Play(input);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect Input");
                }
            }
        }
    }
}
