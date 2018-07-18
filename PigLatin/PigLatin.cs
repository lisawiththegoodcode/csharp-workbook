using System;
using System.Threading;

namespace PigLatin
{
    class Program
    {
        public static void GameLoop()
        {
           Console.WriteLine("Would you like to enter another word? [y/n]"); 
           string answer = Console.ReadLine();
           if ((answer == "y") || (answer =="Y") || (answer =="Yes") || (answer =="YES") || (answer =="yes"))
           {
                Console.WriteLine("Please enter the word that you would like to translate to pig latin:");
                string input = Console.ReadLine();
                string word = input.ToUpper();
                ReadWord(word);
           } 
           else
           {
                Console.WriteLine("ANKTHAY OUYAY ORFAY AYINGPLAY!!");
           }
        } 
        public static void VowelTranslator(string toTranslate)
        {
            string translatedWord = $"{toTranslate}YAY";
            Console.WriteLine(translatedWord);
            GameLoop();

        }
        public static void NoVowelTranslator(string toTranslate)
        {
            string translatedWord = $"{toTranslate}AY";
            Console.WriteLine(translatedWord);
            GameLoop();
        }
        public static void ReadWord(string readThis)
        {
            //declare variables, distinguishing between vowels when they are first letter of the word or later in the word
            char[] firstLetterVowels = { 'A', 'E', 'I', 'O', 'U' };
            char[] vowels = { 'A', 'E', 'I', 'O', 'U', 'Y' };

            //begin if-else to determine type of word 1. starts with vowel, 2. starts with consonant, 3. does not contain a vowel
            if (readThis.IndexOfAny(firstLetterVowels) == 0)
            {
                VowelTranslator(readThis);
            } 
            else if (readThis.IndexOfAny(vowels) != -1)
            {
                Console.WriteLine(readThis.IndexOfAny(vowels));
                GameLoop();

            //return the position of the first vowel and trigger the translator
            }  
            else 
            {
                NoVowelTranslator(readThis);
            }
            //case 1. does it start with a vowel (aeiou)?
            //then vowel translator (keep + yay)
            //case 2. does it contain a vowel (aeiouy)?
            //then consonant translator (move substring preceding vowel to end + ay)
            //case 3. does it not contain a vowel (keep + ay)


            // for (int i = 0; i <= word.Length; i++)
            // {
            //     if word[i] == 'a';
            //     {
            //         return i;
            //     }
            //     else if word[i] == 'e';

                //index of any is doing two loops inside of another
            
        }
        public static void Main()
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("PIG LATIN TRANSLATOR");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~");
            Thread.Sleep(1000);
            
            Console.WriteLine("Please enter the word that you would like to translate to pig latin:");
            string input = Console.ReadLine();
            string word = input.ToUpper();
            ReadWord(word);
        }
        
    }
}
