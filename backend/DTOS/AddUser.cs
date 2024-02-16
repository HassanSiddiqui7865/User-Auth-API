namespace backend.DTOS
{
    public class AddUser
    {
        public string Fullname { get; set; }
        public string Username { get; set; } 
        public string Email { get; set; }
        public string Pass { get; set; } 
        public Guid RoleId { get; set; }
    }
}
