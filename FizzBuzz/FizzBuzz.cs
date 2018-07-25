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
            ForEachLoop(countTo);
            WhileLoop(countTo);
            DoWhileLoop(countTo);
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

        public static void WhileLoop(int countTo)
        {
            int i = 1;
            while (i<=countTo)
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
                i++;
            }
        }
        public static void DoWhileLoop (int countTo)
        {
            int i = 1;
            do
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
                i++;
            } while (i<=countTo);
        }
        public static void ForEachLoop(int countTo)
        {
            int[] nums = new int[countTo];

            foreach (int i in nums)
            {
                nums[i] = i


            }
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
