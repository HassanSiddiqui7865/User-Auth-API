using backend.DTOS;
using backend.Model;

namespace backend.Interfaces
{
    public interface IUserroles
    {
        Task<Role> AddUserRole(Addrole addrole);

        Task<List<Role>> GetAllRoles(); 


    }
}
