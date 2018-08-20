using System;
using System.Collections.Generic;

namespace Mastermind {
    class Program {
        //added code to randomly generate solution
        public static string[] letters = new string[] { "a", "b", "c", "d", "e", "f"};
        public static int codeSize = 4;
        public static string[] solution = new string[] {"a", "b", "c", "d"};

        public static void GenerateRandomCode() 
        {
            Random rnd = new Random();
            for(var i = 0; i < codeSize; i++)
            {
                solution[i] = letters[rnd.Next(0, letters.Length)];
            }
        }
        static void Main (string[] args) {
            //added code to pass maximum number of tries into game
            int maxTries = 10;
            //generates random solution; can comment this out to run with test solution "abcd"
            GenerateRandomCode();
            Game game = new Game (maxTries, solution);
            int turns;
            for (turns = maxTries; turns > 0; turns--)
            {
                Console.WriteLine($"You have {turns} tries left");
                Console.WriteLine ("Choose four letters: ");
                string letters = Console.ReadLine ();
                Ball[] balls = new Ball[4];

                for (int i = 0; i < 4; i++) 
                {
                    balls[i] = new Ball (letters[i].ToString());
                }
                Row row = new Row (balls);
                game.AddRow (row);
                
                //code added to tell user they won and end the game
                if (game.Rows == "win")
                {
                    Console.WriteLine(" - that's right!");
                    break;
                }
            }
            if (turns == 0)
            {
                Console.WriteLine ("Game Over: Out Of Turns");
            }
            else
            {
                Console.WriteLine ("Game Over: You Won");
            }
        }
    }

    class Game {
        private List<Row> rows = new List<Row> ();
        private string[] answer = new string[4];
        private int maxTries;

        public Game (int maxTries, string[] answer) {
            this.answer = answer;
            this.maxTries = maxTries;
        }

        private string Score (Row row) {
            string[] answerClone = (string[]) this.answer.Clone ();
            // red is correct letter and correct position
            // white is correct letters minus red
            // this.answer => ["a", "b", "c", "d"]
            // row.balls => [{ Letter: "c" }, { Letter: "b" }, { Letter: "d" }, { Letter: "a" }]
            int red = 0;
            string result = "";
            for (int i = 0; i < 4; i++) {
                if (answerClone[i] == row.balls[i].Letter) {
                    red++;
                }
            }

            int white = 0;
            for (int i = 0; i < 4; i++) {
                int foundIndex = Array.IndexOf (answerClone, row.balls[i].Letter);
                if (foundIndex > -1) {
                    white++;
                    answerClone[foundIndex] = null;
                }
            }
            
            result = $" {red} - {white - red}";

            return result;
        }

        public void AddRow (Row row) {
            this.rows.Add (row);
        }

        public string Rows {
            get {
                string result = "";

                foreach (var row in this.rows) {
                    Console.Write (row.Balls);
                    //code added to determine if there was a win
                    if ((Score(row)) == " 4 - 0")
                    {
                        result = "win";
                    }
                    else    
                    {
                        Console.WriteLine (Score (row));
                    }
                }
            return result;
            }
        }
    }

    class Ball {
        public string Letter { get; private set; }

        public Ball (string letter) {
            this.Letter = letter;
        }
    }

    class Row {
        public Ball[] balls = new Ball[4];

        public Row (Ball[] balls) {
            this.balls = balls;
        }

        public string Balls {
            get {
                foreach (var ball in this.balls) {
                    Console.Write (ball.Letter);
                }
                return "";
            }
        }
    }
}