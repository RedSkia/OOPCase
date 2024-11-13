using System;
using System.IO;
using System.Linq;


namespace src
{
    internal class Program
    {
        private static volatile bool skipKeyRead = false;
        private static void Main(string[] args)
        {
            //List af elev og lærer SKAL vises alfabetisk sorteret efter fornavne

            Console.CursorVisible = false;
            ConsoleKey loopKey = ConsoleKey.None;
            while (loopKey is not ConsoleKey.Escape)
            {
                skipKeyRead = false;
                Console.Clear();
                Console.ResetColor();
                int index = RootMenu();
                switch (index)
                {
                    case 1: SubjectMenu(); break;
                    case 2: TeacherMenu(); break;
                    case 3: StudentMenu(); break;
                    default: DisplayError("No Result Found!"); break;
                }
                if(!skipKeyRead)
                {
                    Console.WriteLine("Press any key BUT \"ESC\" to continue...");
                    loopKey = Console.ReadKey(true).Key;
                }
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
            skipKeyRead = true;
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
            Console.Clear();
            Console.ResetColor();
            ISubject[] subjects = DataStore.Search(SearchType.Subject).OrderBy(x => x.Name).ToArray();
            int index = MenuHelper.DisplayMenu((line) =>
            {
                Console.WriteLine(line);
                Console.Write("Input: ");
                return int.TryParse(Console.ReadLine(), out int index) ? index : -1;
            }, subjects.Select(subject => subject.Name).Order().ToArray());
            Console.Clear();
            if (index == -1)
            {
                DisplayError("No Result Found!");
                return;
            }
            ISubject subject = subjects[index-1];
            Console.WriteLine($"Subject: {subject.Name}");
            Console.WriteLine($"Teacher: {subject.Teacher.Name}");
            Console.WriteLine($"Total Students: {subject.Students.Count}");
            foreach (IPerson student in subject.Students.OrderBy(x => x.Name))
            {
                if(student.Birthday <= DateOnly.FromDateTime(DateTime.Now).AddYears(-20)) Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\t{student.Name}");
                Console.ResetColor();
            }

        }
        private static void StudentMenu()
        {
            Console.Clear();
            Console.ResetColor();
            Console.Write("Enter student name: ");
            string? input = Console.ReadLine();
            if (String.IsNullOrEmpty(input))
            {
                DisplayError("Student name empty!");
                return;
            }
            ISubject[] subjects = DataStore.Search(SearchType.Student, input);
            if(subjects is null || subjects.Length == 0)
            {
                DisplayError("Student not found!");
                return;
            }
            var studentName = subjects.First().Students.Where(student => student.Name.Contains(input, StringComparison.OrdinalIgnoreCase)).First().Name;
            Console.WriteLine($"Subjects assigned to student ({studentName})");
            Console.WriteLine("".PadRight(Console.WindowWidth, '='));
            foreach (ISubject subject in subjects.OrderBy(x => x.Name))
            {
                Console.WriteLine($"\tSubject: {subject.Name}");
                Console.WriteLine($"\tTeacher: {subject.Teacher.Name}");
                Console.WriteLine("".PadRight(Console.WindowWidth, '='));
            }
        }
        private static void TeacherMenu()
        {
            Console.Clear();
            Console.ResetColor();
            ISubject[] subjects = DataStore.Search(SearchType.Teacher).DistinctBy(x => x.Teacher.Name).OrderBy(x => x.Teacher.Name).ToArray();
            int index = MenuHelper.DisplayMenu((line) =>
            {
                Console.WriteLine(line);
                Console.Write("Input: ");
                return int.TryParse(Console.ReadLine(), out int index) ? index : -1;
            }, subjects.Select(subject => subject.Teacher.Name).Distinct().Order().ToArray());
            Console.Clear();
            if (index == -1)
            {
                DisplayError("No Result Found!");
                return;
            }
            string teacher = subjects[index - 1].Teacher.Name;
            ISubject[] teacherSubjects = DataStore.Search(SearchType.Teacher, teacher).ToArray();
            Console.WriteLine($"Subjects assigned to teacher ({teacher})");
            Console.WriteLine("".PadRight(Console.WindowWidth, '='));
            foreach (ISubject subject in teacherSubjects)
            {
                Console.WriteLine($"Subject: {subject.Name}");
                Console.WriteLine($"Total Students: {subject.Students.Count}");
                foreach (IPerson student in subject.Students.OrderBy(x => x.Name))
                {
                    if (student.Birthday <= DateOnly.FromDateTime(DateTime.Now).AddYears(-20)) Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\t{student.Name}");
                    Console.ResetColor();
                }
                Console.WriteLine("".PadRight(Console.WindowWidth, '='));
            }
        }
    }
}