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
                select new{ customer.CompanyName, customer.Phone};

        foreach (var item in contacts)
        {
            Console.WriteLine(item);
        }

        // var xml = 
        // new XElement("contacts",
        // from contact in contacts
        // select new XElement("contact",
        //     new XAttribute("name", contact.Name),
        //     new XAttribute("phone", contact.Phone)
        //     )
        // );
    }
}