using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class Executor
    {
        public Executor()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsLocked { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
