namespace LinqInAction.Main;

public class Program
{
    public static void Main(string[] args)
    {
        ReturnCustomers();
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 222, 333 };

        Console.WriteLine($"Is any > 100 ?: {IsAny(numbers, IsAnyLargerThan100)}");

        Console.WriteLine($"Is any even ?: {IsAny(numbers, IsAnyEven)}");


        #region XML

        var xml =
        new XElement("contacts",
        from contact in contacts
        select new XElement("contact",
            new XAttribute("name", contact.CompanyName),
            new XAttribute("phone", contact.Phone)
            )
        );
        #endregion
        #region SIMPLE QUERY IN ARRAY
        // Hello LINQ to objects

        string[] words = { "hello", "wonderful", "linq", "beautiful", "world" };

        var shortWords = from word in words
                         where word.Length <= 5
                         select word;

        foreach (var item in shortWords)
        {
            Console.WriteLine(item);
        }

        var groups = from word in shortWords
                     orderby word ascending
                     group word by word.Length into lengthGroups
                     orderby lengthGroups.Key descending
                     select new { Length = lengthGroups.Key, Words = lengthGroups };

        foreach (var item in groups)
        {
            Console.WriteLine("Words of length " + item.Length);
            foreach (string w in item.Words)
                Console.WriteLine(" " + w);
        }

        // Listing 1.9 Hello LINQ in C# improved with grouping and sorting 

        var g1 = from word in words
                 orderby word ascending
                 group word by word.Length into lengthGroups
                 orderby lengthGroups.Key descending
                 select new { Length = lengthGroups.Key, Words = lengthGroups };

        foreach (var group in g1)
        {
            Console.WriteLine("Words of length " + group.Length);
            foreach (string word in group.Words)
                Console.WriteLine(" " + word);
            {

            }
        }
        #endregion
    }

    #region UNDERSTANDING LAMBDA EXPRESSIONS
    // Any()
    public static bool IsAnyLargerThan100(int number) { return number > 100; }

    public static bool IsAnyEven(int number) { return number % 2 == 0; }

    public static bool IsAny(int[] numbers, Func<int, bool> predicate)
    {
        foreach (var number in numbers)
            if (predicate(number)) return true;

        return false;
    }
    #endregion


    #region QUERING DATABASE
    private static void ReturnCustomers()
    {
        var db = new NorthwindContext();

        var contacts = from customer in db.Customers
                       where customer.CompanyName.StartsWith("A")
                       && customer.Orders.Count > 0
                       orderby customer.CompanyName
                       select new { customer.CompanyName, customer.Phone };

        foreach (var item in contacts)
        {
            Console.WriteLine(item);
        }
    }
    #endregion
}
