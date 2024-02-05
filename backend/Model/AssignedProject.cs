using System;
using System.Collections.Generic;

namespace backend.Model
{
    public partial class AssignedProject
    {
        public Guid ProjectAssignedId { get; set; }
        public Guid UserId { get; set; }
        public bool IsLead { get; set; }
        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
