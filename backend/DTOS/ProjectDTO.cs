namespace backend.DTOS
{
    public class ProjectDTO
    {
        public Guid ProjectId { get; set; }
        public string Projectname { get; set; }
        public string Projectdescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
