using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers
{
    class Program
    {
        static void Main(string[] args)
        {
            // Checker black = new Checker(Color.black);
            // Checker white = new Checker(Color.white);
            // Console.WriteLine("Black Checker: " + black.Symbol);
            // Console.WriteLine("White Checker: " + white.Symbol);
            Game g1 = new Game();

        }
    }

    public enum Color { black, white };

    public class Checker
    {
        public string Symbol  { get; private set; }

        
        public Checker(Color color)
        {
            if (color == Color.black)
            {  
                Symbol = "•"; 

            }
            else if (color == Color. white)
            {
                Symbol = "◦";
            }
        }
    }

    public class Board
    {
        public string[][] Grid  { get; set; }
        public List<Checker> Checkers { get; set; }
        
        //board constructor initializes checkers and new gameboard
        public Board()
        {
            Checkers = new List<Checker>();
            
            for(int i=0; i<12; i++)
            {
                Checkers.Add(new Checker(Color.black));
                Checkers.Add(new Checker(Color.white));
            }
            Console.WriteLine(Checkers.Count + "checkers have been instantiated");

            Grid = new string[][] 
            {
                new string[] {" ", " ", " ", " ", " ", " ", " ", " "},
                new string[] {" ", " ", " ", " ", " ", " ", " ", " "},
                new string[] {" ", " ", " ", " ", " ", " ", " ", " "},
                new string[] {" ", " ", " ", " ", " ", " ", " ", " "},
                new string[] {" ", " ", " ", " ", " ", " ", " ", " "},
                new string[] {" ", " ", " ", " ", " ", " ", " ", " "},          
                new string[] {" ", " ", " ", " ", " ", " ", " ", " "},
                new string[] {" ", " ", " ", " ", " ", " ", " ", " "}
            };
        }
        
        public void PlaceCheckers()
        {
            // Your code here
            return;
        }
        
        public void DrawBoard()
        {
            Console.WriteLine("  0 1 2 3 4 5 6 7");
            Console.WriteLine("0 " + String.Join("|", Grid[0]));
            Console.WriteLine("  ---------------");
            Console.WriteLine("1 " + String.Join("|", Grid[1]));
            Console.WriteLine("  ---------------");
            Console.WriteLine("2 " + String.Join("|", Grid[2]));
            Console.WriteLine("  ---------------");
            Console.WriteLine("3 " + String.Join("|", Grid[3]));
            Console.WriteLine("  ---------------");
            Console.WriteLine("4 " + String.Join("|", Grid[4]));
            Console.WriteLine("  ---------------");
            Console.WriteLine("5 " + String.Join("|", Grid[5]));
            Console.WriteLine("  ---------------");
            Console.WriteLine("6 " + String.Join("|", Grid[6]));
            Console.WriteLine("  ---------------");
            Console.WriteLine("7 " + String.Join("|", Grid[7]));
        }
        
        // public Checker SelectChecker(int row, int column)
        // {
        //     return Checkers.Find(x => x.Position.SequenceEqual(new List<int> { row, column }));
        // }
        
        public void RemoveChecker(int row, int column)
        {
            // Your code here
            return;
        }
        
        // public bool CheckForWin()
        // {
        //     return Checkers.All(x => x.Color == "white") || !Checkers.Exists(x => x.Color == "white");
        // }
    }

    class Game
    {
        public Game()
        {
            Board b1 = new Board(); 
            b1.DrawBoard();
        }
    }
}
