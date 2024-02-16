using System;
using System.Collections.Generic;

namespace backend.Model
{
    public partial class User
    {
        public User()
        {
            AssignedProjects = new HashSet<AssignedProject>();
            TicketAssignedToNavigations = new HashSet<Ticket>();
            TicketReportedByNavigations = new HashSet<Ticket>();
        }

        public Guid UserId { get; set; }
        public string Fullname { get; set; } 
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public Guid RoleId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<AssignedProject> AssignedProjects { get; set; }
        public virtual ICollection<Ticket> TicketAssignedToNavigations { get; set; }
        public virtual ICollection<Ticket> TicketReportedByNavigations { get; set; }
    }
}
