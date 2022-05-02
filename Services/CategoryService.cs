using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.Models;

namespace Services
{
    public class CategoryService
    {
        ApplicationDbContext context;
        ILogger<CategoryService> logger;
        public CategoryService(ApplicationDbContext _context,
            ILogger<CategoryService> _logger)
        {
            context = _context;
            logger = _logger;
        }
        public async Task<Category> FindByNameSync(string name)
        {
            return await context.Categories
                .FirstOrDefaultAsync(c=>c.CategoryName == name);
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await context.Categories.ToListAsync();
        }
    }
}
