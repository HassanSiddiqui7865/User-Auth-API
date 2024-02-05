using backend.DTOS;
using backend.Model;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignedProjectController : Controller
    {
        private readonly AssignedprojectService assignedprojectService;
        public AssignedProjectController(TestDBContext context)
        {
            this.assignedprojectService = new AssignedprojectService(context);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAssignedProject([FromBody] AddAssignedProject addAssignedProject)
        {
            try
            {
                var savedAssigned = await assignedprojectService.CreateAssignedProject(addAssignedProject);
                return Ok(savedAssigned);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetProjectByUserId(Guid id)
        {
            try
            {
                var projects = await assignedprojectService.GetAssignedProjects(id);
                var wrappedProjects = projects.Select(a => new ProjectDTO
                {
                    ProjectId = a.ProjectId,
                    Projectname = a.Projectname,
                    Projectdescription = a.Projectdescription,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt

                }).ToList();
                return Ok(wrappedProjects);
            }catch(Exception ex) { 
             return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{projectId:Guid}/{userId:Guid}")]
        public async Task<ActionResult> DeleteAssigned(Guid projectId,Guid userId)
        {
            try
            {
                var findAssigned = await assignedprojectService.GetAssigned(projectId, userId);
                if(findAssigned == null)
                {
                    return NotFound();
                }
                await assignedprojectService.DeleteAssigned(findAssigned);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
