using Microsoft.EntityFrameworkCore;
using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext dataContext, ICurrentTime currentTime) : base(dataContext, currentTime)
        {
            _context = dataContext;
        }

        public async Task<ICollection<Category>> GetCategories()
        {
            return await _context.Category.Include(x => x.SubCategories).ToListAsync();
        }

        public void DeleteCategory(Category category)
        {
            _context.Remove(category);
        }

        public async Task<Category> GetCategoryById(Guid categoryId)
        {
            return await _context.Category.Include(x => x.SubCategories).Where(p => p.Id == categoryId).FirstOrDefaultAsync();
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            return await _context.Category.Include(x => x.SubCategories).Where(p => p.CategoryName == name).FirstOrDefaultAsync();
        }
    }
}
