using System.Xml.Linq;

namespace LinqInAction.Main;

public class Program
{
    public static void Main(string[] args)
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

        /*var xml =
        new XElement("contacts",
        from contact in contacts
        select new XElement("contact",
            new XAttribute("name", contact.CompanyName),
            new XAttribute("phone", contact.Phone)
            )
        );*/

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

        foreach(var group in g1)
        {
            Console.WriteLine("Words of length " + group.Length);
            foreach (string word in group.Words)
                Console.WriteLine(" " + word);
            {

            }
        }
    }
}