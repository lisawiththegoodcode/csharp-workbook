using System;
using System.Threading;

namespace TextGame
{
    class Program
    {
        //begin the function that lays out the main options of the game 
        static void GamePlay()
        {
            Console.WriteLine("You enter a dark cavern out of curiosity. It is dark and you can only make out a small stick on the floor. Do you take it? [y/n]:");
            string ch1 = Console.ReadLine();
            //declare variables
            int stick = 0;
            int complete = 0; 
            //STICK TAKEN
            if ((ch1 == "y") || (ch1 =="Y") || (ch1 =="Yes") || (ch1 =="YES") || (ch1 =="yes"))
            {
                Console.WriteLine("You have taken the stick!");
                Thread.Sleep(2000);
                stick = 1;
            }
            else 
            {
                Console.WriteLine("You did not take the stick.");
                stick = 0;
            }
                
            Console.WriteLine("As you proceed further into the cave, you see a small glowing object. Do you approach the object? [y/n]");
            string ch2 = Console.ReadLine();
            
            //APPROACH SPIDER
            if ((ch2 == "y") || (ch2 =="Y") || (ch2 =="Yes") || (ch2 =="YES") || (ch2 =="yes"))
            {
            Console.WriteLine("You approach the object...");
            Thread.Sleep(2000);
            Console.WriteLine("As you draw closer, you begin to make out the object as an eye!");
            Thread.Sleep(1000);
            Console.WriteLine("The eye belongs to a giant spider! Do you try to fight it? [y/n]");
            string ch3 = Console.ReadLine();
                
                //FIGHT SPIDER
                if ((ch3 == "y") || (ch2 =="Y") || (ch2 =="Yes") || (ch2 =="YES") || (ch2 =="yes"))
                {   
                    //With Stick
                    if (stick == 1)
                    {
                        Console.WriteLine("You only have a stick to fight with!");
                        Console.WriteLine("You quickly jab the spider in it's eye and gain an advantage");
                        Thread.Sleep(2000);
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("                  Fighting...                   ");
                        Console.WriteLine("   YOU MUST HIT ABOVE A 5 TO KILL THE SPIDER    ");
                        Console.WriteLine("IF THE SPIDER HITS HIGHER THAN YOU, YOU WILL DIE");
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Thread.Sleep(2000);
                         //declare variables to generate random numbers for spider fight
                        Random rnd = new Random();
                        int fdmg1 = Convert.ToInt32(rnd.Next(3, 10));
                        int edmg1 = Convert.ToInt32(rnd.Next(1, 5));
                        Console.WriteLine("You hit " + fdmg1);
                        Console.WriteLine("The spider hits " + edmg1);
                        Thread.Sleep(2000);

                        //wrote seperate function, since the spider fight is a repeat action
                        SpiderFight(fdmg1, edmg1);
                    }
                    //WITHOUT STICK
                    else 
                    {
                        Console.WriteLine("You don't have anything to fight with!");
                        Thread.Sleep(2000);
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("                  Fighting...                   ");
                        Console.WriteLine("   YOU MUST HIT ABOVE A 5 TO KILL THE SPIDER    ");
                        Console.WriteLine("IF THE SPIDER HITS HIGHER THAN YOU, YOU WILL DIE");
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Thread.Sleep(2000);
                        //declare variables to generate random numbers for spider fight
                        Random rnd2 = new Random();
                        int fdmg2 = Convert.ToInt32(rnd2.Next(1, 8));
                        int edmg2 = Convert.ToInt32(rnd2.Next(1, 5));
                        Console.WriteLine("You hit " + fdmg2);
                        Console.WriteLine("The spider hits " + edmg2);
                        Thread.Sleep(2000);

                        //wrote seperate function, since the spider fight is a repeat action
                        SpiderFight(fdmg2, edmg2);
                    }

                }
                //DONT FIGHT SPIDER
                else 
                {
                    Console.WriteLine("You choose not to fight the spider.");
                    Thread.Sleep(1000);
                    Console.WriteLine("As you turn away, it ambushes you and impales you with it's fangs!!!");
                    complete = 0;
                    GameLoop(complete);
                }
            }
            //DONT APPROACH SPIDER
            else
            {
                Console.WriteLine("You turn away from the glowing object and attempt to leave the cave...");
                Thread.Sleep(1000);
                Console.WriteLine(@"But something won't let you.... IT'S A GIANT POISONOUS SPIDER! Heading your way fast!!");
                Thread.Sleep(1000);
                Console.WriteLine(@"You turn to run, but you're too late...");
                Thread.Sleep(2000);
                complete = 0;
                GameLoop(complete);
            }
        }

        //begin spider fight function
        static void SpiderFight(int player, int spider)
        {
            //declare variable
            int complete = 0;

            if (spider > player)
            {
                Console.WriteLine("The spider has dealt more damage than you!");
                Thread.Sleep(2000);                
                complete = 0;
                GameLoop(complete);
            } 
            else if (player < 5)
            {
                Console.WriteLine("You didn't do enough damage to kill the spider, but you managed to escape");
                Thread.Sleep(2000);                
                complete = 1;
                GameLoop(complete);
            }
            else
            {
                Console.WriteLine("You killed the spider!");
                Thread.Sleep(2000);
                complete = 1;
                GameLoop(complete);
            }
        }

        //begin game loop function
        static void GameLoop(int completion)
        {
            //declare alive variable
            bool alive = true;

            if ((completion == 1) && (alive == true))
            {
                Console.WriteLine("You escaped the cavern alive! Would you like to play again? [y/n]");
                string ch3 = Console.ReadLine();
                if ((ch3 == "y") || (ch3 =="Y") || (ch3 =="Yes") || (ch3 =="YES") || (ch3 =="yes"))
                {
                alive = true;
                GamePlay();
                }   
                else
                {
                Console.WriteLine("You are now leaving the cavern of secrets! Goodbye!");
                alive = false;
                }

            }
            else
            {
                Console.WriteLine("You have died! Would you like to play again? [y/n]");
                string ch3 = Console.ReadLine();
                if ((ch3 == "y") || (ch3 =="Y") || (ch3 =="Yes") || (ch3 =="YES") || (ch3 =="yes"))
                {
                alive = true;
                GamePlay();
                }
                else
                {
                Console.WriteLine("You are now leaving the cavern of secrets! Goodbye!");
                alive = false;
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Welcome to the cavern of secrets!");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            Thread.Sleep(3000);

            GamePlay();            
        }
    }
}
