using System;
using System.Collections;


namespace _02Program
{
    class Program
    {
        static void Main(string[] args)
        {
            //this program will continuously ask the user to enter a number or "ok" to exit. Then it will calculate the sum of all the previously entered numbers and display it on the console.
            Stack numbers = getNumbers();
            int total = getTotal(numbers);
            //display the answer
            Console.WriteLine($"The sum of all the integers you entered is {total}. Thank you and goodbye.");
        }

        //loop to get input and store input in a stack
        //when user says ok exit the loop
        //return the stack        
        static Stack getNumbers()
        {
            Stack inputs = new Stack();
            Stack nums = new Stack();
            int num;
            string input = "";
            do 
            {
                Console.WriteLine(@"Please enter an integer. Say 'ok' when you are done.");
                input = Console.ReadLine();
                inputs.Push(input);
            } while (input != "ok"); 

            foreach (string item in inputs)
            {
                bool isInt = Int32.TryParse(item, out num);
                if (isInt)
                {
                nums.Push(num);
                } 
                else
                {
                    if (item !="ok")
                    {
                        string invalids = item + " ";
                        Console.WriteLine($"The following input were not entered as valid integers and will be excluded from the sum: {invalids}");
                    }
                }
            }
            return nums;
        }
        //get all the numbers out of the stack and find the total
        static int getTotal(Stack numbers)
        {
            int sum = 0;
            foreach (int num in numbers)
            {
                // int i = Int32.Parse(num);
                sum += num;
            }
            return sum;
        }
    }
}
