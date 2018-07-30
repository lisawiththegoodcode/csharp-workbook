using System;

namespace FizzBuzz
{
    class Program
    {
        static void Main()
        {
            //declare variables
            int total;
            string direction;
            string loopType;
            int start;
            int end;
            int increment;
            bool forward;

            //prompt user for inputs to initialize total number to count to and direction to count
            Begin:
            Console.WriteLine("Welcome to FizzBuzz! To what number shall we count?");
            total = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Ok. And which direction would you like to count? [forwards/backwards]");
            direction = Console.ReadLine().ToLower();

            //conditional to initialize forward boolean
            if (direction == "forwards" || direction == "forward")
            {
                forward = true;
            } 
            else if (direction == "backwards" || direction == "backward")
            {
                forward = false;
            }
            else
            {
                Console.WriteLine("That is not a valid direction. Please try again.");
                goto Begin;   
            }

            //initialize start, end and increment variables based on direction boolean
            start = forward ? 1 : total;
			end = forward ? total : 1;
			increment = forward ? 1 : -1;
        
            //prompt user for input to initialize loopType to use             
            Console.WriteLine("Great, let's do it! Which loop type would you like to use? [for/foreach/while/do]");
            loopType = Console.ReadLine().ToLower();
            Console.WriteLine("");

            //conditional to employ proper loop type and print fizzbuzz output 
            if (loopType == "for")
            {
                Console.WriteLine("For Loop Solution:");
                for (int i = start; ConditionGenerator(forward, i, end); i += increment)
                {
                    FizzBuzzer(i);
                }
            }
            else if (loopType == "foreach")
            {
                Console.WriteLine("For Each Loop Solution:");
                
                int[] nums = new int[total];
                nums[0] = start;

                for (int i = 1; i<total; i++)
                {
                    nums[i] = forward ? (i + 1) : (total-i);
                }   

                foreach (int num in nums)
                {
                    FizzBuzzer(num);
                }
            }
            else if (loopType == "while")
            {
            Console.WriteLine("While Loop Solution:");
                int i = start;
                while (ConditionGenerator(forward, i, end))
                {
                    FizzBuzzer(i);
                    i+=increment;
                }
            }
            else if (loopType == "do")
            {
                Console.WriteLine("Do While Loop Solution:");
                int i = start;
                do
                {
                    FizzBuzzer(i);
                    i+=increment;
                } while (ConditionGenerator(forward, i, end));            
            }
            else 
            {
                Console.WriteLine("That is not a valid loop type. Please try again.");
                goto Begin;
            }

        }
        //this function will produce the proper condition check for the loops, whether the condition shold be <= (in the case of forwards) or >= (in the case of backwards)
        static bool ConditionGenerator(bool forward, int i, int end)
        {
            if  (forward)
			{
				return i <= end;
			}
			else
			{
				return i >= end;
			}
            
        }

        //this function will produce the proper fizzbuzz output (fizz for divisible by 3, buzz for divisible by 5, fizzbuzz if divisible by both, or the number if divisible by neither)
        static void FizzBuzzer(int i)
        {
            string output = "";            
            if (i%3 == 0)
            {
                output +="fizz";
            } 
            if (i%5 ==0)
            {
                output +="buzz";
            }
            if (i%3!=0 && i%5!=0)
            {
                output = $"{i}";
            }
            Console.WriteLine(output);        
        }

    }
}
