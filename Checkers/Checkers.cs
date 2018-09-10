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
        
        //takes in a row and a column coordinate, searches the Checkers list for a match, and returns the Checker if there is a match. Otherwise returns null.
        public Checker SelectChecker(int row, int column)
        {
            return Checkers.Find(x => (x.coords.x == column && x.coords.y == row));
        }

        //takes in which player's turn it is, the checker to move, and the row and column the user would like to move it to. if the move is invalid, it throws an exception, if it is valid, it moves the checker 
        public void MoveChecker (string playerTurn, Checker check, int newRow, int newColumn)
        {
            //first check for invalid moves, and throw an exception if the move is invalid
            if (check == null)
            {
                throw new Exception("Attempting to move from a space that does not contain a checker");
            }
            else if ((playerTurn == "White" && check.color == Color.black) || (playerTurn == "Black" && check.color == Color.white))
            {
                throw new Exception ("Attempting to move a checker out of turn");
            }
            else if (newRow > 7 || newRow < 0 || newColumn > 7 || newRow < 0)
            {
                throw new Exception("Attempting to move to a space that is not on the board");
            }
            else if (SelectChecker(newRow, newColumn) != null)
            {
                throw new Exception("Attempting to move to a space that is occupied");
            }
            else if ((check.coords.x == newColumn || check.coords.y == newRow) && (Math.Abs(check.coords.y - newRow) != 4)) //if the change in y is 4, then it may be a double jump attempt, in which a linear move is allowed
            {
                throw new Exception("Attempting a linear move");
            }
            else if ((check.color == Color.black && ((check.coords.y - newRow) < 0)) || (check.color == Color.white && ((check.coords.y - newRow) > 0)))
            {
                throw new Exception("Attempting to move backwards");
            }

            //then check for valid moves
            //a valid move: the basic move - the move is both one away in the x and y value (and we've already checked that the space is unoccupied)
            else if ((Math.Abs(check.coords.x - newColumn) == 1) && (Math.Abs(check.coords.y - newRow) == 1))
            {
                Move(check, newRow, newColumn);
            }
            //a valid move: the jump - the move is both two away in the x and y value, and there is a checker of the opposite color in between the current position and the new position
            else if ((Math.Abs(check.coords.x - newColumn) == 2) && (Math.Abs(check.coords.y - newRow) == 2))
            {
                Jump(check, newRow, newColumn);
            }

            //a valid move: the double jump - the move is 4 away on the y axis, and has two single jump opportunities within it. 
            else if (Math.Abs(check.coords.y - newRow) == 4)
            {
                //row and column in middle of the two checkers to jump
                int middleRow;
                int middleColumn;
                
                //double jump scenario one - the change in x is 4 / the change in y is 4
                if ((Math.Abs(check.coords.x - newColumn) == 4) && (Math.Abs(check.coords.y - newRow) == 4))
                {
                    //if change in x is positive
                    if ((newColumn - check.coords.x) > 0)
                    {
                        //then add 2 to get to the x to get the middle column
                        middleColumn = check.coords.x + 2;

                        //if the change in y is positive
                        if ((newRow - check.coords.y) > 0)
                        {
                            //then add 2 to the y to get the middle row
                            middleRow = check.coords.y + 2;
                        }
                        else
                        {
                            //otherwise it's negative so subtract 2
                            middleRow = check.coords.y - 2;
                        }
                    }
                    else
                    {
                        //otherwise the change in x is negative, so subtract 2 from the starting x to get the middle column 
                        middleColumn = check.coords.x - 2;
                        
                        //if the change in y is positive
                        if ((newRow - check.coords.y) > 0)
                        {
                            //then add 2 to the y to get the middle row
                            middleRow = check.coords.y + 2;
                        }
                        else
                        {
                            //otherwise it's negative so subtract 2
                            middleRow = check.coords.y - 2;
                        }
                    }
                    DoubleJump(check, newRow, newColumn, middleRow, middleColumn);
                }
                
                //double jump scenario 2 the change in x is 0 / the change in y is 4
                else if ((check.coords.x == newColumn) && (Math.Abs(check.coords.y - newRow) == 4))
                {
                    //if theres a checker to in the positive x and y direction and the color is opposite the current checker, then add 2 to the x and the y to get the middle position
                    if ((check.color == Color.white) && ((SelectChecker((check.coords.y + 1), (check.coords.x + 1)).color == Color.black)))
                    {
                        middleRow = check.coords.y + 2;
                        middleColumn = check.coords.x + 2;
                        DoubleJump(check, newRow, newColumn, middleRow, middleColumn);
                    }
                    //if theres a checker to in the negative x and positive y direction and the color is opposite the current checker, then add 2 to the y and subtract 2 from the x to get the middle position
                    else if ((check.color == Color.white) && ((SelectChecker((check.coords.y + 1), (check.coords.x - 1)).color == Color.black)))
                    {
                        middleRow = check.coords.y + 2;
                        middleColumn = check.coords.x - 2;
                        DoubleJump(check, newRow, newColumn, middleRow, middleColumn);
                    }                    
                    //if theres a checker to in the positive x and negative y direction and the color is opposite the current checker, then add 2 to the x and subtract 2 from the y to get the middle position
                    else if ((check.color == Color.black) && ((SelectChecker((check.coords.y - 1), (check.coords.x + 1)).color == Color.white)))
                    {
                        middleRow = check.coords.y - 2;
                        middleColumn = check.coords.x + 2;
                        DoubleJump(check, newRow, newColumn, middleRow, middleColumn);
                    } 
                    //if theres a checker to in the negative x and y direction and the color is opposite the current checker, then subtract 2 from the x and the y to get the middle position
                    else if ((check.color == Color.black) && ((SelectChecker((check.coords.y - 1), (check.coords.x - 1)).color == Color.white)))
                    {
                        middleRow = check.coords.y - 2;
                        middleColumn = check.coords.x - 2;
                        DoubleJump(check, newRow, newColumn, middleRow, middleColumn);
                    }
                    else
                    {
                        throw new Exception("not a valid double jump - no checker to jump in the first diagonal, forwards position");
                    }
                    //note y will always have a positive change when white is moving and a negative change when black is moving
                }
                else
                {
                    throw new Exception("not a valid double jump - ending position invalid");
                }
                //note: there will never be a double jump scenario in which the change in y is less than 4, unless backwards movements are allowed (it's possible that this occurs when you play with kings?)
            }

            else
            {
                throw new Exception("attempting something that has not been defined as valid");
            }
        }

        //double jump function: takes in starting checker, ending row, ending column, and middle row, middle column, and performs two jumps
        public void DoubleJump(Checker check, int newRow, int newColumn, int middleRow, int middleColumn)
        {
            //declares a "middle space" checker which should be null if the double jump is valid
            Checker middleSpace;
            middleSpace = SelectChecker(middleRow, middleColumn);
                    
            if (middleSpace == null)
                {
                    //perform first jump
                    Jump(check, middleRow, middleColumn);

                    //select the checker to perform the second jump with
                    Checker middleCheck;
                    middleCheck = SelectChecker(middleRow, middleColumn);

                    //perform second jump
                    Jump(middleCheck, newRow, newColumn);
                }
            else
                {
                    throw new Exception("not a valid double jump - middle space is not empty");
                }
        }
        //move function, takes in the starting checker, ending row and ending column, moves it physically on the grid, and updates it's coordinates in the list
        public void Move(Checker check, int newRow, int newColumn)
        {
            //updates the starting position on the grid to be empty
            Grid[check.coords.y][check.coords.x] = " ";

            //updates the starting checker's coordinates to the new row and new column
            check.coords = new Coordinates (newColumn, newRow);
            
            //calls the place checkers and draw board to update the physical board
            PlaceCheckers();
            DrawBoard(); 
        }
        //jump function, takes in starting checker, ending row and ending column, and performs a "jump" which entails a move and remove of the jumped checker
        public void Jump(Checker check, int newRow, int newColumn)
        {
            //declares the space and checker in between the starting position and ending position
            int inBetweemColumn;
            int inBetweenRow;
            Checker inBetweenCheck;

            //if the change in x is positive, the inbetween x will be starting x + 1
            if ((newColumn - check.coords.x) > 0)
            {
                inBetweemColumn = check.coords.x + 1;
                //if the change in y is positive, the inbetween y will be starting y + 1
                if ((newRow - check.coords.y) > 0)
                {
                    inBetweenRow = check.coords.y + 1;
                }
                //otherwise change in y is negative and the in between y will be starting y -1
                else
                {
                    inBetweenRow = check.coords.y - 1;
                }
            }

            //otherwise change in x is negative, the inbetween x will be starting x - 1
            else
            {
                inBetweemColumn = check.coords.x - 1;
                //if the change in y is positive, the inbetween y will be starting y + 1
                if ((newRow - check.coords.y) > 0)
                {
                    inBetweenRow = check.coords.y + 1;
                }
                //otherwise change in y is negative and the in between y will be starting y -1
                else
                {
                    inBetweenRow = check.coords.y - 1;
                }
            }
            //use the in between row and column to select the in between checker
            inBetweenCheck = SelectChecker(inBetweenRow, inBetweemColumn);

            //if there's a checker there and it's the opposite color of the starting checker, then remove it and move the starting checker to the ending coords
            if (inBetweenCheck != null)
            {
                if (inBetweenCheck.color != check.color)
                {
                    RemoveChecker(inBetweenRow, inBetweemColumn);
                    Move(check, newRow, newColumn);
                }
            }
            else
            {
                throw new Exception ("that is not a valid jump");
            }
        }
        
        //removeChecker function, takes in a row and column, and removes the checker at this position from the list, and updates the physical grid to a blank space at that position
        public void RemoveChecker(int row, int column)
        {
            Checkers.Remove(SelectChecker(row, column));
            Grid[row][column] = " ";
        }
        
        //check for win bool, returns true if the only checkers left are all of one color
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

            string playerTurn = "White";
            bool swap = true;

            while(board.CheckForWin() == false)
            {
                if (swap)
                {
                    playerTurn = (playerTurn == "White" ? "Black" : "White");   
                }

                Console.WriteLine("Player Turn: " + playerTurn);
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
                    swap = true;
                    board.MoveChecker(playerTurn, board.SelectChecker(row, column), newRow, newColumn);
                } 
                catch (Exception)
                {
                    Console.WriteLine(@"That's not a valid move. Please try again");
                    swap = false;
                }
            }
        //this will only trigger once check for win returns true and the game progresses out of the while loop
        Console.WriteLine($"Game over, {playerTurn} wins!");
        }
    }
}
