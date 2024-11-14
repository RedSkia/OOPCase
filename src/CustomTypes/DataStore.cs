using System;
using System.Collections.Generic;
using System.Linq;

namespace src.CustomTypes
{
    public static class DataStore
    {
        public static List<ISubject> _subjects = new();
        public static IReadOnlyList<ISubject> Subjects => _subjects;
        private static readonly List<string> _students = new()
        {
            "Andreas Lorenzen",
            "Azad Akdeniz",
            "Bertram Estrup Axen",
            "Casper Clemmensen",
            "Daniel Bjerre Jensen",
            "Djonatan Gjertsen",
            "Dylan Eric Aghahowa",
            "Hjalte Moesgaard Leth",
            "Johan Milter Jakobsen",
            "Louis Thomas Dao Pedersen",
            "Lukas Haugstad Frederiksen",
            "Marcus Wahlstrøm",
            "Marcus Slot Rohr",
            "Marius Ulslev Fogelgren",
            "Mathias Altenburg",
            "Patrick Gutierrez Fogelstrøm",
            "Ramtin Akbari",
            "Sebastian Tølbøl Nielsen",
            "Simon Heilbuth",
            "Thobias Svarter Hammarkvist",
            "Yosef Kasas"
        };

        static DataStore() => CreateData();
        private static IPerson[] GetRandomStudents(int limit)
        {
            limit = Math.Clamp(limit, 0, _students.Count - 1);
            Random rand = new();

            var students = _students
            .Select(name => new Student
            {
                Name = name,
                Birthday = new Birthday() { Date = new(rand.Next(1990, DateTime.Now.Year - 10), rand.Next(1, 12), rand.Next(1, 25)) }
            }).ToArray(); 

            rand.Shuffle(students);
            return students.Take(limit).ToArray();
        }
        private static void CreateData()
        {
            IPerson[] students1 = GetRandomStudents(9);
            IPerson[] students2 = GetRandomStudents(16);
            IPerson[] students3 = GetRandomStudents(12);
            IPerson[] students4 = GetRandomStudents(15);
            _subjects = new List<ISubject>
            {
                new Subject(){ Name = "Basic Programming",      Teacher = new(){ Name = "Henrik Vincents",          Birthday = new Birthday() { Date = new(1995, 7, 16) } }, Students = students1 },
                new Subject(){ Name = "OOP",                    Teacher = new(){ Name = "Niels Olesen",             Birthday = new Birthday() { Date = new(1981, 9, 4) } }, Students = students3 },
                new Subject(){ Name = "Computer Technology",    Teacher = new(){ Name = "Michael Gilbert Hansen",   Birthday = new Birthday() { Date = new(1993, 5, 11) } }, Students = students2 },
                new Subject(){ Name = "Server Security",        Teacher = new(){ Name = "Peter Erik Bergmann",      Birthday = new Birthday() { Date = new(1999, 4, 20) } }, Students = students4 },
                new Subject(){ Name = "Database Programming",   Teacher = new(){ Name = "Peter Erik Bergmann",      Birthday = new Birthday() { Date = new(1999, 4, 20) } }, Students = students4 },
                new Subject(){ Name = "ClientSide Programming", Teacher = new(){ Name = "Michael Gilbert Hansen",   Birthday = new Birthday() { Date = new(1993, 5, 11) } }, Students = students2 },
            };
        }
        public enum SearchType : byte
        {
            Subject = 1,
            Teacher = 2,
            Student = 3,
        }
        public static ISubject[] Search(SearchType search, string value = "") => search switch
        {
            SearchType.Subject => Subjects.Where(subject => subject.Name.Contains(value, StringComparison.OrdinalIgnoreCase)).ToArray(),
            SearchType.Teacher => Subjects.Where(subject => subject.Teacher.Name.Contains(value, StringComparison.OrdinalIgnoreCase)).ToArray(),
            SearchType.Student => Subjects.Where(subject => subject.Students.Any(student => student.Name.Contains(value, StringComparison.OrdinalIgnoreCase))).ToArray(),
            _ => []
        };
    }
}