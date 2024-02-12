using System;
using System.Collections.Generic;

namespace backend.Model
{
    public partial class Project
    {
        public Project()
        {
            AssignedProjects = new HashSet<AssignedProject>();
            Tickets = new HashSet<Ticket>();
        }

        public Guid ProjectId { get; set; }
        public string Projectname { get; set; } = null!;
        public string Projectdescription { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string AvatarUrl { get; set; } = null!;
        public string Projectkey { get; set; } = null!;

        public virtual ICollection<AssignedProject> AssignedProjects { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
