using System;
using System.Collections.Generic;

namespace backend.Model
{
    public partial class Project
    {
        public Project()
        {
            AssignedProjects = new HashSet<AssignedProject>();
        }

        public Guid ProjectId { get; set; }
        public string Projectname { get; set; } = null!;
        public string Projectdescription { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<AssignedProject> AssignedProjects { get; set; }
    }
}
