using System;
using System.Collections.Generic;

namespace backend.Model
{
    public partial class Project
    {
        public Guid ProjectId { get; set; }
        public string Projectshortname { get; set; } = null!;
        public string Projectfullname { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
