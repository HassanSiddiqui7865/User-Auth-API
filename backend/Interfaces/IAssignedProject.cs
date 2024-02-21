using backend.DTOS;
using backend.Model;

namespace backend.Interfaces
{
    public interface IAssignedProject
    {
        Task<AssignedProject> CreateAssignedProject(AddAssignedProject addAssignedProject);

        Task <AssignedProject> GetAssigned(Guid projectId,Guid userId);

        Task DeleteAssigned(AssignedProject assignedProject);

        Task UpdateLeadFalse(AssignedProject assignedProject);

        Task UpdateLeadTrue(AssignedProject assignedProject);


    }
}
