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
            todoContext.Database.EnsureDeleted();
            //create the table
            todoContext.Database.EnsureCreated();

            //begin user interaction
            System.Console.WriteLine("-----------------------");
            System.Console.WriteLine("Welcome to My ToDo App!");
            System.Console.WriteLine("-----------------------");
                        
            //instantiating variables for input loop 
            bool needsCommand = true; 
            int input = 0;

            while (needsCommand == true)
            {
                //the default state of the program will be listing all todos currently incomplete, this will list nothing first time the app runs
                Utils.ListIncomplete(todoContext);
                //give a menu of commands, take in an integer command
                Utils.OptionsMenu();
                //Parse the input into an int, throw an exception if can be parsed but isn't 1-7
                try
                {
                    input = Int32.Parse(Console.ReadLine());

                    if (input == 1)
                    {
                        Utils.AddToDo(todoContext);
                        needsCommand = true;
                    }
                    else if (input == 2)
                    {
                        Utils.UpdateTodo(todoContext);
                        needsCommand = true;
                    }
                    else if (input == 3)
                    {
                        Utils.MarkComplete(todoContext);
                        needsCommand = true;
                    }
                    else if (input == 4)
                    {
                        Utils.Delete(todoContext);
                        needsCommand = true;
                    }
                    else if (input == 5)
                    {
                        Utils.ListComplete(todoContext);
                        needsCommand = true;
                    }
                    else if (input == 6)
                    {
                        Utils.ListAll(todoContext);
                        needsCommand = true;
                    }
                    else if (input == 7)
                    {
                        System.Console.WriteLine("Thank you for using My ToDo App. See you next time!");
                        needsCommand = false;
                    }
                    else
                    {
                        throw new Exception("the int entered is not an int 1-7");
                    }
                }
                catch (Exception)
                {
                    System.Console.WriteLine("That is not a valid entry. Please try again.");
                    needsCommand = true;
                }
            }            
        }
    }

    //default position for an new item will be general. if i have time, i should explore how a user could create their own type
    public enum Type {general, personal, work, school}
    //default position for a new item will be incomplete
    public enum Status {incomplete, complete}

    //begin item class
    public class Item 
    {
        public int id {get;set;}
        public int displayID {get;set;}
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

    //begin utils class, put methods to interact with the user here going to be static methods
    public static class Utils
    {        
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
                
                int i = 0;
                foreach(Item item in results)
                {
                    i++;
                    item.displayID = i;

                    if (item.deadline == DateTime.MinValue)
                    {
                        System.Console.WriteLine(item.displayID + " | " + item.description + " | " + item.type + " | no deadline");
                    }
                    else
                    {
                        System.Console.WriteLine(item.displayID + " | " + item.description + " | " + item.type + " | " + item.deadline);
                    }
                    todoContext.SaveChanges();
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
            System.Console.WriteLine("Enter 5 to view previously completed ToDos");
            System.Console.WriteLine("Enter 6 to view your ToDos, both incomplete and previously completed ");
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

        public static void UpdateTodo(Context todoContext)
        {
            System.Console.WriteLine("Please enter the number of the ToDo item you would like to update:");
            //declare variables
            int id;
            bool needsId = true;

            while (needsId == true)
            {
                try
                {
                    id = Int32.Parse(Console.ReadLine());

                    var match = from t in todoContext.todos where (t.displayID == id)
                    select t;

                    if (match.Count() == 1) //a number that matches an existing todo
                    {
                        foreach (Item item in match)
                        {
                            System.Console.WriteLine("The ToDo item you would like to update is: " + item.description);
                            System.Console.WriteLine("Is that correct? Enter 1 for yes and 2 for no.");
                            if (Int32.Parse(Console.ReadLine()) == 1)
                            {
                                System.Console.WriteLine("Great! What would you like to change this ToDo to?");
                                string newDescription = Console.ReadLine();
                                item.description = newDescription;
                                todoContext.SaveChanges();

                                System.Console.WriteLine("Your ToDo has been updated. Thank you!");
                                System.Console.WriteLine("");
                                needsId = false;
                            }
                            else if (Int32.Parse(Console.ReadLine()) == 2)
                            {
                                System.Console.WriteLine("Sorry about that! Let's try again. Please enter the number to the left of the ToDo item you'd like to update?");
                                needsId = true;
                            }
                            else
                            {
                                throw new Exception("Invalid Entry: 1 or 2 expected");
                            }
                        }
                    }
                    else if (match.Count() > 0)
                    {
                        throw new Exception("the number entered returned multiple id matches");
                    }
                    else
                    {
                        throw new Exception("the number entered does not match the ids of any todos currently stored in the database");
                    }
                }
                catch (Exception)
                {
                    System.Console.WriteLine("That is not a valid entry. Please try again.");
                    needsId = true;
                }              
            }
        }

        public static void MarkComplete(Context todoContext)
        {
            System.Console.WriteLine("Please enter the number of the ToDo item you would like to mark complete:");
            //declare variables
            int id;
            bool needsId = true;

            while (needsId == true)
            {
                try
                {
                    id = Int32.Parse(Console.ReadLine());

                    var match = from t in todoContext.todos where (t.displayID == id)
                    select t;

                    if (match.Count() == 1) //a number that matches an existing todo
                    {
                        foreach (Item item in match)
                        {
                            System.Console.WriteLine("The ToDo item you would like to mark complete is: " + item.description);
                            System.Console.WriteLine("Is that correct? Enter 1 for yes and 2 for no.");

                            try
                            {
                                int response = Int32.Parse(Console.ReadLine());
                                if (response == 1)
                                {
                                    item.status = Status.complete;
                                    item.displayID = 0;
                                    todoContext.SaveChanges();

                                    System.Console.WriteLine("Your ToDo has been marked complete. Thank you!");
                                    System.Console.WriteLine("");
                                    needsId = false;
                                }
                                else if (response == 2)
                                {
                                    System.Console.WriteLine("Sorry about that! Let's try again."); 
                                    System.Console.WriteLine("Please enter the number to the left of the ToDo item you'd like to mark complete or enter 'M' to go back to the options menu.");
                                    string input = Console.ReadLine().ToLower();
                                    if (input == "m")
                                    {
                                        needsId = false;
                                    }
                                    else
                                    {
                                        needsId = true;
                                    }
                                }
                                else
                                {
                                    throw new Exception("Invalid Entry: 1 or 2 expected");
                                }
                            }
                            catch
                            {
                                System.Console.WriteLine("That is not a valid entry. Please try again.");
                                needsId = false;
                            }
                        }
                    }
                    else if (match.Count() > 0)
                    {
                        throw new Exception("the number entered returned multiple id matches");
                    }
                    else
                    {
                        throw new Exception("the number entered does not match the ids of any todos currently stored in the database");
                    }
                }
                catch (Exception)
                {
                    System.Console.WriteLine("That is not a valid entry. Please try again.");
                    needsId = true;
                }              
            }
        }

        public static void Delete(Context todoContext)
        {
            System.Console.WriteLine("Please enter the number of the ToDo item you would like to delete:");
            //declare variables
            int id;
            bool needsId = true;

            while (needsId == true)
            {
                try
                {
                    id = Int32.Parse(Console.ReadLine());

                    var match = from t in todoContext.todos where (t.displayID == id)
                    select t;

                    if (match.Count() == 1) //a number that matches an existing todo
                    {
                        foreach (Item item in match)
                        {
                            System.Console.WriteLine("The ToDo item you would like to delete is: " + item.description);
                            System.Console.WriteLine("Is that correct? Enter 1 for yes and 2 for no.");

                            try
                            {
                                int response = Int32.Parse(Console.ReadLine());
                                if (response == 1)
                                {
                                    // item.displayID = 0;
                                    todoContext.todos.Remove(item);
                                    todoContext.SaveChanges();

                                    System.Console.WriteLine("Your ToDo has been deleted. Thank you!");
                                    System.Console.WriteLine("");
                                    needsId = false;
                                }
                                else if (response == 2)
                                {
                                    System.Console.WriteLine("Sorry about that! Let's try again."); 
                                    System.Console.WriteLine("Please enter the number to the left of the ToDo item you'd like to delete or enter 'M' to go back to the options menu.");
                                    string input = Console.ReadLine().ToLower();
                                    if (input == "m")
                                    {
                                        needsId = false;
                                    }
                                    else
                                    {
                                        needsId = true;
                                    }
                                }
                                else
                                {
                                    throw new Exception("Invalid Entry: 1 or 2 expected");
                                }
                            }
                            catch
                            {
                                System.Console.WriteLine("That is not a valid entry. Please try again.");
                                needsId = false;
                            }
                        }
                    }
                    else if (match.Count() > 0)
                    {
                        throw new Exception("the number entered returned multiple id matches");
                    }
                    else
                    {
                        throw new Exception("the number entered does not match the ids of any todos currently stored in the database");
                    }
                }
                catch (Exception)
                {
                    System.Console.WriteLine("That is not a valid entry. Please try again.");
                    needsId = false;
                }              
            }
        }

        public static void ListComplete(Context todoContext)
        {
            var results = from t in todoContext.todos where (t.status == Status.complete)
            select t;
            
            //Will display the following message if there are no incomplete todos
            if (results.Count() == 0)
            {
                System.Console.WriteLine("You currently have not completed any ToDos.");
            }
            //if there are completed todos, list them
            else
            {
                System.Console.WriteLine("My Previously Completed ToDos:");

                int i = 0;
                foreach(Item item in results)
                {
                    i++;
                    item.displayID = i;
                    System.Console.WriteLine(item.displayID + " | " + item.description);
                    todoContext.SaveChanges();
                }
                System.Console.WriteLine("");
            }

        }

        public static void ListAll(Context todoContext)
        {
            var results = from t in todoContext.todos orderby t.status ascending
            select t;
            
            //Will display the following message if there are no incomplete todos
            if (results.Count() == 0)
            {
                System.Console.WriteLine("Your ToDo list is empty.");
            }
            //if there are todos, list them
            else
            {
                System.Console.WriteLine("All ToDos - incomplete and complete:");

                int i = 0;
                foreach(Item item in results)
                {
                    i++;
                    item.displayID = i;
                    System.Console.WriteLine(item.displayID + " | " + item.description + " | " + item.status);
                    todoContext.SaveChanges();
                }
                System.Console.WriteLine("");
            }

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
