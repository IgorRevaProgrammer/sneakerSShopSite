using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.Models;

namespace Services
{
    public class ImageService
    {
        ApplicationDbContext context;
        ILogger<ImageService> logger;
        public ImageService(ApplicationDbContext _context,
            ILogger<ImageService> _logger)
        {
            context = _context;
            logger = _logger;
        }
        public async Task<Image> GetImageAsync(int id)
        {
            return await context.Images.FirstOrDefaultAsync(i => i.IdNom == id);
        }
    }
}
