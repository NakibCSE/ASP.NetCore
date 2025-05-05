using Demo.Domain.Entities;
using Demo.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Repository
{
    public class BookRepository : Repository<Book, Guid>, IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public BookRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public List<Book> GetLatestBooks()
        {
            DateTime date = DateTime.Now.AddDays(-365);
            return _dbContext.Books.Where(x => x.PublishDate < date).ToList();
        }
    }
}
