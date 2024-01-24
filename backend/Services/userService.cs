using backend.DTOS;
using backend.Interfaces;
using backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace backend.Services
{
    public class userService : IUser
    {
        private readonly TestDBContext context;
        public userService(TestDBContext Dbcontext)
        {
            this.context = Dbcontext;   
        }
        public string EncryptPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);

            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }

        public async Task<User> CheckingExisting(string username, string email)
        {
            var existingUser = await context.Users
           .FirstOrDefaultAsync(e => e.Username == username || e.Email == email);
            return existingUser;
        }

        public async Task<List<User>> GetUsers()
        {
            var userList = await context.Users.ToListAsync();
            return userList;
        }

        public async Task<User> RegisterUser(AddUser adduser)
        {
            var encryptedPassword = EncryptPassword(adduser.Pass);
            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                Username = adduser.Username,
                Email = adduser.Email,
                Pass = encryptedPassword,
                Userrole = adduser.Userrole,
                CreatedAt = DateTime.Now,
            };
            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            return newUser;
        }

        public bool DecryptPassword(string Hashed,string password)
        {
            byte[] hashBytes = Convert.FromBase64String(Hashed);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<User> getByUsername(string username)
        {
           var user = await context.Users.FirstOrDefaultAsync(e => e.Username == username);
            return user;
        }

        public async Task<User> GetById(Guid id)
        {
            var findUser = await context.Users.FirstOrDefaultAsync(e => e.UserId == id);
            return findUser;

        }

        public async Task DeleteUser(User user)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task updatePassword(User user,string password)
        {
            var encryptedPass = EncryptPassword(password);
            user.Pass = encryptedPass;
            await context.SaveChangesAsync();
        }
    }
}
