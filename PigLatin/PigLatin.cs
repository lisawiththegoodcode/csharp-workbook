using System;

namespace PigLatin
{
    class Program
    {
        public static void GameLoop()
        {
           Console.WriteLine();
           Console.WriteLine("Would you like to do another translation? [y/n]"); 
           string answer = Console.ReadLine();
           if ((answer == "y") || (answer =="Y") || (answer =="Yes") || (answer =="YES") || (answer =="yes"))
           {
            Console.WriteLine("");
            Console.WriteLine("Please enter the word or phrase that you'd like to tranlate:");
            string input = Console.ReadLine().ToUpper();
            Read(input);
           } 
           else
           {
                Console.WriteLine("ANKTHAY OUYAY ORFAY AYINGPLAY!!");
           }
        } 
        public static string ConsonantTranslator(string toTranslate, int vowelPosition)
        {
            // Console.WriteLine("In Consonant Translator with {0}, {1})", toTranslate, vowelPosition);
            string part1 = toTranslate.Substring(0, vowelPosition);
            string part2 = toTranslate.Substring(vowelPosition);
            string translatedWord = $"{part2}{part1}AY";
            return translatedWord;            
            // Console.WriteLine(translatedWord);
            // GameLoop();
        }
        public static string VowelTranslator(string toTranslate)
        {
            // Console.WriteLine("In vowel Translator");
            string translatedWord = $"{toTranslate}YAY";
            return translatedWord;
            // Console.WriteLine(translatedWord);
            // GameLoop();

        }
        public static string NoVowelTranslator(string toTranslate)
        {
            // Console.WriteLine("In no vowel Translator");
            string translatedWord = $"{toTranslate}AY";
            return translatedWord;
            // Console.WriteLine(translatedWord);
            // GameLoop();
        }
        // public static void Output(string )
        public static void Read(string input)
        {
            //initialize arrays
            string[] words = input.Split(' '); //break sentences into words
            string[] translation = new string [words.Length]; //initializes a translation array that will contain as many words as the input
            // Console.WriteLine("Here are my words:" + words.Length);

            for (int i = 0; i < words.Length; i++)
            {
                //initialize variables, distinguishing between vowels when they are first letter of the word or later in the word
                char[] firstLetterVowels = { 'A', 'E', 'I', 'O', 'U' };
                char[] vowels = { 'A', 'E', 'I', 'O', 'U', 'Y' };          
                string readThis = words[i];
                string word = "";    
                // Console.WriteLine("In for loop, here's my readThis:" + readThis);

                //begin if-else to determine type of word and send it to the appropriate converter
                //does the word start with vowel aeiou?
                if (readThis.IndexOfAny(firstLetterVowels) == 0)
                {
                    word = VowelTranslator(readThis);
                } 
                //if not, it must start with a consonant. But now I want to know, does it have vowel in it at all?
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
                    word = ConsonantTranslator(readThis, firstVowel);
                }  
                //does not contain a vowel, execute NoVowelTranslator
                else
                {
                    word = NoVowelTranslator(readThis);
                }

                //add translated word into the translation array
                translation[i] = word;
            }
            string output = String.Join(" ", translation);
            Console.WriteLine(output);
            GameLoop();
        }
        public static void Main()
        {
            Console.WriteLine("");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("PIG LATIN TRANSLATOR");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~");
            
            Console.WriteLine("");
            Console.WriteLine("Please enter the word or phrase you'd like to translate:");
            string input = Console.ReadLine().ToUpper();
            Read(input);
        }
        
    }
}
