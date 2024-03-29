﻿using backend.DTOS;
using backend.Model;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly ticketService ticketService;
        public TicketController(TestDBContext context)
        {
            this.ticketService = new ticketService(context);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTicket([FromBody] AddTicketDTO addTicketDTO)
        {
            try
            {
                var savedTicket = await ticketService.AddTicket(addTicketDTO);
                return Ok(savedTicket);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetAllTickets()
        {
            try
            {
                var ticketList = await ticketService.GetTicketList();
                var ticketListDto = ticketList.Select(ticket => new TicketDTO
                {
                    TicketId = ticket.TicketId,
                    Ticketsummary = ticket.Ticketsummary,
                    Ticketdescription = ticket.Ticketdescription,
                    AssignedTo = ticket.AssignedToNavigation != null ? new userLoggedIn
                    {
                        UserId = ticket.AssignedToNavigation.UserId,
                        Username = ticket.AssignedToNavigation.Username,
                        Fullname = ticket.AssignedToNavigation.Fullname,
                        Email = ticket.AssignedToNavigation.Email,
                        RoleName = ticket.AssignedToNavigation.Role.RoleName,
                        RoleId = ticket.AssignedToNavigation.RoleId,
                        CreatedAt = ticket.AssignedToNavigation.CreatedAt
                    }:null,
                    ReportedBy = new userLoggedIn 
                    {
                        UserId = ticket.ReportedByNavigation.UserId,
                        Username = ticket.ReportedByNavigation.Username,
                        Fullname = ticket.ReportedByNavigation.Fullname,
                        Email = ticket.ReportedByNavigation.Email,
                        RoleId = ticket.ReportedByNavigation.RoleId,
                        RoleName = ticket.ReportedByNavigation.Role.RoleName,
                        CreatedAt = ticket.ReportedByNavigation.CreatedAt
                    },
                    ProjectId = new ProjectDTO
                    {
                        ProjectId = ticket.ProjectId,
                        Projectname = ticket.Project.Projectname,
                        Projectkey = ticket.Project.Projectkey,
                        Projectdescription = ticket.Project.Projectdescription,
                        AvatarUrl = ticket.Project.AvatarUrl,
                        CreatedAt = ticket.Project.CreatedAt,
                        UpdatedAt = ticket.Project.UpdatedAt
                    },
                    Ticketpriority = ticket.Ticketpriority,
                    Ticketstatus = ticket.Ticketstatus,
                    Tickettype = ticket.Tickettype,
                    CreatedAt = ticket.CreatedAt,
                    UpdatedAt = ticket.UpdatedAt,
                }).ToList();
                return Ok(ticketListDto);
            }
            catch(Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpGet("{projectId:Guid}")]
        public async Task<ActionResult> GetTicketsByProject(Guid projectId)
        {
            try
            {
                var ticketList = await ticketService.GetTicketByProject(projectId);
                var ticketListDto = ticketList.Select(ticket => new TicketDTO
                {
                    TicketId = ticket.TicketId,
                    Ticketsummary = ticket.Ticketsummary,
                    Ticketdescription = ticket.Ticketdescription,
                    AssignedTo = ticket.AssignedToNavigation != null ? new userLoggedIn
                    {
                        UserId = ticket.AssignedToNavigation.UserId,
                        Username = ticket.AssignedToNavigation.Username,
                        Fullname = ticket.AssignedToNavigation.Fullname,
                        Email = ticket.AssignedToNavigation.Email,
                        RoleName = ticket.AssignedToNavigation.Role.RoleName,
                        RoleId = ticket.AssignedToNavigation.RoleId,
                        CreatedAt = ticket.AssignedToNavigation.CreatedAt
                    } : null,
                    ReportedBy = new userLoggedIn
                    {
                        UserId = ticket.ReportedByNavigation.UserId,
                        Username = ticket.ReportedByNavigation.Username,
                        Fullname = ticket.ReportedByNavigation.Fullname,
                        Email = ticket.ReportedByNavigation.Email,
                        RoleId = ticket.ReportedByNavigation.RoleId,
                        RoleName = ticket.ReportedByNavigation.Role.RoleName,
                        CreatedAt = ticket.ReportedByNavigation.CreatedAt
                    },
                    ProjectId = new ProjectDTO
                    {
                        ProjectId = ticket.ProjectId,
                        Projectname = ticket.Project.Projectname,
                        Projectkey = ticket.Project.Projectkey,
                        Projectdescription = ticket.Project.Projectdescription,
                        AvatarUrl = ticket.Project.AvatarUrl,
                        CreatedAt = ticket.Project.CreatedAt,
                        UpdatedAt = ticket.Project.UpdatedAt
                    },
                    Ticketpriority = ticket.Ticketpriority,
                    Ticketstatus = ticket.Ticketstatus,
                    Tickettype = ticket.Tickettype,
                    CreatedAt = ticket.CreatedAt,
                    UpdatedAt = ticket.UpdatedAt,
                }).ToList();
                return Ok(ticketListDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpGet("getticketById/{ticketId:Guid}")]
        public async Task<ActionResult> GetTicketsById(Guid ticketId)
        {
            try
            {
                var ticket = await ticketService.GetTicketById(ticketId);
                var ticketDto = new TicketDTO
                {
                    TicketId = ticket.TicketId,
                    Ticketsummary = ticket.Ticketsummary,
                    Ticketdescription = ticket.Ticketdescription,
                    AssignedTo = ticket.AssignedToNavigation != null ? new userLoggedIn
                    {
                        UserId = ticket.AssignedToNavigation.UserId,
                        Username = ticket.AssignedToNavigation.Username,
                        Fullname = ticket.AssignedToNavigation.Fullname,
                        Email = ticket.AssignedToNavigation.Email,
                        RoleName = ticket.AssignedToNavigation.Role.RoleName,
                        RoleId = ticket.AssignedToNavigation.RoleId,
                        CreatedAt = ticket.AssignedToNavigation.CreatedAt
                    } : null,
                    ReportedBy = new userLoggedIn
                    {
                        UserId = ticket.ReportedByNavigation.UserId,
                        Username = ticket.ReportedByNavigation.Username,
                        Fullname = ticket.ReportedByNavigation.Fullname,
                        Email = ticket.ReportedByNavigation.Email,
                        RoleId = ticket.ReportedByNavigation.RoleId,
                        RoleName = ticket.ReportedByNavigation.Role.RoleName,
                        CreatedAt = ticket.ReportedByNavigation.CreatedAt
                    },
                    ProjectId = new ProjectDTO
                    {
                        ProjectId = ticket.ProjectId,
                        Projectname = ticket.Project.Projectname,
                        Projectkey = ticket.Project.Projectkey,
                        Projectdescription = ticket.Project.Projectdescription,
                        AvatarUrl = ticket.Project.AvatarUrl,
                        CreatedAt = ticket.Project.CreatedAt,
                        UpdatedAt = ticket.Project.UpdatedAt
                    },
                    Ticketpriority = ticket.Ticketpriority,
                    Ticketstatus = ticket.Ticketstatus,
                    Tickettype = ticket.Tickettype,
                    CreatedAt = ticket.CreatedAt,
                    UpdatedAt = ticket.UpdatedAt,
                };
                return Ok(ticketDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpPut("{ticketId:Guid}/{status}")]
        public async Task<ActionResult> UpdateTicketStatus(Guid ticketId,string status)
        {
            try
            {
                var findTicket = await ticketService.GetTicketById(ticketId);
                if(findTicket == null)
                {
                    return NotFound(new { message = "Ticket not found" });
                }
                await ticketService.UpdateTicketStatus(findTicket, status);
                return Ok(new { message = "Ticket Updated" });

            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{ticketId:Guid}")]
        public async Task<ActionResult> UpdateTicket(Guid ticketId, [FromBody] UpdateTicketDTO updateTicketDTO)
        {
            try
            {
                var findTicket = await ticketService.GetTicketById(ticketId);
                if (findTicket == null)
                {
                    return NotFound(new { message = "Ticket not found" });
                }
                await ticketService.UpdateTicket(findTicket, updateTicketDTO);
                return Ok(new { message = "Ticket Updated" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{ticketId:Guid}")]
        public async Task<ActionResult> DeleteTicket(Guid ticketId)
        {
            try
            {
                var findTicket = await ticketService.GetTicketById(ticketId);
                if (findTicket == null)
                {
                    return NotFound(new { message = "Ticket not found" });
                }
                await ticketService.DeleteTicket(findTicket);
                return Ok(new { message = "Ticket Deleted" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
