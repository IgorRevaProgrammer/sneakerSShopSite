using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.Models;

namespace Services
{
    public class RequestService
    {
        ApplicationDbContext context;
        ILogger<RequestService> logger;
        public RequestService(ApplicationDbContext _context,
             ILogger<RequestService> _logger)
        {
            context = _context;
            logger = _logger;
        }
        public async Task<List<Request>> GetAllAsync(string userId)
        {
            return await context.Requests
                .Include(r=>r.IdDeliveryNavigation)
                .Include(r => r.RequestGoods)
                .ThenInclude(r => r.IdGoodNavigation)
                .ThenInclude(g => g.IdNomNavigation)
                .ThenInclude(g => g.IdBrandNavigation)
                .Where(r => r.IdUser == userId)
                .ToListAsync();
        }
        public void Create(Request req)
        {
            try
            {
                context.Requests.Add(req);
                context.SaveChanges();
                logger.LogInformation("Request created successfully");
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "can't create request");
                return;
            }
        }
    }
}
