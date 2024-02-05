using backend.DTOS;
using backend.Interfaces;
using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class RoleService : IUserroles
    {
        private readonly TestDBContext context;
        public RoleService(TestDBContext context)
        {
            this.context = context;
        }
        public async Task<Role> AddUserRole(Addrole addrole)
        {
            var newRole = new Role
            {
                RoleId = Guid.NewGuid(),
                RoleName = addrole.RoleName
            };
            context.Add(newRole);
            await context.SaveChangesAsync();
            return newRole;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var rolesList = await context.Roles.ToListAsync();
            return rolesList;
        }
    }
}
