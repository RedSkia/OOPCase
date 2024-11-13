using System;
using System.IO;
using System.Linq;


namespace src
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.CursorVisible = false;
            int index = RootMenu();
            Console.Clear();
            Console.ResetColor();
            switch (index)
            {
                case 1: SubjectMenu(); break;
                case 3: StudentMenu(); break;
                default: DisplayError("No Result Found!"); break;
            }
     


        }
        private static void DisplayError(in string error)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{error} (Press any key to continue)");
            Console.ResetColor();
            Console.ReadKey(true);
            Console.Clear();
        }
        private static int RootMenu()
        {
            string[] options = ["Search by Subject", "Search by Teacher", "Search by Student"];
            return MenuHelper.DisplayMenu((line) =>
            {
                Console.WriteLine(line);
                Console.Write("Input: ");
                return int.TryParse(Console.ReadLine(), out int index) ? index : -1;
            }, options);
        }
        private static void SubjectMenu()
        {
            Subject[] subjects = DataStore.Search(SearchType.Subject);
            int index = MenuHelper.DisplayMenu((line) =>
            {
                Console.WriteLine(line);
                Console.Write("Input: ");
                return int.TryParse(Console.ReadLine(), out int index) ? index : -1;
            }, subjects.Select(subject => subject.Name).ToArray());
            Console.Clear();
            if (index == -1)
            {
                DisplayError("No Result Found!");
                return;
            }
            Subject subject = subjects[index-1];
            Console.WriteLine($"Subject: {subject.Name}");
            Console.WriteLine($"Teacher: {subject.Teacher.Name}");
            Console.WriteLine($"Total Students: {subject.Students.Count}");
            foreach (Student student in subject.Students)
            {
                Console.ResetColor();
                if(student.Birthday <= DateOnly.FromDateTime(DateTime.Now).AddYears(-20)) Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\t{student.Name}");
            }
        }
        private static void StudentMenu()
        {
            Console.Write("Enter student name: ");
            string? input = Console.ReadLine();
            if (String.IsNullOrEmpty(input))
            {
                DisplayError("Student name empty!");
                return;
            }
            Subject[] subjects = DataStore.Search(SearchType.Student, input);
            if(subjects is null || subjects.Length == 0)
            {
                DisplayError("Student not found!");
                return;
            }
            var studentName = subjects.First().Students.Where(student => student.Name.Contains(input, StringComparison.OrdinalIgnoreCase)).First().Name;
            Console.WriteLine($"Subjects assigned to student ({studentName})");
            Console.WriteLine("".PadRight(Console.WindowWidth, '='));
            foreach (Subject subject in subjects)
            {
                Console.WriteLine($"\tSubject: {subject.Name}");
                Console.WriteLine($"\tTeacher: {subject.Teacher.Name}");
                Console.WriteLine("".PadRight(Console.WindowWidth, '='));
            }
        }
    }
}
