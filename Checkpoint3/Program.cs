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
                        
            //instantiating variables for user input loop 
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
                        Utils.UpdateToDo(todoContext);
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
                        Utils.ListType(todoContext, 7);
                        needsCommand = true;
                    }
                    else if (input == 8)
                    {
                        Utils.ListType(todoContext, 8);
                        needsCommand = true;
                    }
                    else if (input == 9)
                    {
                        Utils.ListType(todoContext, 9);
                        needsCommand = true;
                    }
                    else if (input == 10)
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

    //default position for an new item will be general. A good future extension project would be to explore how a user could create their own type
    public enum Type {general, personal, work, school}
    //default position for a new item will be incomplete
    public enum Status {incomplete, complete}

    //begin item class
    public class Item 
    {
        //Item properties
        public int id {get;set;}
        public int displayID {get;set;}
        public string description {get;set;}
        public Type type {get; set;}
        public Status status {get; set;}
        public DateTime deadline {get; set;}

        //need an empty constructor to work with Entity Framework
        public Item(){}
        
        //the only property required to instantiate a new todo is the description
        public Item(string description)
        {
            this.description = description;
        }
    }

    //begin utils class, put methods all methods required to perform app tasks here, going to be static methods
    public static class Utils
    {        
        //method ListIncomplete takes in todoContext from main and prints out any todos that need completion, this is the default state for the program
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
                System.Console.WriteLine("My Current ToDos:");
                List(todoContext, results);
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
            System.Console.WriteLine("Enter 6 to view all your ToDos, both incomplete and previously completed");
            System.Console.WriteLine("Enter 7 to view personal ToDos only");
            System.Console.WriteLine("Enter 8 to view work ToDos only");
            System.Console.WriteLine("Enter 9 to view school ToDos only");
            System.Console.WriteLine("Enter 10 to quit");
        }
        //FUNCTION 1: AddToDo takes in todo context and uses this context to add a new to do to the list
        public static void AddToDo(Context todoContext)
        {
            //take input for new ToDo description
            System.Console.WriteLine("Please enter your new ToDo:");
            string description = Console.ReadLine();

            //initialize a new item with a description
            Item newItem = new Item(description);
            todoContext.todos.Add(newItem);
            
            //begin loop to prompt user for a type for this todo
            bool needsTypeResponse = true;
            while (needsTypeResponse)
            {
                System.Console.WriteLine("Would you like to add this ToDo to a specific list? Enter 1 for yes or 2 for no.");
                try
                {
                    int response = Int32.Parse(Console.ReadLine());
                    if (response == 1)
                    {
                        SetType(newItem);
                        needsTypeResponse = false;            
                    }                 
                    else if (response == 2)
                    {
                        needsTypeResponse = false;
                    }            
                    else
                    {
                        throw new Exception("Invalid Entry: 1 or 2 expected");
                    }
                }
                catch
                {
                    System.Console.WriteLine("That is not a valid entry. Please try again.");
                    needsTypeResponse = true;
                }            
            }

            //begin loop to prompt user for a deadline for this todo
            bool needsDeadlineResponse = true;
            while (needsDeadlineResponse)
            {
                System.Console.WriteLine("Would you like to add a deadline to this ToDo? Enter 1 for yes or 2 for no.");

                try
                {
                    int response = Int32.Parse(Console.ReadLine());
                    if (response == 1)
                    {    
                        SetDeadline(newItem);
                        needsDeadlineResponse = false;
                    }
                    else if (response == 2)
                    {
                        needsDeadlineResponse = false;
                    }            
                    else
                    {
                        throw new Exception("Invalid Entry: 1 or 2 expected");
                    }
                }
                catch
                {
                    System.Console.WriteLine("That is not a valid entry. Please try again.");
                    needsDeadlineResponse = true;
                } 
            }  
            
            System.Console.WriteLine("Your ToDo has been added. Thank you!");
            System.Console.WriteLine("");
            //save changes to store this new ToDo and any attributes that have been defined
            todoContext.SaveChanges();
        }
        //method SetType takes in an item and updates it's type property
        public static void SetType(Item item)
        {
            //give type options
            System.Console.WriteLine("Please enter 1 for Personal, 2 for Work, or 3 for School. If you would prefer to keep this ToDo on your general ToDo list, please enter 4.");
            
            //take in input as an integer
            //this function call must be inbedded in a try/catch statement in case a non-integer gets entered
            int input = Int32.Parse(Console.ReadLine());
            
            //begin conditional statements to set the type according to the number entered
            if (input == 1)
            {
                item.type = Type.personal;
            }
            else if (input == 2)
            {
                item.type = Type.work;
            }
            else if (input == 3)
            {
                item.type = Type.school;
            }
            else
            {
                throw new Exception("Invalid Entry: 1, 2, 3 or 4 expected");
            } 
        }
        //method SetDeadline takes in an item and sets it's deadline property
        public static void SetDeadline(Item item)
        {
            //prompt user for a deadline
            System.Console.WriteLine("Please enter the deadline for this ToDo. Follow this Date/Time format: MM/DD/YYYY HH:MM:SS AM/PM:");
            //take in input as a DateTime variable
            //this function call must be embedded in a try/catch statement in case user input is not able to be parsed as a datetime
            DateTime dt = DateTime.Parse(Console.ReadLine());
            //set the item's deadline to this time
            item.deadline = dt;
        }
        //FUNCTION 2: UpdateToDo takes in the todoContext and makes changes to a specified todo's description, type or deadline
        public static void UpdateToDo(Context todoContext)
        {
            //prompt for displayID
            System.Console.WriteLine("Please enter the number associated with the ToDo item you would like to update:");
            
            //declare variables
            int id;
            bool needsId = true;

            //begin loop to confirm and update the ToDo
            while (needsId == true)
            {
                try
                {
                    //take in a number as input
                    id = Int32.Parse(Console.ReadLine());
                    //check if there's a match between this number and any item's displayID in the todo list
                    var match = from t in todoContext.todos where t.displayID == id 
                    select t;

                    
                    if (match.Count() == 1) //there is ONE number that matches an existing todo
                    {
                        foreach (Item item in match) // it didn't allow me to use t, so I followed the foreach format that has been working, even though there is only one item that is a match in this scenario
                        {
                            //confirm the ToDo they would like to update
                            System.Console.WriteLine("The ToDo item you would like to update is: " + item.description);
                            System.Console.WriteLine("Is that correct? Enter 1 for yes or 2 for no.");
                            
                            //take in a number as input
                            int response = Int32.Parse(Console.ReadLine());
                            bool needsUpdateResponse = true;

                            //begin inner loop to identify which property of the ToDo the user would like to update
                            if (response == 1)
                            {
                                while (needsUpdateResponse)
                                {
                                    System.Console.WriteLine("What would you like to change about this ToDo? Please enter 1 for Description, 2 for List, or 3 for Deadline.");

                                    try
                                    {
                                        //takes in number as input
                                        int input = Int32.Parse(Console.ReadLine());
                                        if (input == 1)
                                        {
                                            //prompt for new description
                                            System.Console.WriteLine("Please enter your new description:");
                                            string newDescription = Console.ReadLine();
                                            //update the description
                                            item.description = newDescription;
                                            //exit inner loop
                                            needsUpdateResponse = false;
                                        }
                                        else if (input == 2)
                                        {
                                            //call SetType function
                                            SetType(item);
                                            //exit inner loop
                                            needsUpdateResponse = false;
                                        }
                                        else if (input == 3)
                                        {
                                            //call SetDeadline function
                                            SetDeadline(item);
                                            //exit inner loop
                                            needsUpdateResponse = false;
                                        }            
                                        else
                                        {
                                            throw new Exception("Invalid Entry: 1, 2, or 3 expected");
                                        }
                                    }
                                    catch
                                    {
                                        System.Console.WriteLine("That is not a valid entry. Please try again.");
                                        //if error, go back to beginning of the loop and prompt for the property of the item the user would like to update
                                        needsUpdateResponse = true;
                                    }    
                                }                                        
                                System.Console.WriteLine("Your ToDo has been updated. Thank you!");
                                System.Console.WriteLine("");
                                //exit outer loop
                                needsId = false;
                                //save changes made
                                todoContext.SaveChanges();                                            
                            }
                            else if (response == 2)
                            {
                                //begin outer loop again prompting for item to update
                                needsId = true;
                                System.Console.WriteLine("Sorry about that! Let's try again. Please enter the number to the left of the ToDo item you'd like to update.");
                        
                            }
                            else
                            {
                                throw new Exception("Invalid Entry: 1 or 2 expected");
                            }
                        }
                    }
                    //logging possible error options to help problem solve if bugs occur
                    else if (match.Count() > 0)
                    {
                        System.Console.WriteLine($"This number is associated with multiple entries. Let's take a look at all the items associated with this number:");
                        foreach (Item item in match) 
                        {
                            //confirm the ToDo they would like to update
                            System.Console.WriteLine(item.description + " | " + item.status);
                            System.Console.WriteLine("Is this the ToDo you'd like to update? Please enter 1 for yes or 2 for no");
                            int input = Int32.Parse(Console.ReadLine());
                            if (input == 1)
                            {
                                bool needsUpdateResponse = true;
                                while (needsUpdateResponse)
                                {
                                    System.Console.WriteLine("What would you like to change about this ToDo? Please enter 1 for Description, 2 for List, or 3 for Deadline.");

                                    try
                                    {
                                        //takes in number as input
                                        int response = Int32.Parse(Console.ReadLine());
                                        if (response == 1)
                                        {
                                            //prompt for new description
                                            System.Console.WriteLine("Please enter your new description:");
                                            string newDescription = Console.ReadLine();
                                            //update the description
                                            item.description = newDescription;
                                            //exit inner loop
                                            needsUpdateResponse = false;
                                        }
                                        else if (response == 2)
                                        {
                                            //call SetType function
                                            SetType(item);
                                            //exit inner loop
                                            needsUpdateResponse = false;
                                        }
                                        else if (response == 3)
                                        {
                                            //call SetDeadline function
                                            SetDeadline(item);
                                            //exit inner loop
                                            needsUpdateResponse = false;
                                        }            
                                        else
                                        {
                                            throw new Exception("Invalid Entry: 1, 2, or 3 expected");
                                        }
                                    }
                                    catch
                                    {
                                        System.Console.WriteLine("That is not a valid entry. Please try again.");
                                        //if error, go back to beginning of the loop and prompt for the property of the item the user would like to update
                                        needsUpdateResponse = true;
                                    }    
                                }                                        
                                System.Console.WriteLine("Your ToDo has been updated. Thank you!");
                                System.Console.WriteLine("");
                                //exit outer loop
                                needsId = false;
                                //save changes made
                                todoContext.SaveChanges(); 
                                break;
                            }
                            else if (input == 2)
                            {
                                continue;
                            }
                            else
                            {
                                throw new Exception("Invalid entry at match.Count >0; 1 or 2 expected.");
                            } 
                        }
                    }
                    else
                    {
                        throw new Exception("the number entered does not match the ids of any todos currently stored in the database");
                    }
                }
                catch (Exception)
                {
                    System.Console.WriteLine("That is not a valid entry. Please try again.");
                    //begin outer loop again prompting for item to update
                    needsId = true;
                }              
            }
        }
        //FUNCTION 3: MarkComplete take in todoContext and updates the status property to complete
        public static void MarkComplete(Context todoContext)
        {
            System.Console.WriteLine("Please enter the number of the ToDo item you would like to mark complete:");
            //declare variables
            int id;
            bool needsId = true;

            //begin loop to mar the ToDo complet
            while (needsId == true)
            {
                try
                {
                    //parse input to a number
                    id = Int32.Parse(Console.ReadLine());
                    
                    //find the display id that matches the number the user inputted
                    var match = from t in todoContext.todos where (t.displayID == id)
                    select t;

                    if (match.Count() == 1) //ONE number that matches an existing todo
                    {
                        foreach (Item item in match)
                        {
                            //confirm the item that returned a match for their input
                            System.Console.WriteLine("The ToDo item you would like to mark complete is: " + item.description);
                            System.Console.WriteLine("Is that correct? Enter 1 for yes or 2 for no.");
                            //parse input to a number
                            int response = Int32.Parse(Console.ReadLine());
                            if (response == 1)
                            {
                                //update status, and update display id to 0 so that it does not retain the same id number as a future new ToDo
                                item.status = Status.complete;
                                item.displayID = 0;

                                System.Console.WriteLine("Your ToDo has been marked complete. Thank you!");
                                System.Console.WriteLine("");
                                //save
                                todoContext.SaveChanges();
                                //exit loop
                                needsId = false;
                            }
                            else if (response == 2)
                            {
                                System.Console.WriteLine("Sorry about that! Please enter 'M' to go back to the options menu or any other key to try again."); 
                                string input = Console.ReadLine().ToLower();
                                if (input == "m")
                                {
                                    //exit loop
                                    needsId = false;
                                }
                                else
                                {
                                    //go back to begining of loop and reprompt user to enter id
                                    needsId = true;
                                    System.Console.WriteLine("Please enter the number to the left of the ToDo item you'd like to mark complete.");
                                }
                            }
                            else
                            {
                                throw new Exception("Invalid Entry: 1 or 2 expected");
                            }
                        }
                    }
                    else if (match.Count() > 0)
                    {
                        System.Console.WriteLine($"This number is associated with multiple entries. Let's take a look at all the items associated with this number:");
                        foreach (Item item in match) 
                        {
                            //confirm the ToDo they would like to update
                            System.Console.WriteLine(item.description + " | " + item.status);
                            System.Console.WriteLine("Is this the ToDo you'd like to mark complete? Please enter 1 for yes or 2 for no");
                            int response = Int32.Parse(Console.ReadLine());
                            if (response == 1)
                            {
                                //update status, and update display id to 0 so that it does not retain the same id number as a future new ToDo
                                item.status = Status.complete;
                                item.displayID = 0;

                                System.Console.WriteLine("Your ToDo has been marked complete. Thank you!");
                                System.Console.WriteLine("");
                                //save
                                todoContext.SaveChanges();
                                //exit loop
                                needsId = false;
                                break;
                            }
                            else if (response == 2)
                            {
                                continue;
                            }
                            else
                            {
                                throw new Exception("Invalid entry at match.Count >0; 1 or 2 expected.");
                            } 
                        }
                    }
                    else
                    {
                        throw new Exception("the number entered does not match the ids of any todos currently stored in the database");
                    }
                }
                catch (Exception)
                {
                    System.Console.WriteLine("That is not a valid entry. Please try again.");
                    //go back to beginning of loop
                    needsId = true;
                }              
            }
        }
        //FUNCTION 4: DELETE takes in the todoContext and removes a todo from the list
        public static void Delete(Context todoContext)
        {
            System.Console.WriteLine("Please enter the number associated with the ToDo item you would like to delete:");
            //declare variables
            int id;
            bool needsId = true;
            //begin loop
            while (needsId == true)
            {
                try
                {
                    id = Int32.Parse(Console.ReadLine());

                    var match = from t in todoContext.todos where (t.displayID == id)
                    select t;

                    if (match.Count() == 1) //ONE number that matches an existing todo
                    {
                        foreach (Item item in match)
                        {
                            //use description to confirm the item to delete
                            System.Console.WriteLine("The ToDo item you would like to delete is: " + item.description);
                            System.Console.WriteLine("Is that correct? Enter 1 for yes or 2 for no.");

                            try
                            {
                                int response = Int32.Parse(Console.ReadLine());
                                if (response == 1)
                                {
                                    todoContext.todos.Remove(item);

                                    System.Console.WriteLine("Your ToDo has been deleted. Thank you!");
                                    System.Console.WriteLine("");

                                    //save and exit lop
                                    todoContext.SaveChanges();
                                    needsId = false;
                                }
                                else if (response == 2)
                                {
                                    System.Console.WriteLine("Sorry about that! Please enter 'M' to go back to the options menu or any other key to try again."); 
                                    string input = Console.ReadLine().ToLower();
                                    if (input == "m")
                                    {
                                        //exit loop
                                        needsId = false;
                                    }
                                    else
                                    {
                                        //go back to begining of loop and reprompt user to enter id
                                        needsId = true;
                                        System.Console.WriteLine("Please enter the number to the left of the ToDo item you'd like to mark complete.");
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
                        System.Console.WriteLine($"This number is associated with multiple entries. Let's take a look at all the items associated with this number:");
                        foreach (Item item in match) 
                        {
                            //confirm the ToDo they would like to update
                            System.Console.WriteLine(item.description + " | " + item.status);
                            System.Console.WriteLine("Is this the ToDo you'd like to delete? Please enter 1 for yes or 2 for no");
                            int response = Int32.Parse(Console.ReadLine());
                            if (response == 1)
                            {
                                todoContext.todos.Remove(item);

                                System.Console.WriteLine("Your ToDo has been deleted. Thank you!");
                                System.Console.WriteLine("");

                                //save and exit lop
                                todoContext.SaveChanges();
                                needsId = false;        
                                break;
                            }
                            else if (response == 2)
                            {
                                continue;
                            }
                            else
                            {
                                throw new Exception("Invalid entry at match.Count >0; 1 or 2 expected.");
                            } 
                        }
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
        //FUNCTION 5: ListComplete takes in a todocontext and prints the todo items with a complete status to the screen
        public static void ListComplete(Context todoContext)
        {
            //find the complete todos in the list
            var results = from t in todoContext.todos where (t.status == Status.complete)
            select t;
            System.Console.WriteLine("My Completed ToDos:");
            List(todoContext, results);
            System.Console.WriteLine("");
        }
        //FUNCTION 6: takes in a todo context and returns all todos sorted by status (with incomplete todos first)
        public static void ListAll(Context todoContext)
        {
            //get all todos and order by status
            var results = from t in todoContext.todos orderby t.status ascending
            select t;
            System.Console.WriteLine("All ToDos - complete and incomplete:");
            List(todoContext, results);
            System.Console.WriteLine("");
        }
        //FUNCTION 7-9: take in a todoContext and an integer and list either Personal, Work, or school todos only
        public static void ListType(Context todoContext, int type)
        {
            //conditional to sort out which type we should list
            if (type == 7)
            {
                var results = from t in todoContext.todos where t.type == Type.personal && t.status == Status.incomplete
                select t;
                System.Console.WriteLine("My Current Personal Todos:");
                List(todoContext, results);
            }
            else if (type == 8)
            {
                var results = from t in todoContext.todos where t.type == Type.work && t.status == Status.incomplete
                select t;
                System.Console.WriteLine("My Current Work Todos:");
                List(todoContext, results);
            }
            else if (type == 9)
            {
                var results = from t in todoContext.todos where t.type == Type.school && t.status == Status.incomplete
                select t;
                System.Console.WriteLine("My Current School Todos:");
                List(todoContext, results);
            }
            else
            {
                throw new Exception("issue occured in ListPersonal function");
            }

            System.Console.WriteLine("");
        }
        //Since many functions were ending with the same process for printing a list, I created a function to avoid copy/paste for the process of printing the list
        public static void List(Context todoContext, IQueryable<Item> results)
        {
            //Will display the following message if there are no todos in this results query
            if (results.Count() == 0)
            {
                System.Console.WriteLine("This ToDo list is empty.");
            }
            //if there are todos, list them
            else
            {
                //initialize counter
                int i = 0;
                foreach(Item item in results)
                {
                    i++;
                    item.displayID = i;
                    //use counter to set display id
                    if (item.deadline == DateTime.MinValue)
                    {
                        System.Console.WriteLine(item.displayID + " | " + item.description + " | " + item.type + " | no deadline | " + item.status);
                    }
                    else
                    {
                        System.Console.WriteLine(item.displayID + " | " + item.description + " | " + item.type + " | " + item.deadline + " | " + item.status);
                    }
                    //save each time (not sure if this is necessary but did it this way to be safe)
                    todoContext.SaveChanges();
                }
            }
        }
    }
    //context class which creates the set of todos and calls on the Entity Framework to help link to the database
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
