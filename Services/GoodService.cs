using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.Models;

namespace Services
{
    public class GoodService
    {
        ApplicationDbContext context;
        ILogger<GoodService> logger;
        public GoodService(ApplicationDbContext _context,
            ILogger<GoodService> _logger)
        {
            context = _context;
            logger = _logger;
        }
        public async Task<List<Good>>GetAllAsync()
        {
            return await context.Goods
                .Include(g => g.IdNomNavigation)
                .ThenInclude(n => n.IdBrandNavigation)
                .ToListAsync();
        }
        public async Task<List<Good>>GetAllAsync(IEnumerable<int> ids)
        {
            return await context.Goods
                .Include(g => g.IdNomNavigation)
                .ThenInclude(n => n.IdBrandNavigation)
                .Where(g => ids.Contains(g.Id)).ToListAsync();
        }
        public void Update(Good good)
        {
            if (good != null)
            {
                context.Update(good);
                context.SaveChanges();
                logger.LogInformation("Good updated successfully");
            }
            else logger.LogError("can't update good because of null");
        }
        public async Task<Good> GetAsync(int id)
        {
            return await context.Goods
                .FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
