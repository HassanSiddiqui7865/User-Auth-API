using System;
using System.Collections.Generic;

namespace backend.Model
{
    public partial class Ticket
    {
        public Guid TicketId { get; set; }
        public string Ticketsummary { get; set; } = null!;
        public string? Ticketdescription { get; set; }
        public Guid? AssignedTo { get; set; }
        public Guid ReportedBy { get; set; }
        public Guid ProjectId { get; set; }
        public string Ticketpriority { get; set; } = null!;
        public string Ticketstatus { get; set; } = null!;
        public string Tickettype { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual User? AssignedToNavigation { get; set; }
        public virtual Project Project { get; set; } = null!;
        public virtual User ReportedByNavigation { get; set; } = null!;
    }
}
