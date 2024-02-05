using backend.DTOS;
using backend.Model;

namespace backend.Interfaces
{
    public interface IAssignedProject
    {
        Task<AssignedProjectDTO> CreateAssignedProject(AddAssignedProject addAssignedProject);

        Task<List<Project>> GetAssignedProjects(Guid id);

        Task <AssignedProject> GetAssigned(Guid projectId,Guid userId);

        Task DeleteAssigned(AssignedProject assignedProject);

    }
}
