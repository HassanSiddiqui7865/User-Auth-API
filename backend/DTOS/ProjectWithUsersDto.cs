namespace backend.DTOS
{
    public class ProjectWithUsersDto
    {
        public Guid ProjectId { get; set; }
        public string Projectname { get; set; } 
        public string AvatarUrl { get; set; }
        public string Projectkey { get; set; }
        public string Projectdescription { get; set; } 
        public List<UserDTO> Users { get; set; }    
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
