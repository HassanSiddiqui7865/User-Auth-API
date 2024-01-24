namespace backend.DTOS
{
    public class AddBook
    {
        public string BookName { get; set; } = null!;
        public string BookAuthor { get; set; } = null!;
        public string BookDescription { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
    }
}
