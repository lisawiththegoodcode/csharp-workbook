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

        public static string TranslatorPrep(string readThis)
        {
            //initialize variables, with two vowel char arrays to distinguish between when they are first letter of the word or later in the word
            string word = "";
            char[] firstLetterVowels = { 'A', 'E', 'I', 'O', 'U' };
            char[] vowels = { 'A', 'E', 'I', 'O', 'U', 'Y' };     
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
            return word;
        }
        // public static void Output(string)
        public static void Read(string input)
        {
            //initialize input and output arrays, string variables for each individual words
            string[] words = input.Split(' '); //break sentences into words
            string[] translation = new string [words.Length]; //initializes a translation array that will contain as many words as the input
            string word = "";

            //begin iteration through each word
            for (int i = 0; i < words.Length; i++)
            {
                string readThis = words[i];
                string keepThisStart = "";
                string keepThisEnd = "";
                // Console.WriteLine("In for loop, here's my readThis:" + readThis);
                
                //iterate through characters of each word to search for punctuation
                var chars = readThis.ToCharArray();
                char[] punctuation = {'.', ',', '\'', '\"', '?', '!'};
                for (int y = 0; y < chars.Length; y++)
                {
                    //if there is punctuation, figure out where it appears in the string
                    if (Char.IsPunctuation(chars[y]))
                    {
                        // Console.WriteLine($"there's punctuation at position {y}!");
                        //if it's at the beginning, remove it before sending it to the translator and keep it to add back to the beginning of the word after it gets translated
                        if (y == 0)
                        {
                            // Console.WriteLine("and it's at the beginning of a string");
                            readThis = readThis.TrimStart(punctuation);
                            keepThisStart = (chars[y]).ToString();
                            // Console.WriteLine($"new word: {readThis}, punctuation: {keepThisStart}");
                        } 

                        //if it's at the end, remove it before sending it to the translator and keep it to add back to the end of the word after it gets translated
                        if (y == ((chars.Length) - 1))
                        {
                            // Console.WriteLine("and it's at the end of the string");
                            readThis = readThis.TrimEnd(punctuation);
                            keepThisEnd = (chars[y]).ToString();
                            // Console.WriteLine($"new word: {readThis}, punctuation: {keepThisEnd}");

                        }

                        //if it's in the middle I suppose we should just remove it? ex that's would be atsthay not at'sthay
                        if (y != 0 && (y != (chars.Length - 1)))
                        {
                            int position = y;
                            readThis = readThis.Remove(y,1);
                            // Console.WriteLine("and it's somewhere in the middle of the string");
                            
                        }
                    }
                    //if we have something to add to the start or end of this word, translate it first then add it back in
                    if (keepThisStart != "" || keepThisEnd != "")
                    {
                        //prepend/append any punctuation that was on the front or back of the word
                        word = TranslatorPrep (readThis);
                        word = keepThisStart + word + keepThisEnd;
                    }
                    //otherwise do go through the translation process for the word
                    else
                        word = TranslatorPrep (readThis);
                }         
                //add each translated word into the translation array
                translation[i] = word;
            }
            //join the translation array and pring the result to the screen
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
