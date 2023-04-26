using System.Xml.Linq;
using static System.Console;

namespace LinqInAction.Main;

public class Program
{
    public static void Main(string[] args)
    {
        #region LAMBDA TESTS
        ReturnCustomers();
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 222, 333 };
        var wordsTest = new[] { "aaa", "bbb", "cdfe" };

        Console.WriteLine($"Is any > 100 ?: {IsAny(numbers, IsAnyLargerThan100)}");

        Console.WriteLine($"Is any even ?: {IsAny(numbers, IsAnyEven)}");

        Console.WriteLine(IsAny(wordsTest, word => word.Length == 4));
        #endregion

        #region EXTENSIONS TESTS
        var multilineString = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. 
                                Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, 
                                when an unknown printer took a galley of type and scrambled it to make a type specimen book. 
                                It has survived not only five centuries, but also the leap into electronic typesetting, 
                                remaining essentially unchanged. It was popularised in the 1960s with the release of 
                                Letraset sheets containing Lorem Ipsum passages, and more recently with desktop 
                                publishing software like Aldus PageMaker including versions of Lorem Ipsum";

        var countOfLines = multilineString.GetCountOfLines();
        WriteLine();
        WriteLine("EXTENSIONS TESTS");
        WriteLine($"Lines: {countOfLines}");
        WriteLine();
        #endregion

        #region XML
        /*var xml =
        new XElement("contacts",
        from contact in contacts
        select new XElement("contact",
            new XAttribute("name", contact.CompanyName),
            new XAttribute("phone", contact.Phone)
            )
        );*/
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

    public static bool IsAny<T>(IEnumerable<T> numbers, Func<T, bool> predicate)
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

    #region Linq Defered Execution
    /*
     *  Deferred execution means that the evaluation of a 
     *  LINQ expression is delayed until the value is actually needed.  
     *  It allows us to work on the latest data.
     *  It improves the performance, as the query is materialized
     *  only when it's actually needed, so we can avoid
     *  unecessary execution.
     */
    public static void WordsExample()
    {
        List<string> words = new() { "a", "bb", "ccc", "dddd" };

        // this is just a query
        var shortWords = words.Where(word => word.Length < 3);

        // this is a materialized data
        // var shortWords = words.Where(word => word.Length < 3).ToList();

        foreach (var word in shortWords)
            WriteLine(word);

        words.Add("e");

        foreach (var word in shortWords)
            WriteLine(word);
    }
    #endregion

    #region Method syntax and query syntax
    public static void MethodAndQuerySynta()
    {
        List<int> nums = new() { 1, 2, 3, 4, 5, 6 };

        var methodSyntax = nums
            .Where(x => x < 10)
            .OrderBy(x => x);

        var querySyntax = from n in nums
                          where n < 10
                          orderby n
                          select n;
    }
    #endregion
}

#region Extension Methods
public static class StringExtensions
{
    public static int GetCountOfLines(this string input)
    {
        return input.Split("\n").Length;
    }
}
#endregion