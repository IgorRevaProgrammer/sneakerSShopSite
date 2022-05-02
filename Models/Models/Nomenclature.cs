using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class Nomenclature
    {
        public Nomenclature()
        {
            Goods = new HashSet<Good>();
            Images = new HashSet<Image>();
            LikedGoodNomenclatures = new HashSet<LikedGoodNomenclature>();
        }
        public int Id { get; set; }
        public int IdCategory { get; set; }
        public int IdBrand { get; set; }
        public string Model { get; set; } = null!;
        public bool Sex { get; set; }
        public string Price { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public bool IsAvailable { get; set; }

        public virtual Brand IdBrandNavigation { get; set; } = null!;
        public virtual Category IdCategoryNavigation { get; set; } = null!;
        public virtual ICollection<Good> Goods { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<LikedGoodNomenclature> LikedGoodNomenclatures { get; set; }
    }
}
