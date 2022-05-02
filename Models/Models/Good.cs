using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class Good
    {
        public Good()
        {
            RequestGoods = new HashSet<RequestGood>();
            CartGoods = new HashSet<CartGood>();
        }

        public int Id { get; set; }
        public int IdNom { get; set; }
        public int Size { get; set; }
        public int Amount { get; set; }

        public virtual Nomenclature IdNomNavigation { get; set; } = null!;
        public virtual ICollection<RequestGood> RequestGoods { get; set; }
        public virtual ICollection<CartGood> CartGoods { get; set; }
    }
}
