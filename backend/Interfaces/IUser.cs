using backend.DTOS;
using backend.Model;

namespace backend.Interfaces
{
    public interface IUser
    {
        Task<User> RegisterUser(AddUser adduser);

        Task<List<User>> GetUsers();

        Task<User> GetById(Guid id);

        Task DeleteUser(User user);

        // Task updatePassword(User user,string password);
        Task <List<User>> GetByRole(Guid id);
        Task<User> CheckingExisting(string username, string email);
        Task<User> getByUsername(string username);
        string EncryptPassword(string password);
        bool DecryptPassword(string Hashed, string password);




    }
}
