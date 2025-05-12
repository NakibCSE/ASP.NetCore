using Demo.Domain;
using Demo.Domain.Services;
using System.Data;

namespace Demo.Web.Areas.Admin.Models
{
    public class AuthorListModel : DataTables
    {
        public object GetAuthors(IAuthorService authorService)
        {
            try
            {
                var result = authorService.GetAuthors(PageIndex, PageSize, FormatSortExpression("Name"), Search);
                return EmptyResult;
            }
            catch
            {
                return EmptyResult;
            }
            
        }
    }
}
