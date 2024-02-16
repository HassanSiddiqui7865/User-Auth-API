using backend.DTOS;
using backend.Interfaces;
using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ticketService : ITicket
    {
        private readonly TestDBContext context;
        public ticketService(TestDBContext context)
        {
            this.context = context; 
        }

        public async Task<Ticket> AddTicket(AddTicketDTO addTicketDTO)
        {
            var newticket = new Ticket
            {
                TicketId = Guid.NewGuid(),
                Ticketsummary = addTicketDTO.Ticketsummary,
                Ticketdescription = addTicketDTO.Ticketdescription,
                AssignedTo = addTicketDTO.AssignedTo,
                ReportedBy = addTicketDTO.ReportedBy,
                ProjectId = addTicketDTO.ProjectId,
                Ticketstatus = addTicketDTO.Ticketstatus,
                Ticketpriority = addTicketDTO.Ticketpriority,
                Tickettype = addTicketDTO.Tickettype,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            context.Tickets.Add(newticket);
            await context.SaveChangesAsync();
            return newticket;
        }

        public async Task DeleteTicket(Ticket ticket)
        {
            context.Tickets.Remove(ticket);
            await context.SaveChangesAsync();
        }

        public async Task<Ticket> GetTicketById(Guid ticketId)
        {
            var ticket = await context.Tickets
               .Include(t => t.AssignedToNavigation)
               .ThenInclude(r => r.Role)
               .Include(t => t.ReportedByNavigation)
               .ThenInclude(r => r.Role)
               .Include(t => t.Project)
               .FirstOrDefaultAsync(t => t.TicketId == ticketId);

            return ticket;
        }

        public async Task<List<Ticket>> GetTicketByProject(Guid projectId)
        {
            var ticketList = await context.Tickets
               .Include(t => t.AssignedToNavigation)
               .ThenInclude(r => r.Role)
               .Include(t => t.ReportedByNavigation)
               .ThenInclude(r => r.Role)
               .Include(t => t.Project)
               .Where(t=>t.ProjectId == projectId)
               .ToListAsync();

            return ticketList;
        }

        public async Task<List<Ticket>> GetTicketList()
        {
            var ticketList = await context.Tickets
              .Include(t => t.AssignedToNavigation)
              .ThenInclude(r => r.Role)
              .Include(t => t.ReportedByNavigation)
              .ThenInclude(r => r.Role)
              .Include(t => t.Project)
              .ToListAsync();
            return ticketList;
        }

        public async Task UpdateTicket(Ticket ticket, UpdateTicketDTO updateTicketDTO)
        {
            ticket.Ticketsummary =  updateTicketDTO.Ticketsummary;
            ticket.Ticketdescription = updateTicketDTO.Ticketdescription;
            ticket.Ticketpriority = updateTicketDTO.Ticketpriority;
            ticket.Tickettype = updateTicketDTO.Tickettype;
            ticket.AssignedTo = updateTicketDTO.AssignedTo;
            ticket.UpdatedAt = DateTime.Now;
            await context.SaveChangesAsync();
        }

        public async Task UpdateTicketStatus(Ticket ticket,string status)
        {
            ticket.Ticketstatus = status;
            ticket.UpdatedAt = DateTime.Now;
            await context.SaveChangesAsync();
        }
    }
}
