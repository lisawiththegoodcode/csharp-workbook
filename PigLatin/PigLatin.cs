using System;
using System.Threading;

namespace PigLatin
{
    class Program
    {
        public static void GameLoop()
        {
           Console.WriteLine();
           Console.WriteLine("Would you like to enter another word? [y/n]"); 
           string answer = Console.ReadLine();
           if ((answer == "y") || (answer =="Y") || (answer =="Yes") || (answer =="YES") || (answer =="yes"))
           {
                Console.WriteLine("");
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
        public static void ConsonantTranslator(string toTranslate, int vowelPosition)
        {
            // Console.WriteLine("In Consonant Translator with {0}, {1})", toTranslate, vowelPosition);
            string part1 = toTranslate.Substring(0, vowelPosition);
            string part2 = toTranslate.Substring(vowelPosition);
            string translatedWord = $"{part2}{part1}AY";
            Console.WriteLine(translatedWord);
            GameLoop();
        }
        public static void VowelTranslator(string toTranslate)
        {
            // Console.WriteLine("In vowel Translator");
            string translatedWord = $"{toTranslate}YAY";
            Console.WriteLine(translatedWord);
            GameLoop();

        }
        public static void NoVowelTranslator(string toTranslate)
        {
            // Console.WriteLine("In no vowel Translator");
            string translatedWord = $"{toTranslate}AY";
            Console.WriteLine(translatedWord);
            GameLoop();
        }
        public static void ReadWord(string readThis)
        {
            //declare variables, distinguishing between vowels when they are first letter of the word or later in the word
            char[] firstLetterVowels = { 'A', 'E', 'I', 'O', 'U' };
            char[] vowels = { 'A', 'E', 'I', 'O', 'U', 'Y' };

            //begin if-else to determine type of word: 1. starts with vowel, 2. starts with consonant, 3. does not contain a vowel
            if (readThis.IndexOfAny(firstLetterVowels) == 0)
            {
                VowelTranslator(readThis);
            } 
            else if (readThis.IndexOfAny(vowels) != -1)
            {
                //return the position of the first vowel and trigger the translator
                int firstVowel;
                if (readThis.IndexOfAny(vowels) == 0)
                {
                    //if the first letter is a y, then it's not actually a vowel, so chop it off and try again to find the real first vowel
                    string yAsFirst = readThis.Substring(0,1);
                    string next = readThis.Substring(1);
                    firstVowel = (next.IndexOfAny(vowels)) + 1;
                }
                else
                {
                    firstVowel = readThis.IndexOfAny(vowels);
                }
                ConsonantTranslator(readThis, firstVowel);
            }  
            else 
            {
                NoVowelTranslator(readThis);
            }

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
            Console.WriteLine("");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("PIG LATIN TRANSLATOR");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~");
            Thread.Sleep(1000);
            
            Console.WriteLine("");
            Console.WriteLine("Please enter the word that you would like to translate to pig latin:");
            string input = Console.ReadLine();
            string word = input.ToUpper();
            ReadWord(word);
        }
        
    }
}
