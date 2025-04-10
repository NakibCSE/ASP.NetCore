using Demo.Web.Models.Demo;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            var model = new IndexModel();
            return View(model );
        }

        [HttpPost]
        public IActionResult Index(IndexModel model)
        {
            return View(model);
        }
    }
}
