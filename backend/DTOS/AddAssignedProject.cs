namespace backend.DTOS
{
    public class AddAssignedProject
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public bool IsLead { get; set; }
    }
}
