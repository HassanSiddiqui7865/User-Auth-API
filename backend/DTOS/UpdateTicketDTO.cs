namespace backend.DTOS
{
    public class UpdateTicketDTO
    {
        public string Ticketsummary { get; set; }
        public string? Ticketdescription { get; set; }
        public Guid? AssignedTo { get; set; }
        public string Ticketpriority { get; set; }
        public string Tickettype { get; set; }
    }
}
