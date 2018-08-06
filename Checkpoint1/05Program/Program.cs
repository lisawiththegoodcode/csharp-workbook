using System;
using System.Linq;

namespace _05Program
{
    class Program
    {
        static void Main(string[] args)
        {
            //ask the user to enter a series of numbers separated by comma. 
            Console.WriteLine("Please enter a series of integers seperated by comma.");
            
            //declare a string for user input
            string input = Console.ReadLine();

            //send string to a function to return an array of integers
            int[] intArray = getIntArray(input);

            //sent the array of ints to a function to return the max value

            int maxValue = getMaxValue(intArray);
            //print the max
            Console.WriteLine("The largest integer you have inputted is: " + maxValue);

        }
        //takes int Array and finds largest value
        static int getMaxValue(int[] intArray)
        {
            //initialize max value to be the first value in the array
            int maxValue = intArray[0];

            for (int i=0; i<(intArray.Length-1); i++)
            {
                int current = intArray[i];
                int next = intArray[i+1];
                int larger;

                if (current > next)
                {
                    larger = current;
                } 
                else
                {
                    larger = next;
                }

                if (larger > maxValue)
                {
                    maxValue = larger;
                }
            }
            return maxValue;
        }
        //takes the user input, converts it to a string array, and returns it as an int array
        static int[] getIntArray(string input)
        {
            string[] inputs = input.Split(",");
            int[] intArray = new int[inputs.Length];
            int i=0;

            foreach (string num in inputs)
            {
                try
                {
                    int number = Int32.Parse(num.Trim());
                    intArray[i] = number;
                    i++;
                }
                catch (Exception)
                {
                    Console.WriteLine($"{num} is not a valid integer. This entry will be excluded from the data set.");
                    continue;
                }
                
            }
            return intArray;
        }
    }
}
