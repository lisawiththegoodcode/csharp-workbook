using System;

namespace PigLatin
{
    class Program
    {
        public static void Main()
        {
            // your code goes here
            Console.WriteLine("What word would you like to translate to Pig Latin?");
            ReadWord(word);
            // leave this command at the end so your program does not close automatically
            string word = Console.ReadLine();
        }
        
        public static void ReadWord(string word)
        {
            for (int i = 0; i <= word.Length; i++)
            {
                if word[i] == 'a';
                {
                    return i;
                }
                else if word[i] == 'e';

                //index of any is doing two loops inside of another
            }
        }
        public static string TranslateWord(string word)
        {
            // your code goes here
            return word;
        }
    }
}
