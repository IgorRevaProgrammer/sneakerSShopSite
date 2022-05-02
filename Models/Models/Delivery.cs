using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class Delivery
    {
        public Delivery()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string DeliveryName { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }
    }
}
