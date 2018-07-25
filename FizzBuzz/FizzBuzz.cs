using System;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to FizzBuzz! To what number shall we count?");
            int countTo = Int32.Parse(Console.ReadLine());
            ForLoop(countTo);
        }
        static void ForLoop(int countTo)
        {
            for (int i = 1; i <= countTo; i++)
            {
                if (i%15 == 0)
                {
                    Console.WriteLine("fizzbuzz");
                } 
                else if (i%3 == 0)
                {
                    Console.WriteLine("fizz");
                } 
                else if (i%5 ==0)
                {
                    Console.WriteLine("buzz");
                } 
                else
                {
                    Console.WriteLine(i);
                }
            }
        }

    }
}
