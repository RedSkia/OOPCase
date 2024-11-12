using System;

namespace src
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.CursorVisible = false;
            string[] options = new string[] { "1) Search by subject", "2) Search by teacher", "3) Search by student" };
            var index = MenuHelper.DisplayMenu((a) =>
            {
                Console.WriteLine(a);
                return int.TryParse(Console.ReadLine(), out int index) ? index : -1;
            }, options);
            Console.Clear();
            Console.WriteLine(index);
            */

            var x = DataStore.GetRandomStudents(10);
            foreach (var i in x)
            {
                Console.WriteLine(i);
            }
        }
    }
}
