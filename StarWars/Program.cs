using System;

namespace StarWars
{
    class Program
    {
        public static void Main()
        {
            //instantiating objects
            Person leia = new Person("Leia", "Organa", "Rebel");
            Person darth = new Person("Darth", "Vader", "Imperial");
            Person han = new Person("Han", "Solo", "Rebel");
            Person luke = new Person("Luke", "Skywalker", "Rebel");
            Person emporer = new Person("Emporer", "Palpatine", "Imperial");
            Ship falcon = new Ship("Rebel", "Smuggling", 2);
            Ship tie = new Ship("Tie", "Fighter", 1);
            Ship xWing = new Ship("Rebel", "wing", 8);
            Station rebelStation = new Station(10);
            Station deathStar = new Station(100);

            //complete step one of the project - give a name to the ship
            xWing.Name = "X-Wing";
            tie.Name = "Tie Fighter";
            falcon.Name = "Millennium Falcon";

            //demonstrating completion of step two of project  
            falcon.EnterShip(luke, 0);
            falcon.EnterShip(emporer, 1);
            tie.EnterShip(leia,0);
            xWing.EnterShip(darth, 0);
            rebelStation.dockShip(xWing, 0);
            rebelStation.dockShip(falcon, 1);
            deathStar.dockShip(tie, 10); 
            rebelStation.unDockShip(0);
            Console.WriteLine("Here are the passengers on the falcon: ");
            Console.WriteLine(falcon.Passengers);
            Console.WriteLine("Here are the ships docked at the rebel station: ");
            rebelStation.printRoster();
        }
    }
    //person class, which consists of a first name, last name, alliance 
    class Person
    {
        private string firstName;
        private string lastName;
        private string alliance;

        //constructor
        public Person(string firstName, string lastName, string alliance)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.alliance = alliance;
        }
        //method to turn first and last into a full name, or set a first name and last name based on a full name
        public string FullName
        {
            get
            {
                return this.firstName + " " + this.lastName;
            }

            set
            {
                string[] names = value.Split(' ');
                this.firstName = names[0];
                this.lastName = names[1];
            }
        }
    }

    //ship class made up of a person array, name, type, alliance and passengers
    class Ship
    {
        //a person object, which is used statically by the ship
        private Person[] passengers;
        //name of the ship
        public string Name {get;set;}
        
        //constructor
        public Ship(string alliance, string type, int size)
        {
            this.Type = type;
            this.Alliance = alliance;
            this.passengers = new Person[size];
        }

        public string Type
        {
            get;
            set;
        }

        public string Alliance
        {
            get;
            set;
        }   
        //method printing out the name of each person in the person array
        public string Passengers
        {
            get
            {
                foreach (var person in passengers)
                {
                    Console.WriteLine(String.Format("{0}", person.FullName));
                }

                return "That's Everybody!";
            }
        }

        public void EnterShip(Person person, int seat)
        {
            this.passengers[seat] = person;
        }

        public void ExitShip(int seat)
        {
            this.passengers[seat] = null;
        }
    }

    //station class consisting of a name, alliace, capacity and ship array
    class Station
    {
        public string Name {get;set;}
        public string Alliance {get;set;}
        public int capacity {get;private set;}
        public Ship[] ships;

        //constructor
        public Station(int capacity)
        {
            this.capacity = capacity; 
            this.ships = new Ship[capacity];
        }
        //method to dock a ship in a spot in the station
        public void dockShip(Ship shipToBeDocked, int position)
        {
            if (this.ships[position] == null)
            {
                this.ships[position] = shipToBeDocked;
                Console.WriteLine($"{shipToBeDocked.Name} ship has been docked.");
            }
            else
            {
                throw new Exception("position is taken.");
            }
        }
        //method to vacate spot in the station
        public void unDockShip(int position)
        {
            this.ships[position] = null;
        }

        public void printRoster()
        {
            foreach (var ship in ships)
            {
                try
                {
                    Console.WriteLine(String.Format("The {0}, which contains the following passengers: ", ship.Name));
                    Console.WriteLine(ship.Passengers);
                }
                catch(Exception)
                {
                    continue;
                }
            }
        }


    }
}
