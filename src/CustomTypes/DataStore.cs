using System;
using System.Collections.Generic;
using System.Linq;

namespace src.CustomTypes
{
    public static class DataStore
    {
        public static List<Subject> _subjects = new();
        public static IReadOnlyList<Subject> Subjects => _subjects;
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
        private static Student[] GetRandomStudents(int limit)
        {
            limit = Math.Clamp(limit, 0, _students.Count - 1);
            Random rand = new();

            var students = _students
            .Select(name => new Student
            {
                Name = name,
                Birthday = new DateOnly(rand.Next(1990, DateTime.Now.Year - 10), rand.Next(1, 12), rand.Next(1, 25))
            }).ToArray(); 

            rand.Shuffle(students);
            return students.Take(limit).ToArray();
        }
        private static void CreateData()
        {
            /*
            Henrik Vincents
            Michael Gilbert Hansen
            Niels Olesen
            Peter Erik Bergmann
            */
            _subjects = new List<Subject>
            {
                new("Basic Programming",        new() { Name = "Henrik Vincents",           Birthday = new(1995,10,20) }, GetRandomStudents(99)),
                new("OOP",                      new() { Name = "Niels Olesen",              Birthday = new(1995,10,20) }, GetRandomStudents(14)),
                new("Computer Technology",      new() { Name = "Michael Gilbert Hansen",    Birthday = new(1995,10,20) }, GetRandomStudents(9)),
                new("Server Security",          new() { Name = "Peter Erik Bergmann",       Birthday = new(1995,10,20) }, GetRandomStudents(16)),
                new("Database Programming",     new() { Name = "Peter Erik Bergmann",       Birthday = new(1995,10,20) }, GetRandomStudents(12)),
                new("Client Side Programming",  new() { Name = "Michael Gilbert Hansen",    Birthday = new(1995,10,20) }, GetRandomStudents(18)),
            };
        }
        public enum SearchType : byte
        {
            Subject = 1,
            Teacher = 2,
            Student = 3,
        }
        public static Subject[] Search(SearchType search, string value = "") => search switch
        {
            SearchType.Subject => Subjects.Where(subject => subject.Name.Contains(value, StringComparison.OrdinalIgnoreCase)).ToArray(),
            SearchType.Teacher => Subjects.Where(subject => subject.Teacher.Name.Contains(value, StringComparison.OrdinalIgnoreCase)).ToArray(),
            SearchType.Student => Subjects.Where(subject => subject.Students.Any(student => student.Name.Contains(value, StringComparison.OrdinalIgnoreCase))).ToArray(),
            _ => []
        };
    }
}