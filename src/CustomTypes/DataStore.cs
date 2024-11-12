using src.CustomTypes.DTOs;
using System;
using System.Buffers;
using System.Collections.Generic;

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
        public static string[] GetRandomStudents(int limit)
        {
            limit = Math.Clamp(limit, 0, _students.Count - 1);
            var buffer = ArrayPool<string>.Shared.Rent(_students.Count - 1);
            _students.CopyTo(buffer);
            new Random().Shuffle(buffer);
            return buffer.AsSpan().Slice(0, limit).ToArray();
        }


        public static void CreateData()
        {
            _subjects = new List<Subject>
            {
            };

/*
             
Henrik Vincents
Michael Gilbert Hansen
Niels Olesen
             */
        }
    }
}