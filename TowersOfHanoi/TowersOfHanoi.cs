using System;
using System.Collections;
using System.Collections.Generic;
//this project took me the majority of a day to complete... is that normal?
namespace TowersOfHanoi
{
    class Program
    {
        static void Main(string[] args)
        {
            //get input for the number of blocks the user would like to play the game with
            Console.WriteLine("Welcome to Towers of Hanoi! How many blocks would you like to play with?");
            int numOfBlocks = 0;
            bool parsed = Int32.TryParse(Console.ReadLine(), out numOfBlocks);

            //if the number of blocks is not a valid int, print error message and end game
            if (!parsed)
            {
                Console.WriteLine("That is not a valid number. Goodbye.");
            }  

            //instantiate a new game with the number of blocks from the user

            Game g1 = new Game(numOfBlocks);

            //call the create blocks method in the game class to create the blocks for the initial gameboard
            g1.createBlocks();
            //call the create towers method in the game class to create the towers for the initial gameboard
            g1.createTowers();

            //now that we have blocks and towers created, we can print the game board
            Console.WriteLine("Great! Here's your starting game board.");
            Console.WriteLine("");
            g1.printGameBoard();
            Console.WriteLine("");

            //while loop to check for a win between each move
            while (!g1.checkForWin())
            {
                Console.WriteLine("You may move the topmost block from any tower. Which tower would you like to move from?[A/B/C]");
                string fromTower = Console.ReadLine().ToUpper();
                Console.WriteLine("Got it. And to which tower would you like to move?[A/B/C]");
                string toTower = Console.ReadLine().ToUpper();

                //if the move is legal, move the piece, else let them know they can't move
                if (g1.isLegal(fromTower, toTower))
                {
                    Console.WriteLine("Great! Here's your updated game board.");
                    Console.WriteLine("");
                    g1.movePiece(fromTower, toTower);
                }
                else
                {
                    Console.WriteLine("That's an illegal move - you can't place a bigger block on a smaller block. Try again.");
                    Console.WriteLine("");
                }
                //print the game board and loop again if no win
                g1.printGameBoard();
                Console.WriteLine("");
            }
        }
    }
    class Block
    {
        //weight property for block
        public int weight {get;private set;}
        //constructor
        public Block(int weight)
        {
            this.weight = weight;
        }

    }

    //TALK TO YOUSIF ABOUT THIS: the initial instructions said to make a tower class but I didn't end up using it in my implementation

    // class Tower
    // {
    //     public Stack blocks = new Stack();

    //     public Tower(Stack blocks)
    //     {
    //         this.blocks = blocks;

    //     }
    // }
    class Game
    {
        //initialize fields of the game class
        Dictionary<String,Stack> towers = new Dictionary<String,Stack>(); //maps label to corresponding stack of blocks
        int numOfBlocks; //one property that the game is instantiated with
        Stack towerA = new Stack(); //for blocks stacked in towerA
        Stack towerB = new Stack(); //for blocks stacked in towerB
        Stack towerC = new Stack(); //for blocks stacked in towerC

        //constructor
        public Game (int numOfBlocks)
        {
            // this.towers = towers;
            this.numOfBlocks = numOfBlocks;
        }

        //method to add the initial number of blocks the game is instantiated with to tower A; only called once at beginning of game
        public void createBlocks()
        {
            int i = numOfBlocks;
            while (i>0)
            {
                Block b = new Block(i);
                towerA.Push(b);
                i--;
            }
        }

        //method to add initial string and stack to towers dictionary; only called once at beginning of game
        public void createTowers()
        {
            String a = "A: ";
            String b = "B: ";
            String c = "C: ";

            towers.Add(a, towerA);
            towers.Add(b, towerB);
            towers.Add(c, towerC);
        }

        //prints the info stored in the towers dictionary
        public void printGameBoard()
        {
            foreach (KeyValuePair<String, Stack> tower in towers)
            {
                string blocks = "";
                foreach(Block block in tower.Value)
                {
                    blocks += block.weight.ToString();
                }
                Console.WriteLine(tower.Key + blocks);
            }
        }

        //manipulates the stacks contained in the towers dictionary
        public void movePiece(string fromTower, string toTower)
        {
            char[] charsToTrim = { ':', ' '};
            Stack toStack = new Stack();
            Stack fromStack = new Stack();

            foreach (KeyValuePair<String, Stack> tower in towers)
            {
                if (tower.Key.Trim(charsToTrim) == fromTower)
                {
                    fromStack = tower.Value;
                }
                if (tower.Key.Trim(charsToTrim) == toTower)
                {
                    toStack = tower.Value; 
                }
            }

            toStack.Push(fromStack.Pop());
        }

        //checks that the block's weight that is being moved is less than the current block at the top of the stack; also allows the move if the receiving stack is currently null
        public bool isLegal(string fromTower, string toTower)
        {
            Stack value;
            int toWeight = 0;
            int fromWeight = 0;

            if (towers.TryGetValue($"{toTower}: ", out value))
            {
                try
                {
                    Block block = (Block)value.Peek();
                    toWeight = block.weight;
                    // Console.WriteLine(toWeight);
                }
                //this is to catch null 
                catch (Exception)
                {
                    return true;
                }
            }
            if (towers.TryGetValue($"{fromTower}: ", out value))
            {
                Block block = (Block)value.Peek();
                fromWeight = block.weight;
                // Console.WriteLine(fromWeight);
            }
            if (fromWeight<toWeight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //returns true if win and therefore breaks the game loop in main
        public bool checkForWin()
        {
            Stack value = towers["C: "];
            if (value.Count == numOfBlocks)
            {
                Console.WriteLine("You win!!");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
