using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class Stage
    {
        public Stage()
        {
            Histories = new HashSet<History>();
        }

        public int Id { get; set; }
        public string StageName { get; set; } = null!;

        public virtual ICollection<History> Histories { get; set; }
    }
}
