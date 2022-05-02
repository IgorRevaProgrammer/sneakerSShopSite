using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Services;
using Models.Models;
using sneakerSShopSite.Models;
using sneakerSShopSite.Models.ViewModels;

namespace sneakerSShopSite.Controllers
{
    public class HomeController : Controller
    {
        GoodService goodService;
        CategoryService categoryService;
        BrandService brandService;
        NomenclatureService nomenclatureService;
        DeliveryService deliveryService;
        RequestService requestService;
        CartService cartService;
        UserManager<ApplicationUser> userManager;
        public HomeController(GoodService _goodService,
               CategoryService _categoryService,
               BrandService _brandService,
               NomenclatureService _nomenclatureService,
               DeliveryService _deliveryService,
               RequestService _requestService,
               CartService _cartService,
               UserManager<ApplicationUser> _userManager)
        {
            goodService = _goodService;
            brandService = _brandService;
            categoryService = _categoryService;
            nomenclatureService= _nomenclatureService;
            requestService = _requestService;
            userManager = _userManager;
            deliveryService = _deliveryService;
            cartService = _cartService;
        }
        [HttpGet]
        public IActionResult Index()
        {         
            return View();
        }
        [HttpGet]
        public IActionResult LikedGoods()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var currentUser = await userManager
                .FindByNameAsync(User.Identity?.Name);

            var cartGoods = await cartService.GetAllAsync(currentUser.Id, true);

            if (cartGoods.Any())
                ViewBag.cartGoods = cartGoods;

            return View();
        }
        public async Task<IActionResult> SelectedCategory(bool sex,string category)
        {
            string filters = "";
            if (category != "all") 
            {
                var foundCategory = await categoryService.FindByNameSync(category);
                if (foundCategory == null)
                    return NotFound();
      
                filters = "0;0;" + foundCategory.Id.ToString() + ";" + (sex == false ? "1" : "2");
            }
            else if(category == "all")
            {
                filters = "0;0;0;" + (sex == false ? "1" : "2");               
            }
            else return BadRequest();
            return RedirectToAction("Goods", "Pages", new { filters = filters, idsWithFoundModels = "" });
        }
        [HttpPost]
        public async Task<IActionResult> Search(string searchStr)
        {
            if (searchStr == null)
                return BadRequest();
            var splitedStr = searchStr.Split(" ");

            List<Brand> brands = await brandService.GetAllAsync();
            List<Category> categories = await categoryService.GetAllAsync();
            List<Nomenclature> nomenclatures = await nomenclatureService.GetAllAvailableAsync();

            if (categories == null || brands == null || nomenclatures == null) return StatusCode(500);
            int idCateg=0;
            int idBrand=0;
            int isex = 0;
            bool sex = false;
            string idsWithFoundModels = "";
            using (CountdownEvent cde = new CountdownEvent(4))
            {
                ThreadPool.QueueUserWorkItem(
                tp =>
                {
                    foreach (var str in splitedStr)
                    {
                        var cat = categories.FirstOrDefault(c => c.CategoryName.ToLower() == str.ToLower());
                        if (cat != null)
                        {
                            idCateg = cat.Id;
                            break;
                        }                  
                    }
                    cde.Signal();
                });
                ThreadPool.QueueUserWorkItem(
                tp =>
                {
                    foreach (var str in splitedStr)
                    {
                        var br = brands.FirstOrDefault(c => c.BrandName.ToLower() == str.ToLower());
                        if (br != null)
                        {
                            idBrand = br.Id;
                            break;
                        }
                    }
                    cde.Signal();
                });
                ThreadPool.QueueUserWorkItem(
                tp =>
                {
                    foreach (var str in splitedStr)
                    {
                        if (str.ToLower() == "women" ||
                        str.ToLower() == "woman" ||
                        str.ToLower() == "women's" ||
                        str.ToLower() == "womens")
                        {
                            sex = false;
                            isex = 1;
                            break;
                        }
                        if (str.ToLower() == "men" ||
                        str.ToLower() == "man" ||
                        str.ToLower() == "men's" ||
                        str.ToLower() == "mens")
                        {
                            sex = true;
                            isex = 2;
                            break;
                        }
                    }
                    cde.Signal();
                });
                ThreadPool.QueueUserWorkItem(
                tp =>
                {
                    int[] counts = new int[nomenclatures.Count];
                    for (int i = 0; i < nomenclatures.Count; i++)
                    {
                        var modelArr = nomenclatures[i].Model.Split(" ");
                        int count = 0;
                        foreach (var str in splitedStr)
                        {
                            foreach (var item in modelArr)
                            {
                                if (item.ToLower().Contains(str.ToLower()))
                                {
                                    count++;
                                    break;
                                }
                            }
                        }
                        counts[i] = count;
                    }
                    for (int i = 0; i < counts.Length; i++)
                        if (counts[i] == counts.Max() && counts.Max() != 0)
                            idsWithFoundModels += nomenclatures[i].Id.ToString() + ";";
                    cde.Signal();
                });
                cde.Wait();
            }
            string filters = "0;" + idBrand.ToString() + ";" + idCateg.ToString() + ";" + isex.ToString();
           
            return RedirectToAction("Goods", "Pages", new { filters = filters, idsWithFoundModels = idsWithFoundModels });
        }
        [HttpPost]
        public async Task<IActionResult> AddRequest(CartVM cvm)
        {
            if (cvm == null) return BadRequest();
  
            var curUser = await userManager.FindByNameAsync(User.Identity?.Name);
            var goods = await goodService.GetAllAsync(cvm.idsGoods);

            double fullPrice = 0;
            for (int i = 0; i < goods.Count; i++)
                fullPrice += double.Parse(goods[i].IdNomNavigation.Price) * cvm.counts[i];

            History history = new History();
            history.IdStage = 1;

            var delivery = await deliveryService.FindByNameAsync(cvm.delivery);

            Request req = new Request();
            req.IdUser = curUser.Id;
            req.FullPrice = fullPrice.ToString();
            req.Adress = cvm.adress;
            req.IdDelivery = delivery.Id;
            for (int i = 0; i < goods.Count; i++)
                req.RequestGoods
                .Add(new RequestGood()
                { IdGood = goods[i].Id, Number = cvm.counts[i] });
            req.Histories.Add(history);

            requestService.Create(req);

            cartService.DeleteAll(curUser.Id);

            for (int i = 0; i < goods.Count; i++)
            {
                goods[i].Amount -= cvm.counts[i];
                goodService.Update(goods[i]);
            }
            return RedirectToAction("Index");
        }
        public IActionResult DeleteFromCart(int id)
        {
            if (id == 0) return BadRequest();

            cartService.Delete(id);
            return RedirectToAction("Cart");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}