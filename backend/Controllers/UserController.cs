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
                    return NotFound("Wrong Credentials");
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
                return NotFound("Wrong Credentials");


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
                var ListUser = await userService.GetUsers();
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
        [HttpGet("role/{id:Guid}")]
        public async Task<ActionResult> GetUserByRole(Guid id)
        {
            try
            {
                var userList = await userService.GetByRole(id);
                var users = userList.Select(user => new userLoggedIn
                {
                    UserId = user.UserId,
                    Fullname = user.Fullname,
                    Username = user.Username,
                    Email = user.Email,
                    RoleId = user.RoleId,
                    RoleName = user.Role.RoleName,
                    CreatedAt = user.CreatedAt,
                }).ToList();
                return Ok(users);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //    [HttpGet("{username}")]
        //    public async Task<ActionResult> GetByUsername(string username)
        //    {
        //        try
        //        {
        //            var findUser = await userService.getByUsername(username);
        //            if (findUser != null)
        //            {
        //                return Ok(findUser);
        //            }
        //            return NotFound("user not found");
        //        }catch(Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            try
            {
                var findUser = await userService.GetById(id);
                if (findUser != null)
                {
                    await userService.DeleteUser(findUser);
                    return Ok();
                }
                return NotFound();
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
        //    [HttpPut]
        //    public async Task<ActionResult> Updatepassword( [FromBody] loginUser loginuser)
        //    {
        //        try
        //        {
        //            var findUser = await userService.getByUsername(loginuser.Username);
        //            if(findUser != null)
        //            {
        //                await userService.updatePassword(findUser, loginuser.Pass);
        //                return Ok("Updated");
        //            }return NotFound("Username not found");
        //        }catch(Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //}
    }
    
}
