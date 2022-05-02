using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class History
    {
        public int Id { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int IdReq { get; set; }
        public int IdStage { get; set; }

        public virtual Request IdReqNavigation { get; set; } = null!;
        public virtual Stage IdStageNavigation { get; set; } = null!;
    }
}
