using System;

namespace _03Program
{
    class Program
    {
        static void Main(string[] args)
        {
            //ask the user to enter a number. 
            Console.WriteLine("Please enter the integer you would like to factorialize:");

            int tryCount = 2;

            while (tryCount >= 0)
            {
                try
                {
                    int input = Int32.Parse(Console.ReadLine());
                    //Compute the factorial of the number 
                    double factorial = getFactorial(input);
                     //print it on the console. 
                    Console.WriteLine($"{input}! = {factorial}");
                }
                catch (Exception)
                {
                    if (tryCount > 0)
                    {
                        Console.WriteLine($"You did not enter a valid integer. You have {tryCount} more attempt(s). Please enter a valid integer.");
                    }
                    else
                    {
                        Console.WriteLine("You have failed to enter a valid integer. You are now locked out of this game. Goodbye.");
                    }
                    tryCount--;
                    continue;
                }
                break;
            }
        }
        //takes the integer from the user and returns a double that is the product of the inputted integer and every integer between it and 0 
        static double getFactorial(int input)
        {
            double answer = 1;
            for (double next = Convert.ToDouble(input); next > 0; next--)
            {
                answer *= next;
                Console.WriteLine("answer currently" + answer);
            }
            return answer;
        }
    }
}
