
// array , IEnumerable, IQueryable

using LinqExample;

List<string> names = new List<string> { "apple", "banana", "mango" };
List<Order> orders = new List<Order>
{
    new Order { Item = "banana", Quantity = 10  },
    new Order { Item = "mango", Quantity = 20 },
    new Order { Item = "jackfruit", Quantity = 5 }
};

var quantities = (from o in orders join n in names on o.Item equals n select o.Quantity);

foreach (var quant in quantities)
    Console.WriteLine(quant);


int count = orders.Count(x => x.Quantity < 15);