using System;

namespace _04Program
{
    class Program
    {
        static void Main(string[] args)
        {
            //get a random number between 1 and 10. 
            Console.WriteLine("I'm thinking of a number between 1 and 10...");
            int number = getNumber();

            //Give the user 4 chances to guess the number, takes the computer generated number with it to checks for a win, and returns number of attempts it took to complete the guessing loop.
            int attempts = 4 - (guessingLoop(number));
            
            //If the guessingloop function returned with a value greater than 0, the loop was exited with a win. 0 can only be acheived by a loss.
            if (attempts == 0)
            {
                Console.WriteLine($"You lost. The number was {number}. Sorry, better luck next time.");
            }
            else 
            {
                Console.WriteLine($"You won! Good job!! It took you {attempts} attempts to guess my number.");
            }
        }
        //begins with the computer number and 4 attempts, checks for a match between computer number and input. Returns number of attempts left, or 0 if no match occured.
        static int guessingLoop(int number)
        {
            int attempts = 4; 
            while (attempts > 0)
            {
                Console.WriteLine($"You have {attempts} chance(s) to guess my number. Please enter your guess now:");
                int guess = 0;

                try
                {
                    guess = Int32.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Eek! You didn't enter a valid integer, so that was kinda total waste of a turn...");
                    continue;
                }

                if (number == guess)
                {
                    break;
                } 
                
                attempts--;

                if (attempts > 0)
                {
                    Console.WriteLine("Nope, that's not it! Please try again.");
                }
            }
            return attempts;
        }
        //randomly generates a number between 1 and 10 and returns it to main
        static int getNumber()
        {
            Random rnd = new Random();
            //assuming that the instructions "between 1 and 10" mean >1 and <10
            int numGen = rnd.Next(2, 10);
            return numGen;
        }
    }
}
