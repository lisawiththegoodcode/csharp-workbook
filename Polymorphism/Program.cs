using System;
using System.Collections.Generic;

namespace Polymorphism
{
    abstract class Triangle
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }

        public int Height { get; set; }
        public int Base { get; set; }

        public Triangle(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    
        public virtual double Area()
        {
            Console.WriteLine("Triangle class area method called");
            return (Height * Base)/2.0;

        }
        //all calls to perimeter should come to base class as there is no perimeter method in derived classes
        public int Perimeter()
        {
            return x + y + z;
        }  

        public virtual string Name()
        {
            return "im a triangle";
        }
    }
    class Equilateral : Triangle
    {
        public int side;
        public Equilateral(int side) : base (side, side, side)
        {
            this.side = side;

        }
        
        public override double Area() //override means the derived class method should be called over the base class method no matter what
        {
            return (Math.Sqrt(3)/4)*side*side;
        }

        public override string Name() //override means the derived class method should be called over the base class method no matter what
        {
            return "im an equalateral";
        }

    }

    class Isosceles : Triangle
    {
        public int side;

        public Isosceles(int s, int b) : base (s, s, b)
        {
            this.Base = b;
            this.side = s;
        }

        public new string Name() //new keyword means base class should be called if the object is instantiated as type base
        {
            return "im an isosceles";
        }

        public new double Area() //new keyword means base class should be called if the object is instantiated as type base
        {
            return .5*Base*(Math.Sqrt((Math.Pow(side, 2)) - (Math.Pow((Base/2), 2))));
        }

    }
    class Scalene : Triangle
    {
        public Scalene(int a, int b, int c) : base (a, b, c)
        {

        }

        public override string Name() //override means the derived class method should be called over the base class method no matter what
        {
            return "im a scalene";
        }

    }    
    class Program
    {
        static void Main(string[] args)
        {
            //test case one - Equilateral object of type Equilateral (equilateral methods written with "override")
            Equilateral e1 = new Equilateral(3);
            Console.WriteLine(e1.Name());
            Console.WriteLine("my perimeter is: " + e1.Perimeter());
            Console.WriteLine("my area is: " + e1.Area());
            Console.WriteLine("");

            //test case two - Equilateral object of type Triangle (equilateral methods written with "override")
            Triangle e2 = new Equilateral(3);
            Console.WriteLine(e2.Name());
            Console.WriteLine("my perimeter is: " + e2.Perimeter());
            e2.Height = 10; //bc area method in triangle class needs height defined to calculate area, let's throw garbage to see how it handles it
            Console.WriteLine("my area is: " + e2.Area());
            Console.WriteLine("");

            //test case three - Isosceles object of type Triangle (Isosceles methods written with "new")
            Triangle i1 = new Isosceles(5,6);
            Console.WriteLine(i1.Name());
            Console.WriteLine("my perimeter is: " + i1.Perimeter());
            i1.Height = 4; //bc area method in triangle class needs height defined to calc area
            Console.WriteLine("my area is: " + i1.Area());
            Console.WriteLine("");

            //test case four - Isosceles object of type Isosceles (Isosceles methods written with "new")
            Isosceles i2 = new Isosceles(5,6);
            Console.WriteLine(i2.Name());
            Console.WriteLine("my perimeter is: " + i2.Perimeter());
            Console.WriteLine("my area is: " + i2.Area());
            Console.WriteLine("");

            //test case five - Scalene object of type scalene(Name method uses "override" but there is no area method and no height given)
            Scalene s1 = new Scalene(3,4,5);
            Console.WriteLine(s1.Name());
            Console.WriteLine("my perimeter is: " + s1.Perimeter());
            Console.WriteLine("my area is: " + s1.Area());
            Console.WriteLine("");
        }
    }

}





// namespace Polymorphism
// {
//     abstract class Vehicle
//     {
//         // public virtual string honk()
//         // {
//         //     return "honk";
//         // }
//         public abstract string honk();
//     }
//     class Sedan : Vehicle
//     {
//         public new string honk() //this will be used only if the variable type is sedan
//         {
//             return "beep beep";
//         }

//     }
//     class Truck : Vehicle
//     {
//         public override string honk() //this will be used no matter what variable type; vehicle or truck
//         {
//             return "HONK";
//         }

//         public string dump()
//         {
//             return "dumping";
//         }

//     }
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             Vehicle v1 = new Vehicle(); //makes a new vehicle
//             Vehicle s1 = new Sedan(); //makes a new sedan with sedan constructor
//             Vehicle s2 = new Sedan(); //makes new vehicle with sedan constructor
//             Vehicle t1 = new Truck(); //makes a new truck

//             //it's ok to make a list of type base that contains the base class's derived objects, but not vice versa
//             List<Vehicle> listOfVehicles = new List<Vehicle>();
//             listOfVehicles.Add(v1);
//             listOfVehicles.Add(s1);
//             listOfVehicles.Add(s2);
//             listOfVehicles.Add(t1);

//             Console.WriteLine("my mazda: " + s1.honk());
//             Console.WriteLine("lisa prius: " + s2.honk());

//             foreach(Vehicle v in listOfVehicles)
//             {
//                 if (v is Truck) //the is operator checking what kind of instance it is
//                 {
//                     Truck theTruck = (Truck)v; //casting it's type vehicle but it's an instance of a truck, basically asking it if it's a truck
//                     Console.WriteLine(theTruck.dump());
//                 }
//                 else {
//                     Console.WriteLine("is not a truck");
//                 }
//             }


//         }
//     }
// }
