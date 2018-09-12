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

            string theWord = wordsArray[numGenerator].ToLower();
            char[] theWordLetters = new char[theWord.Length];
            System.Console.WriteLine(theWord);

            Console.WriteLine("I'm thinking of word. Can you guess what it is?");
            string theGuess = Console.ReadLine().ToLower();
            char[] theGuessLetters = new char[theGuess.Length];

            using (StringReader sr1 = new StringReader(theWord))
            {
                sr1.Read(theWordLetters, 0, theWord.Length);
            }

            using (StringReader sr2 = new StringReader(theGuess))
            {
                sr2.Read(theGuessLetters, 0, theGuess.Length);
            }

            if (theGuess == theWord)
            {
                System.Console.WriteLine("That's right! You guessed it!");
            }
            else 
            {
                int counter = 0;
                //finding which word is shorter and setting the for loop's condition to the smaller of the two
                int shorterWord = (theGuessLetters.Length < theWordLetters.Length) ? theGuessLetters.Length : theWordLetters.Length;
                for (int i=0; i<shorterWord; i++)
                {
                    Console.WriteLine(theWordLetters[i]);                 
                    if ((theGuessLetters[i].CompareTo(theWordLetters[i])) > 0)
                    {
                        System.Console.WriteLine($"Your guess comes after the word");
                        break;
                    }
                    else if ((theGuessLetters[i].CompareTo(theWordLetters[i])) < 0)
                    {
                        System.Console.WriteLine("Your guess comes before the word");
                        break;
                    }
                    else if ((theGuessLetters[i].CompareTo(theWordLetters[i])) == 0)
                    {
                        counter++;
                        continue;
                    }              
                }
                //some logic in case that one of the words is a shorter version of the other word (i.e. tan and tangential)
                if (counter == shorterWord && shorterWord == theGuessLetters.Length)
                {
                    Console.WriteLine("Your guess comes before the word");
                }
                else if (counter == shorterWord && shorterWord == theWordLetters.Length)
                {
                    System.Console.WriteLine("Your guess comes after the word");
                }
            }

//commenting this out since I incorporated a for loop to compare more than just the first two letters of each word            
            // if ((theGuessLetters[0].CompareTo(theWordLetters[0])) > 0)
            // {
            //     System.Console.WriteLine($"Your guess comes after than the word");
            // }
            // else if ((theGuessLetters[0].CompareTo(theWordLetters[0])) < 0)
            // {
            //     System.Console.WriteLine($"Your guess comes before the word");
            // }
            // else if ((theGuessLetters[0].CompareTo(theWordLetters[0])) == 0)
            // {
            //     System.Console.WriteLine("we're going to need to compare more than just the first letter");
            // }
        }
    }
}
