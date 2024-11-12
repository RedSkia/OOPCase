namespace src
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] options = new string[] { "Option 1", "Option 2", "Option 3" };

            var index = MenuHelper.DisplayMenu((a) =>
            {
                Console.WriteLine(a);
                return int.TryParse(Console.ReadLine(), out int index) ? index : -1;
            }, options);
            Console.WriteLine(index);

        }
    }
}
