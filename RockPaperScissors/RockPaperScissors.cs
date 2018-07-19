using System;

namespace RockPaperScissors
{
    class Program
    {
        public static void Scoreboard(int yourScore, int computerScore)
        {
            yourScore += yourScore;
            computerScore += computerScore; 
            Console.WriteLine($"Current Score: Player 1-{yourScore} / Computer-{computerScore}");
            GameLoop();

        }

        public static void Scorekeeper(int winner)
        {
            int score1 = 0;
            int score2 = 0;

            if (winner == 1)
            {
                score1++;
                Console.WriteLine("Score it now {0}", score1);

            }
            else if (winner == 2)
            {
                score2++;
                Console.WriteLine("Score it now {0}", score2);
            }
            Scoreboard(score1, score2);
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
        public static void CheckForWins(string hand1, string hand2)
        {
            int winner = 0;
            //compare the two hands
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
            Scorekeeper(winner);
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
            CheckForWins(hand1, hand2);
        }
        public static void Main()
        {

            //initalize scoreboard variables
            int yourScore = 0;
            int computerScore = 0;

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Welcome to Rock Paper Scissors!");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            GamePlay();
            Scoreboard(yourScore, computerScore);

            // leave this command at the end so your program does not close automatically
            // Console.ReadLine();
        }
    }
}
