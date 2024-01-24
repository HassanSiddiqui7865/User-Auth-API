using backend.DTOS;
using backend.Interfaces;
using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class projectServices : IProject
    {
        private readonly TestDBContext context;
        public projectServices(TestDBContext context)
        {
            this.context = context;
        }
        public async Task<Project> CreateProject(AddProject addproject)
        {
            var newProject = new Project
            {
                ProjectId = Guid.NewGuid(),
                Projectfullname = addproject.Projectfullname,
                Projectshortname = addproject.Projectshortname,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            context.Projects.Add(newProject);
            await context.SaveChangesAsync();
            return newProject;

        }

        public async Task DeleteProject(Project project)
        {
           context.Remove(project);
           await context.SaveChangesAsync();
        }

        public async Task<List<Project>> GetAllProjects()
        {
            var projectList = await context.Projects.ToListAsync();
            return projectList;
        }

        public async Task<Project> GetProjectById(Guid id)
        {
            var findProject = await context.Projects.FirstOrDefaultAsync(e=>e.ProjectId == id);
            return findProject;
        }

        public async Task UpdateProject(Project project, AddProject addproject)
        {
            project.Projectshortname = addproject.Projectshortname;
            project.Projectfullname = addproject.Projectfullname;
            await context.SaveChangesAsync();
        }
    }
}
