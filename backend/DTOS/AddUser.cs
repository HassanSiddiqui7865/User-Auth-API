namespace backend.DTOS
{
    public class AddUser
    {
        public string Fullname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public Guid RoleId { get; set; }
    }
}
