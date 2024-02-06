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
        [HttpPut("{projectId:Guid}/{userId:Guid}")]
        public async Task<ActionResult> UpdateLead(Guid projectId, Guid userId)
        {
            try
            {
                var findAssigned = await assignedprojectService.GetAssigned(projectId, userId);
                if (findAssigned == null)
                {
                    return NotFound();
                }
                await assignedprojectService.UpdateLeadTrue(findAssigned);
                return Ok();
            }
        catch(Exception ex) {
                return BadRequest();
            }
        }
        [HttpPut("{projectId:Guid}/{leadId:Guid}/{userId:Guid}")]
        public async Task<ActionResult> UpdateRemoveLead(Guid projectId,Guid leadId , Guid userId)
        {
            try
            {
                var findLead = await assignedprojectService.GetAssigned(projectId, leadId);
                var findUser = await assignedprojectService.GetAssigned(projectId, userId);
                if (findLead == null || findUser == null)
                {
                    return NotFound();
                }
                await assignedprojectService.UpdateLeadFalse(findLead);
                await assignedprojectService.UpdateLeadTrue(findUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
