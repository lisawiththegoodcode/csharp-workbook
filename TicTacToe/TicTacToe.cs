using System;
using System.Threading;

namespace TicTacToe
{
    class Program
    {
        public static string playerTurn = "O"; //this is a static global variable
        public static string[][] board = new string[][]
        {
            new string[] {" ", " ", " "},
            new string[] {" ", " ", " "},
            new string[] {" ", " ", " "}
        };
        //initialize public variables to keep score
        public static int scoreX = 0;
        public static int scoreO = 0;

        public static void Main()
        {
            bool swap = true;
            do
            {
                if (swap)
                {
                    playerTurn = (playerTurn == "O") ? "X" : "O";
                    // rewriting this code with ternary operator above
                    // if (playerTurn == "O")
                    // {
                    //     playerTurn = "X";
                    // }
                    // else 
                    // {
                    //     playerTurn ="O";
                    // }
                }
                //if input was valid swap x be o if o be x;
                DrawBoard();
                swap = GetInput(); //get input from user, update the board
                //if input is valid swap if not valid don't swap, this can swap variable but you have to write your check for win and tie to check the opposite o for x win, x for o win, can make it return a variable, place mark would return t/f, then get input would return t/f, put it inside get input, and swap the logic on the check fors, or have it return something and then have it return t/f

            } while (!CheckForWin() && !CheckForTie());

            //if there's a win or a tie, lets determine if they want to play again or if they want to quit
            if (CheckForWin() || CheckForTie())
            {
                DrawBoard();
                GameLoop();
            }

            // leave this command at the end so your program does not close automatically
            Console.ReadLine();
        }
        public static void GameLoop()
        {
                if (CheckForWin())
                {
                    Print($"{playerTurn} wins! Well done!");
                    if (playerTurn == "X")
                    {
                        scoreX++;
                        Print("");
                        Print($"Current Score: Player X-{scoreX}, Player O-{scoreO}");
                        Print("");
                    }
                    else
                    {
                        scoreO++;
                        Print($"Current Score: Player X-{scoreX}, Player O-{scoreO}");
                    }
                }
                else 
                {
                    Print("It's a tie!");
                    Print($"Current Score: Player X-{scoreX}, Player O-{scoreO}");
                }
                Print("Would you like to play again?[y/n]");
                string answer = Console.ReadLine();
                if ((answer == "y") || (answer =="Y") || (answer =="Yes") || (answer =="YES") || (answer =="yes"))
                {
                    //if they want to play again, clear the board, draw the board and start get input function
                    Console.WriteLine("");
                    ClearBoard();
                    playerTurn = "O";
                    Main();
                } 
                else
                {
                    Print("Thank you for playing!");
                }

        }
        //creating method to clear board in between games
        public static void ClearBoard()
        {
            board[0][0] = " ";
            board[0][1] = " ";
            board[0][2] = " ";
            board[1][0] = " "; 
            board[2][0] = " ";
            board[1][2] = " ";
            board[1][1] = " ";
            board[2][1] = " ";
            board[2][2] = " ";                                                
        }
        //creating method to write print instead of console.writeline
        public static void Print(string s)
        {
        Console.WriteLine(s);
        }
        //get input gathers the position on the board to insert the player turn, calls the place mark function to place the player turn and returns t/f to the main function
        public static bool GetInput()
        {
            Console.WriteLine("Player " + playerTurn);
            Console.WriteLine("Enter Row:");
            int row = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Column:");
            int column = int.Parse(Console.ReadLine());
            return PlaceMark(row, column);
        }
        //update the board with the correct value at the correct position
        public static bool PlaceMark(int row, int column)
        {
            // Print("in place mark function");
            //must check that the input is valid first
            //takes input row and column, job is to update board if valid input and return true if if updated and false if not updated
            //Input is valid if 1. position is unoccupied, 2. position inputted is in bounds of board
            if ((row > -1) && (row < 3) && (column > -1) && (column < 3) && (board[row][column] == " "))
            {
                board[row][column] = playerTurn;
                // Print("returning true");
                return true;
            }
            else
            {
                Print("That is not a valid move. Try again!");
                Thread.Sleep(1000);
                return false;
            }
        }

        public static bool CheckForWin()
        {
            // your code goes here, calling the other three methods to see if there was a winner or not
            bool won = false;
            if(HorizontalWin() || VerticalWin() || DiagonalWin())
            {
                won = true;
            }
            return won;
        }

        public static bool CheckForTie()
        {
            // your code goes here
            bool tie = false;
            if((board[0][0] != " ") && (board[0][1] != " ") && (board[0][2] != " ") && (board[1][0] != " ") && (board[2][0] != " ") && (board[1][1] != " ") && (board[1][2] != " ") && (board[2][1] != " ") && (board[2][2] != " ") && !HorizontalWin() && !VerticalWin() && !DiagonalWin())
            {
                tie = true;
            }

            return tie;
        }
        
        public static bool HorizontalWin()
        {
            // your code goes here
            bool win = false;
            if(((board[0][0] != " ") && (board[0][0] == board[0][1]) && (board[0][0] == board [0][2])) || ((board[1][0] != " ") && (board[1][0] == board[1][1]) && (board[1][0] == board [1][2])) || ((board[2][0] != " ") && (board[2][0] == board[2][1]) && (board[2][0] == board [2][2])))
            {
                win = true;
            }

            return win;
        }

        public static bool VerticalWin()
        {
            // your code goes here
            bool win = false;
            if(((board[0][0] != " ") && (board[0][0] == board[1][0]) && (board[0][0] == board [2][0])) || ((board[0][1] != " ") && (board[0][1] == board[1][1]) && (board[0][1] == board [2][1])) || ((board[0][2] != " ") && (board[0][2] == board[1][2]) && (board[0][2] == board [2][2])))
            {
                win = true;
            }

            return win;
            
        }

        public static bool DiagonalWin()
        {
            bool win = false;
            if(((board[0][0] != " ") && (board[0][0] == board[1][1]) && (board[0][0] == board [2][2])) || ((board[0][2] != " ") && (board[0][2] == board[1][1]) && (board[0][2] == board [2][0])))
            {
                win = true;
            }

            return win;
        }

        public static void DrawBoard()
        {
            Console.WriteLine("  0 1 2");
            Console.WriteLine("0 " + String.Join("|", board[0]));
            Console.WriteLine("  -----");
            Console.WriteLine("1 " + String.Join("|", board[1]));
            Console.WriteLine("  -----");
            Console.WriteLine("2 " + String.Join("|", board[2]));
        }
    }
}
