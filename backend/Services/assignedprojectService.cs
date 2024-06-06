using backend.DTOS;
using backend.Interfaces;
using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class AssignedprojectService : IAssignedProject
    {
        private readonly TMSBackupContext context;
        public AssignedprojectService(TMSBackupContext context)
        {
            this.context = context;   
        }

        public async Task<AssignedProjectDTO> CreateAssignedProject(AddAssignedProject addAssignedProject)
        {
            var newassigned = new AssignedProject
            {
                ProjectAssignedId = Guid.NewGuid(),
                UserId = addAssignedProject.UserId,
                IsLead = addAssignedProject.IsLead,
                ProjectId = addAssignedProject.ProjectId,
            };
            var savedassigned = new AssignedProjectDTO
            {
                ProjectAssignedId = newassigned.ProjectAssignedId,
                UserId = newassigned.UserId,
                IsLead = newassigned.IsLead,
                ProjectId = newassigned.ProjectId,
            };
            context.Add(newassigned);
            await context.SaveChangesAsync();
            return savedassigned;
        }

        public async Task DeleteAssigned(AssignedProject assignedProject)
        {
            context.AssignedProjects.Remove(assignedProject);
            await context.SaveChangesAsync();
        }

        public async Task<AssignedProject> GetAssigned(Guid projectId, Guid userId)
        {
            var assignedProject = await context.AssignedProjects
                .FirstOrDefaultAsync(e => e.UserId == userId && e.ProjectId == projectId);
            return assignedProject;
        }
        public async Task UpdateLeadFalse(AssignedProject assignedProject)
        {
            assignedProject.IsLead = false;
            await context.SaveChangesAsync();
        }
        public async Task UpdateLeadTrue(AssignedProject assignedProject)
        {
            assignedProject.IsLead = true;
            await context.SaveChangesAsync();
        }
    }
}
