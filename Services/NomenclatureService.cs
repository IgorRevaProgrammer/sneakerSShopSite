using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class NomenclatureService
    {
        ApplicationDbContext context;
        ILogger<NomenclatureService> logger;
        public NomenclatureService(ApplicationDbContext _context,
            ILogger<NomenclatureService> _logger)
        {
            logger= _logger;
            context = _context;
        }
        public async Task<List<Nomenclature>> GetAllAvailableAsync()
        {
            return await context.Nomenclatures
                .Where(n => n.IsAvailable == true).ToListAsync();
        }
        public async Task<Nomenclature> GetAsync(int id)
        {
            return await context.Nomenclatures
                .Include(n => n.IdBrandNavigation)
                .Include(n => n.IdCategoryNavigation)
                .Include(n => n.Goods)
                .FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}
