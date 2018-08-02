using System;

namespace ExceptionHandling
{
    class Program
    {
        static int computerScore = 0;
        static int userScore = 0;

        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Welcome to Rock Paper Scissors!");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            do
            {
                bool run = true;
                string userHand = "";
                string computerHand = "";
                while (run)
                {
                    try
                    {
                        userHand = getUserInput();
                        run = false;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("That is not a valid entry. Please try again.");
                        userHand = "";
                        run = true;
                    }
                    finally
                    {
                        computerHand = generateAiInput(); 
                    }
                }
                Console.WriteLine($"Computer plays: {computerHand}"); 

                int winner = CompareHands(userHand, computerHand);
                if (winner ==1) 
                {
                    Console.WriteLine("You win!");
                    userScore++;
                } else if (winner == 2)
                {
                    Console.WriteLine("Computer wins!");
                    computerScore++;
                } else
                {
                    Console.WriteLine("It's a tie!");
                }
                Console.WriteLine($"Current Score: Your Score - {userScore} / Computer Score - {computerScore}");
            } while (PlayAgain());
           
            Console.WriteLine("Thank you for playing. Goodbye!");

        }
        
        static int CompareHands(String hand1, String hand2)
        {
            if (hand1 == hand2)
            {
                return 0;
            }
            else if (((hand1 == "rock" && hand2 == "scissors") || (hand1 == "paper" && hand2 == "rock") || (hand1 == "scissors" && hand2 == "paper")))
            {
                return 1;
            }
            else if (hand1 == "secretword")
            {
                return 1;
            }
            {
                return 2;
            }
        }

        //returns a hand for computer player
        static String generateAiInput()
        {
            Random generator = new Random();
            int randomNumber = generator.Next(0,3);
            if(randomNumber == 0) {
                return "paper";
            } else if (randomNumber ==1)
            {
                return "scissors";
            } else 
            {
                return "rock";
            }
        }

        static string getUserInput()
        {
            Console.WriteLine("Rock, Paper, or Scissors?");
            String hand = Console.ReadLine().Trim().ToLower();
            while(hand != "rock" && hand != "scissors" && hand != "paper")
            {
                if(hand == "secretword")
                {
                    break;
                }
                throw new Exception();   
            }
            return hand;
        }

        public static bool PlayAgain()
        {
            Console.WriteLine("Do you want to play again? [y/n]");
            string answer = Console.ReadLine();
            if ((answer == "y") || (answer =="Y") || (answer =="Yes") || (answer =="YES") || (answer =="yes"))
            {
                return true;
            } 
            else
            {
                return false;
            }
        }         
    }
}
