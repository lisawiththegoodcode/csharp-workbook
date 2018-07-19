using System;

namespace RockPaperScissors
{
    class Program
    {
        public static void Scoreboard(int winner, int yourScore, int computerScore)
        {

            if (winner == 1)
            {
                yourScore++;
            }
            else if (winner == 2)
            {
                computerScore++;
            }
            Console.WriteLine($"Current Score: Player 1-{yourScore} / Computer-{computerScore}");

        }
        public static void GameLoop()
        {
            Console.WriteLine("Do you want to play again? [y/n]");
            string answer = Console.ReadLine();
            if ((answer == "y") || (answer =="Y") || (answer =="Yes") || (answer =="YES") || (answer =="yes"))
            {
                GamePlay();
            } 
            else
            {
                 Console.WriteLine("Thank you for playing!");
            }
        } 
        public static void CompareHands(int winner, string hand1, string hand2)
        {
            if ((hand1 == "rock" && hand2 == "scissors") || (hand1 == "paper" && hand2 == "rock") || (hand1 == "scissors" && hand2 == "paper"))
            {
                winner = 1;
                Console.WriteLine($"Player {winner} wins!");

            }
            else if (hand1 == hand2)
            {
                Console.WriteLine("It's a draw!");
            }
            else if (((hand2 == "rock" && hand1 == "scissors") || (hand2 == "paper" && hand1 == "rock") || (hand2 == "scissors" && hand1 == "paper")))
            {
                winner = 2;
                Console.WriteLine("Computer wins!");
            } 
            else    
            {
                Console.WriteLine("You did not enter a valid hand, please try again.");
            }
            GameLoop();
            // Scoreboard();
        }
        public static void GamePlay()
        {
            Console.WriteLine("Enter hand for player 1: [rock/paper/scissors]");
            string hand1 = Console.ReadLine().ToLower();
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

            // string hand2 = Console.ReadLine().ToLower();

            //compare the two hands
            CompareHands(hand1, hand2);
            // Console.WriteLine(CompareHands(hand1, hand2));
        }
        public static void Main()
        {

            //initalize scoreboard variables
            int yourScore = 0;
            int computerScore = 0;
            int winner = 0;

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Welcome to Rock Paper Scissors!");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            GamePlay();

            Scoreboard(winner, yourScore, computerScore);

            // leave this command at the end so your program does not close automatically
            // Console.ReadLine();
        }
    }
}
