using backend.DTOS;
using backend.Model;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly RoleService roleService;
        public RoleController(TestDBContext context)
        {
            this.roleService = new RoleService(context);   
        }

        [HttpPost]
        public async Task<ActionResult> AddUserRole([FromBody] Addrole role) 
        {
            try
            {
                var savedUserrole = await roleService.AddUserRole(role);
                return Ok(savedUserrole);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetAllRoles()
        {
            try
            {
              var roles = await roleService.GetAllRoles();
              return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
