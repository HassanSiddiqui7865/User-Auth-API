namespace backend.DTOS
{
    public class TicketDTO
    {
        public Guid TicketId { get; set; }
        public string Ticketsummary { get; set; }
        public string? Ticketdescription { get; set; }
        public userLoggedIn? AssignedTo { get; set; }
        public userLoggedIn ReportedBy { get; set; }
        public ProjectDTO ProjectId { get; set; }
        public string Ticketpriority { get; set; } 
        public string Ticketstatus { get; set; } 
        public string Tickettype { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
