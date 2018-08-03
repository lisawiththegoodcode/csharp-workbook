using System;

namespace _01Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello there! Have you ever wondered how many numbers between 1 and 100 are divisible by 3?");
            //get input, send it to a method that returns 0,1 or 2
            string input = Console.ReadLine().ToLower();
            int outputType = chooseYourOwnOutput(input);
            
            //use the number returned by the variable output to display different responses
            switch (outputType)
            {
                case 1:
                    Console.WriteLine(@"Really? Why? Well, here's the info you've been wondering about, ya weirdo!");
                    break;

                case 2:
                    Console.WriteLine(@"Yea me either. Here's the answer anyway.");
                    break;

                default:
                    Console.WriteLine(@"I don't understand, but that's ok, I'm just a computer. Here's the answer.");
                    break;
            }

            //calculate answer
            int answer = calculateAnswer();
            //display answer
            Console.WriteLine($"There are {answer} numbers between 1 and 100 that are divisible by 3. You're welcome.");
        }

        //takes input as a string, returns an int (1 for yes, 2 for no, otherwise 0)
        static int chooseYourOwnOutput(string input)
        {
            if (input == "yes" || input == "yea" || input == "sure")
            {
                return 1;
            } else if (input == "no" || input == "nope" || input == "never")
            {
                return 2;
            } else
            {
                return 0;
            }
        }

        //returns the answer to the question how many numbers between 1 and 100 are divisible by 3
        static int calculateAnswer()
        {
            int count = 0;
            for (int i=1; i<100; i++)
            {
                if (i%3 == 0)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
