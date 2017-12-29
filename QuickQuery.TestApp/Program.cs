using System;

namespace QuickQuery.TestApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var quickQuery = new QuickQuery()
            {
                ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\wichu\Documents\Visual Studio\Projects\QuickQuery\QuickQuery.TestApp\Database.mdf"";Integrated Security=True"
            };

            var items = quickQuery.Query("SELECT Id, Name, Value FROM Items", new { Id = 0, Name = "", Value = 0M });

            items.ForEach(x => Console.WriteLine(x));

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}