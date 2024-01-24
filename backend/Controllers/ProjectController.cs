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
                var ProjectList = await projectServices.GetAllProjects();
                return Ok(ProjectList);
            }
           catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetProjectById(Guid id)
        {
            try
            {
                var Project = await projectServices.GetProjectById(id);
                return Ok(Project);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteProject(Guid id)
        {
            try
            {
                var findUser = await projectServices.GetProjectById(id);
                if (findUser != null)
                {
                    await projectServices.DeleteProject(findUser);
                    return Ok(new { message = "Project Deleted" });
                }
                return NotFound(new {message = "Project not found"});
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateProject([FromBody] AddProject addproject,Guid id)
        {
            try
            {
                var findProject = await projectServices.GetProjectById(id);
                if(findProject != null)
                {
                    await projectServices.UpdateProject(findProject, addproject);
                    return Ok(new { message = "Project Updated" });
                }
                return NotFound(new { message = "Project not found" });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
