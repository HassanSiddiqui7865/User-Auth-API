namespace backend.DTOS
{
    public class ProjectWithUsersDto
    {
        public Guid ProjectId { get; set; }
        public string Projectname { get; set; } = null!;
        public string Projectdescription { get; set; } = null!;
        public List<UserDTO> Users { get; set; }    
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
