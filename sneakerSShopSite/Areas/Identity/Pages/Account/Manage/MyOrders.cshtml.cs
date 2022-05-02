using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Models.Models;
using Services;

namespace sneakerSShopSite.Areas.Identity.Pages.Account.Manage
{
    public class MyOrdersModel : PageModel
    {
        RequestService requestService;
        private readonly UserManager<ApplicationUser> userManager;
        public List<Request>? requests { get; set; }
        public MyOrdersModel(RequestService _requestService,
            UserManager<ApplicationUser> _userManager)
        {
            requestService= _requestService;
            userManager = _userManager;
        }
        public async Task OnGetAsync()
        {
            var user = await userManager.FindByNameAsync(User.Identity?.Name);
            requests = await requestService.GetAllAsync(user.Id);
        }
    }
}
