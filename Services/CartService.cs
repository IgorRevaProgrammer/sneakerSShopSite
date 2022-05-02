using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.Models;

namespace Services
{
    public class CartService
    {
        ApplicationDbContext context;
        ILogger<CartService> logger;
        public CartService(ApplicationDbContext _context,
            ILogger<CartService> _logger)
        {
            context = _context;
            logger = _logger;
        }
        public async Task<List<CartGood>> GetAllAsync(string userId,bool isGoodInclude=false)
        {
            if (isGoodInclude)
            {
                return await context.CartGoods
                    .Include(cg => cg.IdGoodNavigation)
                    .ThenInclude(g=>g.IdNomNavigation)
                    .ThenInclude(n=>n.IdBrandNavigation)
                    .Where(cg => cg.IdUser == userId)
                    .ToListAsync();
            }
            else
            {
                return await context.CartGoods
                   .Where(cg => cg.IdUser == userId)
                   .ToListAsync();
            }
        }
        public async Task<CartGood> GetAsync(int goodId,string userId, bool isGoodInclude = false)
        {
            if (isGoodInclude)
            {
                var cartGood = await context.CartGoods
                    .Include(cg => cg.IdGoodNavigation)
                    .FirstOrDefaultAsync(cg => cg.IdUser == userId &&
                    cg.IdGood == goodId);
                if (cartGood == null)
                    logger.LogWarning("CartGood not found");
                return cartGood;
            }
            else
            {
                var cartGood = await context.CartGoods
                   .FirstOrDefaultAsync(cg => cg.IdUser == userId &&
                   cg.IdGood == goodId);
                if (cartGood == null)
                    logger.LogWarning("CartGood not found");
                return cartGood;
            }
        }
        public void DeleteAll(string userId)
        {
            var cartGoods = context.CartGoods
                .Where(cg => cg.IdUser == userId);
            context.CartGoods.RemoveRange(cartGoods);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var cartGood = context.CartGoods
                .FirstOrDefault(cg => cg.Id == id);
            if (cartGood != null)
            {
                context.CartGoods.Remove(cartGood);
                context.SaveChanges();
                logger.LogInformation("CartGood deleted successfully");
            }
            else logger.LogError("Can't delete cartGood because of null");
        }
        public void Create(CartGood cartGood)
        {
            try
            {
                context.CartGoods.Add(cartGood);
                context.SaveChanges();
                logger.LogInformation("Good added to cart successfully");
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "can't add good to cart");
                return;
            }
        }
        public bool Contains(string idUser,int idGood)
        {
            return context.CartGoods
                .Where(cg => cg.IdUser == idUser 
                && cg.IdGood == idGood).Any();
        }
    }
}
