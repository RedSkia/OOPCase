﻿using src.CustomTypes.DTOs;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;

namespace src.CustomTypes
{
    public static class DataStore
    {
        private static List<Subject> _subjects = new();
        public static IReadOnlyList<Subject> Subjects => _subjects;
        private static readonly HashSet<string> _students = 
        [
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
        ];
        public static Student[] GetRandomStudents(int limit)
        {
            limit = Math.Clamp(limit, 0, _students.Count - 1);
            var buffer = ArrayPool<Student>.Shared.Rent(_students.Count - 1);
            Random rand = new();
            try
            {

                _students.Select(name => new Student() { Name = name, Birthday = new(rand.Next(1990, DateTime.Now.Year-1), rand.Next(12), rand.Next(365)) });
                rand.Shuffle(buffer);

                return buffer
                    .Where(student => student != null)
                    .Take(limit)
                    .ToArray();
            }
            finally
            {
                ArrayPool<Student>.Shared.Return(buffer);
            }
        }

        public static void CreateData()
        {
            _subjects = new List<Subject>
            {
                new("Basic Programming",    new() { Name = "Henrik Vincents",   Birthday = new(1995,10,20) }, GetRandomStudents(12)),
                new("OOP",                  new() { Name = "Niels Olesen",      Birthday = new(1995,10,20) }, GetRandomStudents(15)),
            };

/*
             
Henrik Vincents
Michael Gilbert Hansen
Niels Olesen
Peter Erik Bergmann
             */
        }
    }
}