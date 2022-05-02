using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.Models;

namespace Services
{
    public class DeliveryService
    {
        ApplicationDbContext context;
        ILogger<DeliveryService> logger;
        public DeliveryService(ApplicationDbContext _context,
            ILogger<DeliveryService> _logger)
        {
            context = _context;
            logger = _logger;
        }
        public async Task<Delivery> FindByNameAsync(string name)
        {
            return await context.Deliveries
                .FirstOrDefaultAsync(d => d.DeliveryName == name);
        }
    }
}
