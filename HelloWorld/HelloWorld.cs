using System;

namespace HelloWorld
{
    class Program
    {
        static void Main()
        {
		    string name = "";
            int favNum = 0;
            string favColor = "";

            Console.WriteLine("Please enter your name:");
            name = Console.ReadLine();
            Console.WriteLine("Please enter your favorite number:");
            favNum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter your favorite color:");
            favColor = Console.ReadLine();

            Console.WriteLine("Hello World! My name is {0}, my favorite number is {1}, and my favorite color is {2}.", name, favNum, favColor);
        }
    }
}
