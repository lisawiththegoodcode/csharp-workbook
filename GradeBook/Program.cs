using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            //declare variables
            string nameInput;
            string[] students;
            string gradeInput;
            string[] stringGrades;
            int[] intGrades;
            Stack<String> myStudents = new Stack<String>();
            Dictionary<string, int[]> gradeBook = new Dictionary<string, int[]>();

            Console.WriteLine(@"Please enter your list of students. Use a comma and a single space to seperate each student's name (i.e. Lisa, Jason).");
            nameInput = Console.ReadLine();
            students = nameInput.Split(", ");

            //loop through each student inputted and add them to a stack "my students." 
            foreach (string student in students)
            {
                myStudents.Push(student);

                // prompt for grade data, split this input into individual strings for each grade 
                Console.WriteLine($"Please enter the grades for {student}. Use a comma and a single space to seperate each grade.");
                gradeInput = Console.ReadLine();
                stringGrades = gradeInput.Split(", ");

                //initialize int array "intGrades" to store grades entered. convert each grade to an integer and then store it in the int array 
                intGrades = new int[stringGrades.Length];
                for (int i = 0; i < stringGrades.Length; i++) 
                {
                    intGrades[i] = Int32.Parse(stringGrades[i]);
                    // Console.WriteLine(stringGrades[i]);
                }  
                
                //initialize a dictionary with student name as key and grades (as int array) as values. 
                gradeBook.Add(student, intGrades);
            }

            //Use stack to print student count and confirm last entry. 
            Console.WriteLine($"You entered grades for {myStudents.Count} students. The last student you entered a grade for was {myStudents.Peek()}. Here are the current GPAs for each student:");

            //calculate GPA for each student and their array of grades
            foreach (var name in gradeBook.Keys)
            {
                int[] grades = gradeBook[name];
                int total = 0;

                foreach (var grade in grades)
                {
                    total += grade; 
                }

                double average = (total/(grades.Length));
                Console.WriteLine($"{name}'s grade point average: {average}");
            }

        }
    }
}
