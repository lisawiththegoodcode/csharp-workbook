using System;
using System.Collections;
using System.Collections.Generic;

namespace TowersOfHanoi
{
    class Program
    {
        static void Main(string[] args)
        {
            Block b1 = new Block(1);
            Block b2 = new Block(2);
            Block b3 = new Block(3);
            Block b4 = new Block(4);
            
            Stack towerA = new Stack();
            Stack towerB = new Stack();
            Stack towerC = new Stack();

            towerA.Push(b4);
            towerA.Push(b3);
            towerA.Push(b2);
            towerA.Push(b1);

            String a = "A: ";
            String b = "B: ";
            String c = "C: ";

            Dictionary<String,Stack> towers = new Dictionary<String,Stack>();

            towers.Add(a, towerA);
            towers.Add(b, towerB);
            towers.Add(c, towerC);

            Game game1 = new Game(towers);

            game1.printGameBoard();
            Console.WriteLine("");

            if (game1.isLegal("A", "B"))
            {
                game1.movePiece("A", "B");
            }
            else
            {
                Console.WriteLine("That's an illegal move.");
            }

            game1.printGameBoard();
            Console.WriteLine("");


            if (game1.isLegal("A", "C"))
            {
                game1.movePiece("A", "C");
            }
            else
            {
                Console.WriteLine("That's an illegal move.");
            }

            game1.printGameBoard();
            Console.WriteLine("");


            if (game1.isLegal("C", "B"))
            {
                game1.movePiece("C", "B");
            }
            else
            {
                Console.WriteLine("That's an illegal move.");
            }

            game1.printGameBoard();
            Console.WriteLine("");

        }
    }
    class Block
    {
        //properties
        public int weight {get;private set;}

        public Block(int weight)
        {
            this.weight = weight;
        }

    }
    class Tower
    {
        Stack blocks = new Stack();


    }
    class Game
    {
        Dictionary<String,Stack> towers = new Dictionary<String,Stack>();
        public Game (Dictionary<String,Stack> towers)
        {
            this.towers = towers;
        }

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

    }
}
