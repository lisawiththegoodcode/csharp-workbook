using System;

namespace Inheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            //employee objects
            HourlyEmployee lisa = new HourlyEmployee(22.50, 82, 22222, "lisa", "umhey");
            SalariedEmployee yousif = new SalariedEmployee(9000000, 33333, "yousif", "seedhom");
            
            //employee paychecks
            lisa.printPaycheck();
            yousif.printPaycheck();
        }

        //parent class
        public abstract class Employee
        {
            public int id;
            public string firstName;
            public string lastName;
            public string streetAddress;
            public string city;
            public string state;
            public int zipCode;
            public int phoneNumber; 
            static bool isSalaried; 

            public Employee(int id, string firstName, string lastName)
            {
                this.id = id;
                this.firstName = firstName;
                this.lastName = lastName;
            }

            //method to create a full name from first and last name 
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
            
            //method to print full name and paycheck amount
            public void printPaycheck()
            {
                Console.WriteLine(FullName + ": " + calculatePaycheck());
            }

            //abstract calculatePaycheck method, defers to the calculatePaycheck methods in the child classes when called
            public abstract double calculatePaycheck();

        }

        //child class of employee, an employee who makes a salary
        public class SalariedEmployee : Employee
        {
            //field
            public double annualSalary;

            //constructor
            public SalariedEmployee (int annualSalary, int id, string firstName, string lastName) : base (id, firstName, lastName)
            {
                this.annualSalary = annualSalary;
            }

            //method
            public override double calculatePaycheck()
            {
                return annualSalary / 26.0;
            }

        }

        //child class of employee, an employee who earns an hourly wage
        public class HourlyEmployee : Employee
        {
            //fields
            public double rate;
            public double hours;

            //constructor
            public HourlyEmployee (double rate, double hours, int id, string firstName, string lastName) : base (id, firstName, lastName)
            {
                this.rate = rate;
                this.hours = hours;
            }

            //method
            public override double calculatePaycheck()
            {
                double amountOwed = 0;
                
                if (hours <=80)
                {
                    amountOwed = rate * hours;
                }
                else
                {
                    amountOwed = (rate * 80) + (rate * (hours-80));;
                }

                return amountOwed;
            }
        }
    }
}

