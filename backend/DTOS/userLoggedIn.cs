namespace backend.DTOS
{
    public class userLoggedIn
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Userrole { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
