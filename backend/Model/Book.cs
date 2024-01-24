using System;
using System.Collections.Generic;

namespace backend.Model
{
    public partial class Book
    {
        public Guid BookId { get; set; }
        public string BookName { get; set; } = null!;
        public string BookAuthor { get; set; } = null!;
        public string BookDescription { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
