using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repository;
using CleanArchitecture.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private DbSet<Category> _dataset;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _dataset = context.Set<Category>();
        }
    }
}
