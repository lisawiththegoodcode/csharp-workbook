using System;
using System.Collections.Generic;

namespace Quiz
{
    public interface IRentable
    {
        double CalculateRent(double days);

        string ItemType();
    }

    class Car : IRentable
    {
        public double hourlyRent {get; private set;}

        public Car(double hourlyRent)
        {
            this.hourlyRent = hourlyRent;
        }

        public double CalculateRent (double days)
        {
            double hours = days*24;
            return hours*hourlyRent;
        }    

        public string ItemType()
        {
            return "this is a car";
        }
    }

    class HotelRoom : IRentable
    {   
        public double nightlyRent {get; private set;}

        public HotelRoom(double nightlyRent)
        {
            this.nightlyRent = nightlyRent;
        }

        public double CalculateRent (double days)
        {
            return days*nightlyRent;
        }

        public string ItemType()
        {
            return "this is a hotel room";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Car mercedes = new Car (10.50);
            Car jeep = new Car (4.99);
            Car mazda = new Car (1.50);

            HotelRoom kingSuite = new HotelRoom (499.99);
            HotelRoom queenSuite = new HotelRoom (399.99);
            HotelRoom standardRoom = new HotelRoom (299.99);
            HotelRoom budgetRoom = new HotelRoom (199.99);

            // Console.WriteLine("5 days for a jeep: " + jeep.CalculateRent(5));
            // Console.WriteLine("5 days for a mercedes: " + mercedes.CalculateRent(5));
            // Console.WriteLine("5 days for a mazda3: " + mazda.CalculateRent(5));

            // Console.WriteLine("3 days for a kingSuite: " + kingSuite.CalculateRent(3));
            // Console.WriteLine("3 days for a queenSuite: " + queenSuite.CalculateRent(3));
            // Console.WriteLine("3 days for a standard: " + standardRoom.CalculateRent(3));
            // Console.WriteLine("3 days for a budget: " + budgetRoom.CalculateRent(3));

            List<IRentable> things = new List<IRentable>();
            things.Add(mercedes);
            things.Add(kingSuite);
            things.Add(mazda);
            things.Add(jeep);
            things.Add(queenSuite);
            things.Add(standardRoom);
            things.Add(budgetRoom);

            foreach (IRentable thing in things){
                Console.WriteLine("{0}, and the amount due for 1 day of rent is {1}", thing.ItemType(), thing.CalculateRent(1)); // ItemType() returns the thing the object is constructed as
                Console.WriteLine("{0}, and the amount due for 1 day of rent is {1}", thing.GetType().Name, thing.CalculateRent(1));

        }
    }
}

}

