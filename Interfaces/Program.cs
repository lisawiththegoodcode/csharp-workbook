using System;
using System.Collections.Generic;

namespace Interfaces
{
    public class Program
    {
        // Fields
        static Engine _engine;
        static Workflow _workflow;

        // Properties
        // n/a

        // Constructor(s)
        static Program()
        {
            _engine = new Engine();
            _workflow = new Workflow();
        }

        // Methods
        public static void Main()
        {
            _engine.Run(_workflow);
        }
    }

    public class Engine
    {
        // Fields
        // n/a

        // Properties
        // n/a

        // Constructor(s)
        public Engine()
        {
            // not used
        }

        // Methods
        public void Run(Workflow workflow)
        {
            foreach (var activity in workflow.GetActivities())
            {
                activity.Execute();
            }
        }
    }

    public class Workflow
    {
        // Fields
        List<IActivity> _activities;

        // Properties
        // n/a

        // Constructor
        public Workflow()
        {
            _activities = new List<IActivity>
            {
                { new SportActivity{Message = "Running..." } },
                { new SportActivity{Message = "Batting..." } },
                { new SportActivity{Message = "Kicking..." } },
                { new SportActivity{Message = "Dribbling..." } },
                { new OutdoorActivity{Message = "Fishing..." } },
                { new OutdoorActivity{Message = "Camping..." } },
                { new OutdoorActivity{Message = "Grilling..." } },
                { new IndoorActivity{Message = "Watching TV..." } },
                { new IndoorActivity{Message = "Drawing..." } },
                { new IndoorActivity{Message = "Baking..." } },
            };
        }

        // Methods
        public List<IActivity> GetActivities()
        {
            return _activities;
        }
    }

    public class SportActivity : IActivity
    {
        // Fields
        // n/a

        // Properties
        public string Message { get; set; }

        // Constructor(s)
        public SportActivity()
        {
            // not used
        }

        // Methods
        public void Execute()
        {
            Console.WriteLine("Performing Sport Activity: {0}", Message);
        }
    }

    public class OutdoorActivity : IActivity
    {
        public string Message { get; set; }

        public void Execute()
        {
            Console.WriteLine("Performing Outdoor Activity: {0}", Message);
        }
    }


    public class IndoorActivity : IActivity
    {
        public string Message { get; set; }

        public void Execute()
        {
            Console.WriteLine("Performing Indoor Activity: {0}", Message);
        }
    }

    //Here is an example of an interface
    public interface IActivity
    {
        // Properties
        string Message { get; set; }

        // Methods
        void Execute();
    }
}
