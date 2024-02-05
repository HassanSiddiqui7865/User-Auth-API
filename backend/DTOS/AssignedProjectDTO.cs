namespace backend.DTOS
{
    public class AssignedProjectDTO
    {
        public Guid ProjectAssignedId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ProjectId { get; set; }

        public bool IsLead { get; set; }
    }
}
