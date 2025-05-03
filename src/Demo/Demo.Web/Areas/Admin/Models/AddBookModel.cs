namespace Demo.Web.Areas.Admin.Models
{
    public class AddBookModel
    {
        public string Title { get; set; }
        public Guid? AuthorID { get; set; }
        public DateTime? PublishDate { get; set; }
    }
}
