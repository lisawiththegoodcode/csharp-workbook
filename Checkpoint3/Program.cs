using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Checkpoint3
{
    class Program
    {
        static void Main(string[] args)
        {
            //create the context
            Context todoContext = new Context();
            //keep this here while working on the program, this will allow a new db structure to start fresh each time we run a test
            // todoContext.Database.EnsureDeleted();
            //create the table
            todoContext.Database.EnsureCreated();

            //begin user interaction
            System.Console.WriteLine("-----------------------");
            System.Console.WriteLine("Welcome to My ToDo App!");
            System.Console.WriteLine("-----------------------");
                        
            //the default state of the program will be listing all todos currently incomplete, this will list nothing first time the app runs
            Utils.ListIncomplete(todoContext);

            //take in a command from the user based on a menu of commands
            bool needsCommand = true; 
            int input = 0;
            while (needsCommand == true)
            {
                Utils.OptionsMenu();
                //Parse the input into an int, throw an exception if can be parsed but isn't 1-7
                try
                {
                    input = Int32.Parse(Console.ReadLine());

                    if (input == 1)
                    {
                        Utils.AddToDo(todoContext);
                        Utils.ListIncomplete(todoContext);
                        needsCommand = true;
                    }
                    else if (input == 2)
                    {

                    }
                    else if (input == 3)
                    {

                    }
                    else if (input == 4)
                    {

                    }
                    else if (input == 5)
                    {

                    }
                    else if (input == 6)
                    {

                    }
                    else if (input == 7)
                    {
                        System.Console.WriteLine("Thank you for using My ToDo App. See you next time!");
                        break;
                    }
                    // if (input > 0 && input < 8)
                    // {
                    //     needsCommand = false;
                    // }
                    else
                    {
                        throw new Exception("the int entered is not an int 1-7");
                    }
                }
                catch (Exception)
                {
                    needsCommand = true;
                    System.Console.WriteLine("That is not a valid entry. Please try again.");
                }
            }
            


            //testing items to figure out how it works
            // Item test = new Item("hello");
            // test.status = Status.complete;
            // todoContext.todos.Add(test);
            // todoContext.todos.Add(new Item("my first todo"));
            // todoContext.SaveChanges();


        
            // // //testing printing to the console
            // var results = from t in todoContext.todos
            // select t;

            // foreach(Item item in results)
            // {
            //     System.Console.WriteLine(item.description);
            //     System.Console.WriteLine(item.status);
            //     System.Console.WriteLine(item.id);
            // }

            //App starts - what happens?
            //Incomplete ToDos populate
            //User gets asked: "What do you want to do? Add a todo, update a todo, mark a todo complete, delete a todo, list todos already completed, list all todo history
            
        }
    }
    //if i have time, i should explore how a user could create their own type
    public enum Type {general, personal, work, school}
    public enum Status {incomplete, complete}
    //not set bc if not set it will default at position
    public class Item {
        public int id {get;set;}
        public string description {get;set;}
        public Type type {get; set;}
        public Status status {get; set;}
        public DateTime deadline {get; set;}

        //need an empty constructor to work with Entity Framework
        public Item(){}
        
        //the only property required to make a todo is the description
        public Item(string description)
        {
            this.description = description;
        }

    }
    public static class Utils
    {
        //put methods to interact with the user here going to be static methods
        
        //method ListIncomplete takes in todoContext from main and prints out any todos that need completion
        public static void ListIncomplete(Context todoContext)
        {
            var results = from t in todoContext.todos where (t.status == Status.incomplete)
            select t;
            
            //Will display the following message if there are no incomplete todos
            if (results.Count() == 0)
            {
                System.Console.WriteLine("You currently have nothing ToDo.");
            }
            //if there are incomplete todos, list them
            else
            {
                System.Console.WriteLine("My ToDos:");

                foreach(Item item in results)
                {
                    System.Console.WriteLine(item.id + " | " + item.description);
                }
            }
        }
        //method OptionsMenu simply prints the menu of options for user to select from
        public static void OptionsMenu()
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Please select from the following menu:");
            System.Console.WriteLine("Enter 1 to add a new ToDo");
            System.Console.WriteLine("Enter 2 to update an existing ToDo");
            System.Console.WriteLine("Enter 3 to mark a ToDo complete");
            System.Console.WriteLine("Enter 4 to delete an existing ToDo");
            System.Console.WriteLine("Enter 5 to view your previously completed ToDos");
            System.Console.WriteLine("Enter 6 to view your entire history of ToDos");
            System.Console.WriteLine("Enter 7 to quit");
        }
        //method AddToDo takes in todo context and uses this context to add a new to do to the list
        public static void AddToDo(Context todoContext)
        {
            System.Console.WriteLine("Please enter your new ToDo:");
            string description = Console.ReadLine();

            todoContext.todos.Add(new Item(description));
            todoContext.SaveChanges();

            System.Console.WriteLine("Your ToDo has been added. Thank you!");
            System.Console.WriteLine("");
        }
    }

    public class Context:DbContext
    {
        public DbSet<Item> todos {get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //link to the database 
            optionsBuilder.UseSqlite("Filename=./todoapp.db");
        }
    }
}
