using backend.DTOS;
using backend.Model;

namespace backend.Interfaces
{
    public interface ITicket
    {
        Task<Ticket> AddTicket(AddTicketDTO addTicketDTO);

        Task<List<Ticket>> GetTicketList();

        Task<List<Ticket>> GetTicketByProject(Guid projectId);

        Task<Ticket> GetTicketById(Guid ticketId);

        Task UpdateTicketStatus(Ticket ticket,string status);
    }
}
