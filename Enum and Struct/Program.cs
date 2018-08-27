using System;

namespace Enum_and_Struct
{
    class Program
    {
        public struct Birthday
        {
            public string month;
            public int day; 

            public Birthday(string month, int day)
            {
                this.month = month;
                this.day = day;
            }
        }
        public enum Month 
        {
            January = 1, February = 2, March = 3, April = 4, May = 5, June = 6, July = 7, August = 8, September = 9, October = 10, November = 11, December = 12
        }    


        static void Main(string[] args)
        {
            Console.WriteLine("I'm a smartie pants computer, and I know what day of the week your birthday will fall on no matter what the year! What month were you born?");
            string month = Console.ReadLine();

            Console.WriteLine("Great. And what day of the month were you born?");
            int day = Convert.ToInt32(Console.ReadLine());

            Birthday myBday = new Birthday(month, day);
            
            Console.WriteLine("Fantastic. What year would you like to find out the day of the week your birthday will fall on?");
            FindDayOfTheWeek(myBday);

            Console.WriteLine("Would you like to check another year? [y/n]");
            while (Console.ReadLine().ToLower().Equals("y"))
            {
                Console.WriteLine("Ok. What year would you like to check?");
                FindDayOfTheWeek(myBday);
                Console.WriteLine("Would you like to check another year? [y/n]");
            }
        }

        static void FindDayOfTheWeek(Birthday myBday)
        {
            int year = Convert.ToInt32(Console.ReadLine());

            DateTime dt = DateTime.Parse(myBday.month
             + " " + myBday.day + " " + year);
            int compare = DateTime.Compare(dt, DateTime.Now);

            if (compare == 0)
            {
                Console.WriteLine("Your birthday is today!");

            } else if (compare > 0)
            {
            Console.WriteLine("Your birthday will be on a " + dt.DayOfWeek + "!");
            } else if (compare < 0)
            {
                Console.WriteLine("Your birthday was on a " + dt.DayOfWeek + "!");
            } else
            {
                Console.WriteLine("You did not enter a valid birthday.");
            }

        }

    }
}

//NOTES FROM CLASS
//         static void Main(string[] args)
//         {
//             Days today = Days.Saturday;

//             Console.WriteLine("Sunday is " + ((int)Days.Sunday));
//             Console.WriteLine("If today is {0}, tomorrow will be {1}", today, getNextDay(today));

//         }

//             public static Days getNextDay(Days day)
//             {
//                 return (Days)(((int)day+1) % 7);
//             }
//             public static bool isWeekend(Days day)
//             {
//                 if (day == Days.Saturday || day == Days.Sunday)
//                 {
//                     return true;
//                 }
//                 else
//                 {
//                     return false;
//                 }
//             }
//     }
    

//     public enum Days 
//     {
//         Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday
//     }
// }
