using System;


namespace src
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            //Console.CursorVisible = false;
            //string[] options = new string[] { "1) Search by subject", "2) Search by teacher", "3) Search by student" };
            //var index = MenuHelper.DisplayMenu((a) =>
            //{
            //    Console.WriteLine(a);
            //    return int.TryParse(Console.ReadLine(), out int index) ? index : -1;
            //}, options);
            //Console.Clear();
            //Console.WriteLine(index);

            foreach (Subject subject in DataStore.Search(SearchType.Teacher, "Niels"))
            {
                Console.WriteLine(subject.Name);
            }
        }
    }
}
