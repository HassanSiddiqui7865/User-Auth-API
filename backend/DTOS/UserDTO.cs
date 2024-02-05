namespace backend.DTOS
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsLead { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
