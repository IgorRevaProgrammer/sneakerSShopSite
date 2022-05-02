using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class RequestGood
    {
        public int Id { get; set; }
        public int IdReq { get; set; }
        public int IdGood { get; set; }
        public int Number { get; set; }

        public virtual Good IdGoodNavigation { get; set; } = null!;
        public virtual Request IdReqNavigation { get; set; } = null!;
    }
}
