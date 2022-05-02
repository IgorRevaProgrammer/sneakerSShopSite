using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class Request
    {
        public Request()
        {
            Histories = new HashSet<History>();
            RequestGoods = new HashSet<RequestGood>();
        }

        public int Id { get; set; }
        public string IdUser { get; set; } = null!;
        public string FullPrice { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public int IdDelivery { get; set; }
        public int? IdExec { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Delivery IdDeliveryNavigation { get; set; } = null!;
        public virtual Executor? IdExecNavigation { get; set; }
        public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<RequestGood> RequestGoods { get; set; }
    }
}
