using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.Models;

namespace Services
{
    public class BrandService
    {
        ILogger<BrandService> logger;
        ApplicationDbContext context;
        public BrandService(ILogger<BrandService> _logger,
        ApplicationDbContext _context)
        {
            logger = _logger;
            context = _context;
        }
        public async Task<List<Brand>> GetAllAsync()
        {
            return await context.Brands.ToListAsync();
        }
    }
}
