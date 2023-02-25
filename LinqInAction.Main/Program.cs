using System.Xml.Linq;

namespace LinqInAction.Main;

public class Program
{
    public static void Main(string[] args)
    {
        var contacts = from customter in db.Customers
            where customter.Name.StartsWith("A") 
            && customter.ORders.COunt > 0
            orderby customter.Name
            select new{customer.Name, customer.Phone};

        var xml = 
        new XElement("contacts",
        from contact in contacts
        select new XElement("contact",
            new XAttribute("name", contact.Name),
            new XAttribute("phone", contact.Phone)
            )
        );
    }
}