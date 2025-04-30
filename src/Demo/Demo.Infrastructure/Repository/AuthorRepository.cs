using Demo.Domain.Entities;
using Demo.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Repository
{
    public class AuthorRepository : Repository<Author, Guid>, IAuthorRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AuthorRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
