namespace Demo.Domain.Entities
{
    public class Book : IEntity<Guid>
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public Guid AuthorID { get; set; }
    }
}
