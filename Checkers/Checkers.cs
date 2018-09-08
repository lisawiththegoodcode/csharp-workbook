using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

        }
    }

    //an enum to set the color of a checker to either black or white, this will be used by the checker class
    public enum Color { black, white };

    //a coordinates struct to be used by the Checker class
    public struct Coordinates
    {
        public int x {get; set;}
        public int y {get; set;}

        public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    //Checker Class
    public class Checker
    {
        //Properties: symbol, coordinates, color
        public string Symbol  { get; private set; }
        public Coordinates coords {get; set;}
        public Color color {get; set;}


        //Checker constructor takes in a  color and coordinates and sets the symbol based on the color
        public Checker(Color color, Coordinates coords)
        {
            this.coords = coords;
            this.color = color;

            if (color == Color.black)
            {  
                //the open dot symbol is for black
                Symbol = "◦"; 

            }
            else if (color == Color.white)
            {
                //the closed dot symbol is for white
                Symbol = "•";
            }
        }
    }

    public class Board
    {
        //properties: a grid and a list of checkers
        public string[][] Grid  { get; set; }
        public List<Checker> Checkers { get; set; }
        
        //board constructor initializes checkers and new gameboard
        public Board()
        {
            //initialize empty grid
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
  
            //initialize checkers list 
            Checkers = new List<Checker>();
            //loops to initialize the checkers at the appropriate starting coordinates
            for (int y=0; y<8; y++)
            {
                //for each y 0-7, loops the x, increasing by two to create checkers on every other column
                for(int x=0; x<8; x+=2)
                {
                    //if y is an even number, start the checkers at x=1, not x=0
                    if (y%2 == 0 && x==0)
                    {
                        x=1;
                    }

                    Coordinates coords = new Coordinates(x, y);
                    
                    //conditional logic to create white on rows 0, 1, 2, nothing on rows 3, 4 and black on rows 5, 6, 7
                    if (y<3)
                    {
                        Checkers.Add(new Checker(Color.white, coords));
                    }
                    else if (y == 3 || y == 4)
                    {
                        continue;
                    }
                    else
                    {
                       Checkers.Add(new Checker(Color.black, coords));
                    }
                }
            }

            // Console.WriteLine(Checkers.Count + "checker pieces have been instantiated");
            PlaceCheckers();
        }
        //loops through the list of checkers to set the grid with the checker piece symbols that match the Checkers list
        public void PlaceCheckers()
        {
            foreach (Checker check in Checkers)
            {
                Grid[check.coords.y][check.coords.x] = check.Symbol;
            }
        }
        //prints contents of Grid[][] to the console, along with an x axis of integers, y axis of integers and lines for formatting 
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
        
        public Checker SelectChecker(int row, int column)
        {
            return Checkers.Find(x => (x.coords.x == column && x.coords.y == row));
        }

        //takes in the checker to move, and the row and column the user would like to move it to. if the move is not valid, it throws an exception, if it is valid moves the checker 
        public void MoveChecker (Checker check, int newRow, int newColumn)
        {
            if (check == null)
            {
                throw new Exception("Attempted to move from a space that does not contain a checker");
            }
            else if (newRow > 7 || newRow < 0 || newColumn > 7 || newRow < 0)
            {
                throw new Exception("Attempting to move to a space that is not on the board");
            }
            else if (SelectChecker(newRow, newColumn) != null)
            {
                throw new Exception("Attempting to move to a space that is occupied");
            }
            else if (check.coords.x == newColumn || check.coords.y == newRow)
            {
                throw new Exception("Attempting a linear move");
            }
            else if (((Math.Abs(check.coords.x - newColumn) == 1) && (Math.Abs(check.coords.y - newRow) == 1)) || ((Math.Abs(check.coords.x - newColumn) == 2) && (Math.Abs(check.coords.y - newRow) == 2) && (SelectChecker(check.coords.y + 1, check.coords.x + 1) != null || SelectChecker(check.coords.y - 1, check.coords.x - 1) != null)))
            {
                Grid[check.coords.y][check.coords.x] = " ";
                check.coords = new Coordinates (newColumn, newRow);
                PlaceCheckers();
                DrawBoard(); 
            }
            else
            {
                throw new Exception("attempting something else");
            }
        }

        
        public void RemoveChecker(int row, int column)
        {
            // Your code here
            return;
        }
        
        public bool CheckForWin()
        {
            return Checkers.All(x => x.color == Color.white) || !Checkers.Exists(x => x.color == Color.white);
        }
    }


    class Game
    {
        public Game()
        {
            Board board = new Board(); 
            board.DrawBoard();

            while(board.CheckForWin() == false)
            {

                Console.WriteLine("Enter Pickup Row:");
                int row = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Pickup Column:");
                int column = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Placement Row:");
                int newRow = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Placement Column:");
                int newColumn = int.Parse(Console.ReadLine());

                try
                {
                    board.MoveChecker(board.SelectChecker(row, column), newRow, newColumn);

                } 
                catch (Exception)
                {
                    Console.WriteLine(@"That's not a valid move. Please try again");
                }
            }
        }
    }
}
