using System.Diagnostics;
using Demo.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItem _item;
        private readonly IProduct _product;

        public HomeController(ILogger<HomeController> logger, IItem item, [FromKeyedServices("Config1")] IProduct product)
        {
            _logger = logger;
            _item = item;
            _product = product;
        }

        public IActionResult Index()
        {
            var amount = _item.getAmount();
            var price = _product.getPrice();
            Console.WriteLine(price);
            Console.WriteLine("I am from IAction result index page");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
