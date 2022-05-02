namespace sneakerSShopSite.Models.ViewModels
{
    public class CartVM
    {
        public List<int> idsGoods { get; set; } = null!;
        public List<int> counts { get; set; } = null!;
        public string adress { get; set; } = null!;
        public string delivery { get; set; } = null!;
    }
}
