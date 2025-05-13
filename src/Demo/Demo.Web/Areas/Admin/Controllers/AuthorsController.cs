using Demo.Application.Services;
using Demo.Domain;
using Demo.Domain.Entities;
using Demo.Domain.Services;
using Demo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Demo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController(ILogger<AuthorsController> logger, IAuthorService authorService) : Controller
    {
        private readonly ILogger<AuthorsController> _logger = logger; 
        private readonly IAuthorService _authorService = authorService;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            var model = new AddAuthorModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(AddAuthorModel model)
        {
            if (ModelState.IsValid)
            {
                _authorService.AddAuthor(new Author { Name = model.Name, Biography = string.Empty, Rating = 1.0 });
            }
            return View(model);
        }
        public JsonResult GetAuthorJsonData([FromBody] AuthorListModel model)
        {
            try
            {
                var (data, total, totalDisplay) = _authorService.GetAuthors(model.PageIndex, model.PageSize, 
                    model.FormatSortExpression("Name","Biography", "Rating", "ID"), model.Search);

                var authors = new
                {
                    recordsTotal = total,
                    recordesFiltered = totalDisplay,
                    data = (from record in data
                            select new string[]
                            {
                                    HttpUtility.HtmlEncode(record.Name),
                                    HttpUtility.HtmlEncode(record.Biography),
                                    record.Rating.ToString(),
                                    record.ID.ToString()
                            }).ToArray()
                };
                return Json(authors);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "There was a problem in getting authors");
                return Json(DataTables.EmptyResult);
            }
        }
    }
}
