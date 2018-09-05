using System;
using System.IO;
using System.Collections.Generic;

namespace Files
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] wordsArray = File.ReadAllLines(@"//Users/lisa/Downloads/words.txt");

            Random rnd = new Random();
            int numGenerator = rnd.Next(0, wordsArray.Length);

            Console.WriteLine(wordsArray[numGenerator]);

            Console.WriteLine("I'm thinking of word. Can you guess what it is?");
        }
    }
}
