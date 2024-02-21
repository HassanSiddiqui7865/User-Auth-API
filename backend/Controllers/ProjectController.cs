using backend.DTOS;
using backend.Model;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly projectServices projectServices;
        public ProjectController(TestDBContext DbContext)
        {
            this.projectServices = new projectServices(DbContext);   
        }
        [HttpPost]
        public async Task<ActionResult> CreateProject([FromBody] AddProject addproject)
        {
            try
            {
                var savedUser = await projectServices.CreateProject(addproject);
                return Ok(savedUser);
            }
              catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetAllProjects()
        {
            try
            {
                var projectList = await projectServices.GetAllProjects();
                var projectDTO = projectList.Select(project => new ProjectWithUsersDto
                {
                    ProjectId = project.ProjectId,
                    Projectname = project.Projectname,
                    Projectkey = project.Projectkey,
                    AvatarUrl = project.AvatarUrl,
                    Projectdescription = project.Projectdescription,
           
                    Users = project.AssignedProjects.Select(a => new UserDTO
                    {
                        UserId = a.UserId,
                        Fullname = a.User.Fullname,
                        Username = a.User.Username,
                        Email = a.User.Email,
                        IsLead = a.IsLead,
                        RoleId = a.User.RoleId,
                        RoleName = a.User.Role.RoleName,
                        CreatedAt = a.User.CreatedAt,
                        
                    }).ToList(),
                    CreatedAt = project.CreatedAt,
                    UpdatedAt = project.UpdatedAt
                }).ToList();

                return Ok(projectDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("withoutUsers")]
        public async Task<ActionResult> GetProjectsWithoutUsers()
        {
            try
            {
                var projectList = await projectServices.GetAllProjects();
                var projectDTO = projectList.Select(project => new ProjectDTO
                {
                    ProjectId = project.ProjectId,
                    Projectname = project.Projectname,
                    Projectkey = project.Projectkey,
                    AvatarUrl = project.AvatarUrl,
                    Projectdescription = project.Projectdescription,
                    CreatedAt = project.CreatedAt,
                    UpdatedAt = project.UpdatedAt
                }).ToList();

                return Ok(projectDTO);
            }
            catch(Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }
        [HttpGet("withoutUsers/{projectid:Guid}")]
        public async Task<ActionResult> GetProjectByIdsWithoutUser(Guid projectid)
        {
            try
            {
                var project = await projectServices.GetProjectById(projectid);
                if(project == null)
                {
                    return NotFound(new { message = "Project Not found" });
                }
                var projectDTO = new ProjectDTO
                {
                    ProjectId = project.ProjectId,
                    Projectname = project.Projectname,
                    Projectkey = project.Projectkey,
                    AvatarUrl = project.AvatarUrl,
                    Projectdescription = project.Projectdescription,
                    CreatedAt = project.CreatedAt,
                    UpdatedAt = project.UpdatedAt,
                };
                return Ok(projectDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{projectId:Guid}")]
        public async Task<ActionResult> GetProjectById(Guid projectId)
        {
            try
            {
                var project = await projectServices.GetProjectById(projectId);
                if(project == null)
                {
                    return NotFound(new { message = "Project Not found" });
                }
                var projectDTO = new ProjectWithUsersDto
                {
                    ProjectId = project.ProjectId,
                    Projectname = project.Projectname,
                    Projectkey = project.Projectkey,
                    AvatarUrl = project.AvatarUrl,
                    Projectdescription = project.Projectdescription,
                    Users = project.AssignedProjects.Select(a => new UserDTO
                    {
                        UserId = a.UserId,
                        Fullname = a.User.Fullname,
                        Username = a.User.Username,
                        Email = a.User.Email,
                        RoleId = a.User.RoleId,
                        RoleName = a.User.Role.RoleName,
                        IsLead = a.IsLead,
                        CreatedAt = a.User.CreatedAt,

                    }).ToList(),
                    CreatedAt = project.CreatedAt,
                    UpdatedAt = project.UpdatedAt
                };
                return Ok(projectDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("user/{userId:Guid}")]
        public async Task<ActionResult> GetProjectsByUserId(Guid userId)
        {
            try
            {
                var project = await projectServices.GetProjectsByUserId(userId);
                if (project == null)
                {
                    return NotFound(new { message = "User Not found" });
                }
                var projectDTO = project.Select(p=>new ProjectWithUsersDto
                {
                    ProjectId = p.ProjectId,
                    Projectname = p.Projectname,
                    Projectkey = p.Projectkey,
                    AvatarUrl = p.AvatarUrl,
                    Projectdescription = p.Projectdescription,
                    Users = p.AssignedProjects.Select(a => new UserDTO
                    {
                        UserId = a.UserId,
                        Fullname = a.User.Fullname,
                        Username = a.User.Username,
                        Email = a.User.Email,
                        RoleId = a.User.RoleId,
                        RoleName = a.User.Role.RoleName,
                        IsLead = a.IsLead,
                        CreatedAt = a.User.CreatedAt,

                    }).ToList(),
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                }).ToList();
                return Ok(projectDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{projectId:Guid}")]
        public async Task<ActionResult> DeleteProject(Guid projectId)
        {
            try
            {
                var findUser = await projectServices.GetProjectById(projectId);
                if (findUser != null)
                {
                    await projectServices.DeleteProject(findUser);
                    return Ok(new { message = "Project Deleted" });
                }
                return NotFound(new { message = "Project not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{projectId:Guid}")]
        public async Task<ActionResult> UpdateProject([FromBody] AddProject addproject, Guid projectId)
        {
            try
            {
                var findProject = await projectServices.GetProjectById(projectId);
                if (findProject != null)
                {
                    await projectServices.UpdateProject(findProject, addproject);
                    return Ok(new { message = "Project Updated" });
                }
                return NotFound(new { message = "Project not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
    }
}
