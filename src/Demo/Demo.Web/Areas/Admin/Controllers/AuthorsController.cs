using AutoMapper;
using Demo.Application.Exceptions;
using Demo.Application.Services;
using Demo.Domain;
using Demo.Domain.Entities;
using Demo.Domain.Services;
using Demo.Infrastructure;
using Demo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Demo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController(ILogger<AuthorsController> logger, 
        IAuthorService authorService, IMapper mapper) : Controller
    {
        private readonly ILogger<AuthorsController> _logger = logger; 
        private readonly IAuthorService _authorService = authorService;
        private readonly IMapper _mapper = mapper;

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
                try
                {
                    var author = _mapper.Map<Author>(model);
                    author.ID = IdentityGenerator.NewSequentialGuid();
                    _authorService.AddAuthor(author);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Author added",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }
                catch(DuplicateAuthorNameException de)
                {
                    ModelState.AddModelError("DuplicateAuthor", de.Message);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = de.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to add author");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Failed to add author",
                        Type = ResponseTypes.Danger
                    });
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Update()
        {
            var model = new UpdateAuthorModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _authorService.DeleteAuthor(id);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Author Deleted",
                    Type = ResponseTypes.Success
                });
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "Failed to delete author");

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Failed to add author",
                    Type = ResponseTypes.Danger
                });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult GetAuthorJsonData([FromBody] AuthorListModel model)
        {
            try
            {
                var (data, total, totalDisplay) = _authorService.GetAuthors(model.PageIndex, model.PageSize, 
                    model.FormatSortExpression("Name","Biography", "Rating", "ID"), model.Search);

                var authors = new
                {
                    recordsTotal = total,
                    recordsFiltered = totalDisplay,
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
