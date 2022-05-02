using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services;
using sneakerSShopSite.Models.ViewModels;

namespace sneakerSShopSite.Controllers
{
    class PriceSort : IComparer<String>
    {
        public int Compare(string? x, string? y)
        {
            return int.Parse(x) - int.Parse(y);
        }
    }
    public class PagesController : Controller
    {
        NomenclatureService nomenclatureService;
        BrandService brandService;
        CategoryService categoryService;
        ImageService imageService;
        CartService cartService;
        GoodService goodService;
        private readonly UserManager<ApplicationUser> userManager;
        public PagesController(NomenclatureService _nomenclatureService,
            BrandService _brandService,
            CategoryService _categoryService,
            ImageService _imageService,
            CartService _cartService,
            GoodService _goodService,
            UserManager<ApplicationUser> _userManager)
        {
            nomenclatureService = _nomenclatureService;
            brandService = _brandService;
            categoryService = _categoryService;
            imageService = _imageService;
            cartService = _cartService;
            goodService= _goodService;
            userManager = _userManager;
        }
        public async Task<IActionResult> Good(int Id)
        {
            if (Id == 0)
                return BadRequest();

            var good = await nomenclatureService.GetAsync(Id);
            return View(good);
        }
        public IActionResult Filter(int sort = 0, int brand = 0, int category = 0, int sex = 0)
        {
            string filters = sort.ToString() + ";" + brand.ToString() + ";" + category.ToString() + ";" + sex.ToString();
            return RedirectToAction("Goods", new { filters = filters, idsWithFoundModels = "" });
        }
        public async Task<IActionResult> Goods(string filters,string idsWithFoundModels)
        {
            if (filters == null && idsWithFoundModels == null)
                return BadRequest();

            var nomenclatures = await nomenclatureService.GetAllAvailableAsync();
            int sort = 0;
            int idBrand = 0;
            int idCateg = 0;
            int isex = 0;
            bool sex = false;
            var filtersArr = filters.Split(";");
            sort = int.Parse(filtersArr[0]);
            idBrand = int.Parse(filtersArr[1]);
            idCateg = int.Parse(filtersArr[2]);
            isex = int.Parse(filtersArr[3]);
            if (isex != 0) 
                sex = isex == 1 ? false : true;
            int[] ids = Array.Empty<int>();
            List<Nomenclature> nomenclatures2 = new();
            if (idsWithFoundModels != null)
            {
                var strIds = idsWithFoundModels.Split(";");
                ids = new int[strIds.Length - 1];
                for (int i = 0; i < strIds.Length - 1; i++)
                    ids[i] = int.Parse(strIds[i]);
                nomenclatures2.AddRange(nomenclatures.Where(n => ids.Contains(n.Id)).ToList());
            }
            List<Nomenclature> goods = new();
            if (nomenclatures2.Any())
            {
                goods.AddRange(nomenclatures2
               .Where(n =>
               {
                   if (idBrand != 0)
                       if (n.IdBrand != idBrand) return false;
                   if (idCateg != 0)
                       if (n.IdCategory != idCateg) return false;
                   if (isex != 0)
                       if (n.Sex != sex) return false;
                   return true;
               }).ToList());
            }
            else
            {
                if (idBrand != 0 || idCateg != 0 || isex != 0)
                {
                    goods.AddRange(nomenclatures
                   .Where(n =>
                   {
                       if (idBrand != 0)
                           if (n.IdBrand != idBrand) return false;
                       if (idCateg != 0)
                           if (n.IdCategory != idCateg) return false;
                       if (isex != 0)
                           if (n.Sex != sex) return false;
                       return true;
                   }).ToList());
                }
            }
            List<Nomenclature> orderedGoods = new();
            if (sort == 1)
            {
                orderedGoods.AddRange(goods
               .OrderByDescending(n => n.Price, new PriceSort())
               .ToList());
            }
            else if(sort == 2)
            {
                orderedGoods = goods
               .OrderBy(n => n.Price, new PriceSort())
               .ToList();
            }

            FiltersVM fvm = new FiltersVM();
            var sorts = new List<Sort>();
            sorts.Add(new Sort { Id = 1, sortName = "Price high to low" });
            sorts.Add(new Sort { Id = 2, sortName = "Price low to high" });
            fvm.sorts = sorts;
            fvm.brands = await brandService.GetAllAsync();
            fvm.categories = await categoryService.GetAllAsync();
            fvm.selectedSortId = sort;
            fvm.selectedBrandId = idBrand;
            fvm.selectedCategoryId = idCateg;
            fvm.selectedGender = isex;
            ViewBag.FiltersVM = fvm;
            if (orderedGoods.Any())
                return View(orderedGoods);
            else return View(goods);
        }
        public async Task<FileContentResult> GetImage(int Id)
        {
            var img = await imageService.GetImageAsync(Id);
            return File(img.ImgData, img.MimeType);
        }
        [HttpPost("goodId")]
        public async Task<IActionResult> AddToCart(int goodId = 0)
        {
            if (goodId == 0) return BadRequest();

            var currentUser = await userManager.FindByNameAsync(User.Identity?.Name);
 
            if (cartService.Contains(currentUser.Id,goodId))
                return RedirectToAction("Index", "Home");

            var good = await goodService.GetAsync(goodId);
            if (good == null) return NotFound();
            CartGood cg = new CartGood();
            cg.IdUserNavigation = currentUser;
            cg.IdGoodNavigation = good;
            cartService.Create(cg);
            return RedirectToAction("Index", "Home");
        }
    }
}
