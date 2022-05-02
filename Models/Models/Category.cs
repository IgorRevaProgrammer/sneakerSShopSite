using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public partial class Category
    {
        public Category()
        {
            Nomenclatures = new HashSet<Nomenclature>();
        }
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public virtual ICollection<Nomenclature> Nomenclatures { get; set; }
    }
}
