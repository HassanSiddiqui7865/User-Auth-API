using backend.DTOS;
using backend.Model;

namespace backend.Interfaces
{
    public interface IProject
    {
        Task<Project> CreateProject(AddProject addproject);
        Task<List<Project>> GetAllProjects();
        Task DeleteProject(Project project);
        Task<Project> GetProjectById(Guid id);
        Task UpdateProject(Project project, AddProject addproject);
    }
}
