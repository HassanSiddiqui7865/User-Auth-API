using backend.DTOS;
using backend.Model;

namespace backend.Interfaces
{
    public interface IUser
    {
        Task<User> RegisterUser(AddUser adduser);

        Task<List<User>> GetUsersWithProjects();

        Task<User> GetUserWithProjectById(Guid id);

        Task<User> GetUserById (Guid id);
        Task DeleteUser(User user);
        Task<User> CheckingExisting(string username, string email);
        Task<User> getByUsername(string username);
        Task<List<User>> GetUsers();
        Task UpdateRole (Guid RoleId,User user);
        string EncryptPassword(string password);
        bool DecryptPassword(string Hashed, string password);




    }
}
