using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Nomenclatures = new HashSet<Nomenclature>();
        }

        public int Id { get; set; }
        public string BrandName { get; set; } = null!;

        public virtual ICollection<Nomenclature> Nomenclatures { get; set; }
    }
}
