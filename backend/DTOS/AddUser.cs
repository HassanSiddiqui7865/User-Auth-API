namespace backend.DTOS
{
    public class AddUser
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public string Userrole { get; set; } = null!;
    }
}
