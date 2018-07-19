using System;

namespace RockPaperScissors
{
    class Program
    {
        public static void GameLoop(int winner, int yourScore, int computerScore)
        {
            Console.WriteLine("Do you want to play again? [y/n]");
            string answer = Console.ReadLine();
            if ((answer == "y") || (answer =="Y") || (answer =="Yes") || (answer =="YES") || (answer =="yes"))
            {
                GamePlay(winner, yourScore, computerScore);
            } 
            else
            {
                 Console.WriteLine("Thank you for playing. Goodbye!");
            }
        } 
        public static void GamePlay(int winner, int yourScore, int computerScore)
        {
            Console.WriteLine();
            Console.WriteLine("Enter hand for Player 1: [rock/paper/scissors]");
            string hand1 = Console.ReadLine().ToLower();
            Console.WriteLine();
            Console.WriteLine("Computer plays:");
            
            //randomly generate and output rock paper or scisoors
            Random rnd = new Random();
            int handGen = rnd.Next(0,3);
            string hand2 = "";
            if (handGen == 0)
            {
                hand2 = "rock";
            }
            else if (handGen == 1)
            {
                hand2 = "paper";
            }
            else 
            {
                hand2 = "scissors";
            }
            Console.WriteLine(hand2);
            Console.WriteLine();

            //compares hands to check for wins
            if ((hand1 == "rock" && hand2 == "scissors") || (hand1 == "paper" && hand2 == "rock") || (hand1 == "scissors" && hand2 == "paper"))
            {
                winner = 1;
                Console.WriteLine($"Player {winner} wins!");

            }
            else if (hand1 == hand2)
            {
                winner = 0;
                Console.WriteLine("It's a draw!");
            }
            else if (((hand2 == "rock" && hand1 == "scissors") || (hand2 == "paper" && hand1 == "rock") || (hand2 == "scissors" && hand1 == "paper")))
            {
                winner = 2;
                Console.WriteLine("Computer wins!");
            } 
            else    
            {
                winner = 0;
                Console.WriteLine("You did not enter a valid hand. Valid hands include rock, paper, or scissors. Please try again.");
            }

            //increases score depending on winner
            if (winner == 1)
            {
                yourScore++;
            }
            else if (winner == 2)
            {
                computerScore++;
            }

            //displays updated scoreboard
            Console.WriteLine($"Current Score: Player 1-{yourScore} / Computer-{computerScore}");
            Console.WriteLine();


            //trigger loop to determine if player would like to continue playing game
            GameLoop(winner, yourScore, computerScore);
        }
        public static void Main()
        {

            //initalize variables
            int yourScore = 0;
            int computerScore = 0;
            int winner = 0;

            Console.WriteLine();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Welcome to Rock Paper Scissors!");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            GamePlay(winner, yourScore, computerScore);

            // leave this command at the end so your program does not close automatically
            // Console.ReadLine();
        }
    }
}
