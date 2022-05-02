using Models.Models;

namespace sneakerSShopSite.Models.ViewModels
{
    public class FiltersVM
    {
        public List<Sort> sorts { get; set; } = null!;
        public List<Brand>brands { get; set; } = null!;
        public List<Category> categories { get; set; } = null!;
        public int selectedSortId { get; set; }
        public int selectedBrandId { get; set; }
        public int selectedCategoryId { get; set; }
        public int selectedGender { get; set; }
    }
}
