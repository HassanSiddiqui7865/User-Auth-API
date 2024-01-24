using System;
using System.Collections.Generic;

namespace backend.Model
{
    public partial class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public string Userrole { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
