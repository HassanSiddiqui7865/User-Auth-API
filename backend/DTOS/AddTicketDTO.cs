namespace backend.DTOS
{
    public class AddTicketDTO
    {
        public string Ticketsummary { get; set; }
        public string? Ticketdescription { get; set; }
        public Guid? AssignedTo { get; set; }
        public Guid ReportedBy { get; set; }
        public Guid ProjectId { get; set; }
        public string Ticketpriority { get; set; } 
        public string Ticketstatus { get; set; }
        public string Tickettype { get; set; }
    }
}
