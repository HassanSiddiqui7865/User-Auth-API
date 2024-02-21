using backend.DTOS;
using backend.Interfaces;
using backend.Model;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly userService userService;
        public UserController(TestDBContext DbContext)
        {
            this.userService = new userService(DbContext);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> CreateUser([FromBody] AddUser adduser)
        {
            try
            {
                var existing = await userService.CheckingExisting(adduser.Username, adduser.Email);
                if (existing != null)
                {
                    return Conflict("Account Exist with same Credentials");
                }
                var savedUser = await userService.RegisterUser(adduser);
                return Ok(savedUser);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("login")]

        public async Task<ActionResult> Login([FromBody] loginUser loginuser)
        {
            try
            {
                var finduser = await userService.getByUsername(loginuser.Username);

                if (finduser == null)
                {
                    return NotFound(new { message = "Wrong Credentials" });
                }
                var decryptedPaasword = userService.DecryptPassword(finduser.Pass, loginuser.Pass);
                if (decryptedPaasword)
                {

                    var user = new userLoggedIn
                    {
                        UserId = finduser.UserId,
                        Fullname = finduser.Fullname,
                        Username = finduser.Username,
                        Email = finduser.Email,
                        RoleId = finduser.RoleId,
                        RoleName = finduser.Role.RoleName,
                        CreatedAt = finduser.CreatedAt
                    };
                    return Ok(user);
                }
                return NotFound(new { message = "Wrong Credentials" });


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]

        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                var ListUser = await userService.GetUsersWithProjects();
                var usersDto = ListUser.Select(user => new UserWithProjectsDto
                {
                    UserId = user.UserId,
                    Fullname = user.Fullname,
                    Username = user.Username,
                    Email = user.Email,
                    RoleId = user.RoleId,
                    RoleName = user.Role.RoleName,
                    CreatedAt = user.CreatedAt,
                    Projects = user.AssignedProjects.Select(ap => new ProjectDTO
                    {
                        ProjectId = ap.Project.ProjectId,
                        Projectname = ap.Project.Projectname,
                        Projectdescription = ap.Project.Projectdescription,
                        AvatarUrl = ap.Project.AvatarUrl,
                        Projectkey = ap.Project.Projectkey,
                        CreatedAt = ap.Project.CreatedAt,
                        UpdatedAt = ap.Project.UpdatedAt
                    }).ToList()
                }).ToList();
                return Ok(usersDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("UserWithProjects/{userId:Guid}")] 
        public async Task<ActionResult> GetUserById(Guid userId)
        {
            try
            {
                var user = await userService.GetUserWithProjectById(userId);
                if(user!= null)
                {
                    var usersDto = new UserWithProjectsDto
                    {
                        UserId = user.UserId,
                        Fullname = user.Fullname,
                        Username = user.Username,
                        Email = user.Email,
                        RoleId = user.RoleId,
                        RoleName = user.Role.RoleName,
                        CreatedAt = user.CreatedAt,
                        Projects = user.AssignedProjects.Select(ap => new ProjectDTO
                        {
                            ProjectId = ap.Project.ProjectId,
                            Projectname = ap.Project.Projectname,
                            Projectdescription = ap.Project.Projectdescription,
                            AvatarUrl = ap.Project.AvatarUrl,
                            Projectkey = ap.Project.Projectkey,
                            CreatedAt = ap.Project.CreatedAt,
                            UpdatedAt = ap.Project.UpdatedAt
                        }).ToList()
                    };
                    return Ok(usersDto);
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{userId:Guid}")]
        public async Task<ActionResult> DeleteUser(Guid userId)
        {
            try
            {
                var findUser = await userService.GetUserById(userId);
                if (findUser != null)
                {
                    await userService.DeleteUser(findUser);
                    return Ok(new {message = "Deleted Successfully"});
                }
                return NotFound(new { message = "User not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("withoutProjects")]
        public async Task<ActionResult> GetUsersWithoutProjects()
        {
            try
            {
                var ListUser = await userService.GetUsers();
                var usersDto = ListUser.Select(user => new userLoggedIn
                {
                    UserId = user.UserId,
                    Fullname = user.Fullname,
                    Username = user.Username,
                    Email = user.Email,
                    RoleId = user.RoleId,
                    RoleName = user.Role.RoleName,
                    CreatedAt = user.CreatedAt,
                }).ToList();
                return Ok(usersDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{userId:Guid}/{roleId:Guid}")]
        public async Task<ActionResult> UpdateUserRole(Guid userId,Guid roleId)
        {
            try
            {
                var findUser = await userService.GetUserById(userId);
                if (findUser != null)
                {
                    await userService.UpdateRole(roleId, findUser);
                    return Ok(new { message = "Updated Successfully" });
                }
                return NotFound(new { message = "User not found" });

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
    }
    
}
