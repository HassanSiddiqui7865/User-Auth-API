namespace backend.DTOS
{
    public class UserWithProjectsDto
    {
        public Guid UserId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Guid? RoleId { get; set; }

        public string RoleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ProjectDTO> Projects { get; set; }
    }
}
