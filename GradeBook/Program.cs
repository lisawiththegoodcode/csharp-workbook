using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            string nameInput;
            string[] students;
            string gradeInput;
            string[] stringGrades;
            int[] intGrades;
            Stack<String> myStudents = new Stack<String>();
            Dictionary<string, int[]> gradeBook = new Dictionary<string, int[]>();

            Console.WriteLine(@"Please enter your list of students. Use a comma and a single space to seperate each student's name (i.e. Lisa, Jason).");
            // Type ""done"" when you are finished entering students.
            nameInput = Console.ReadLine();
            students = nameInput.Split(", ");

            foreach (string student in students)
            {
                myStudents.Push(student);
                Console.WriteLine("Please enter the grades for {0}", student);
                gradeInput = Console.ReadLine();
                stringGrades = gradeInput.Split(", ");
                intGrades = new int[stringGrades.Length];


                for (int i = 0; i < stringGrades.Length; i++) 
                {
                    intGrades[i] = Int32.Parse(stringGrades[i]);
                    // Console.WriteLine(stringGrades[i]);
                }  
                
                gradeBook.Add(student, intGrades);
                // double average = intGrades.Average();
                // Console.WriteLine(average);
            }

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
