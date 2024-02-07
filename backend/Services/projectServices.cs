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
                Projectname = addproject.Projectname,
                Projectdescription = addproject.Projectdescription,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            context.Projects.Add(newProject);
            await context.SaveChangesAsync();
            return newProject;

        }

        public async Task DeleteProject(Project project)
        {
            var assignedProjects = await context.AssignedProjects.Where(e => e.ProjectId == project.ProjectId).ToListAsync();
            context.AssignedProjects.RemoveRange(assignedProjects);
            context.Projects.Remove(project);
            await context.SaveChangesAsync();
        }

        public async Task<List<Project>> GetAllProjects()
        {
            var projectList = context.Projects
                .Include(x => x.AssignedProjects)
                .ThenInclude(a => a.User)
                .ThenInclude(b=>b.Role)
                .ToList();
            return projectList;
        }

        public async Task<Project> GetProjectById(Guid id)
        {
            var findProject = await context.Projects
                .Include(x => x.AssignedProjects)
                .ThenInclude(a => a.User)
                 .ThenInclude(b => b.Role)
                .FirstOrDefaultAsync(e => e.ProjectId == id);
            return findProject;
        }

        public async Task<List<Project>> GetProjectsByUserId(Guid userId)
        {
            var assignedProject = await context.AssignedProjects
                  .Include(x => x.Project)
                  .Include(a => a.User)
                  .Include(b => b.User.Role)
                  .Where(e => e.UserId == userId)
                  .ToListAsync();
            var project = assignedProject.Select(ap => ap.Project).ToList();
            return project;
        }


        public async Task UpdateProject(Project project, AddProject addproject)
        {
            project.Projectname = addproject.Projectname;
            project.Projectdescription = addproject.Projectdescription;
            project.UpdatedAt = DateTime.Now;
            await context.SaveChangesAsync();
        }
        
    }
}
