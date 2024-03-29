﻿using backend.DTOS;
using backend.Interfaces;
using backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

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

        public async Task<List<User>> GetUsersWithProjects()
        {
            var usersWithProjects = await context.Users.Include(r=>r.Role)
               .Include(u => u.Role)
               .Include(u => u.AssignedProjects)
               .ThenInclude(ap => ap.Project)
               .ToListAsync();


            return usersWithProjects;
        }

        public async Task<List<User>> GetUsers()
        {
            var usersWithProjects = await context.Users.Include(r => r.Role)
               .Include(u => u.Role)
               .ToListAsync();
            return usersWithProjects;
        }

        public async Task<User> RegisterUser(AddUser adduser)
        {
            var encryptedPassword = EncryptPassword(adduser.Pass);
            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                Fullname = adduser.Fullname,
                Username = adduser.Username,
                Email = adduser.Email,
                Pass = encryptedPassword,
                RoleId = adduser.RoleId,
                CreatedAt = DateTime.Now,
            };
            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            return newUser;
        }

        public bool DecryptPassword(string Hashed, string password)
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
            var user = await context.Users.Include(x=>x.Role)
            .FirstOrDefaultAsync(e => e.Username == username);
            return user;
        }

        public async Task<User> GetUserWithProjectById(Guid id)
        {
            var findUser = await context.Users
                .Include(x=>x.Role)
                .Include(x => x.AssignedProjects)
                .ThenInclude(a => a.Project)
                .FirstOrDefaultAsync(e => e.UserId == id);
            return findUser;

        }

        public async Task DeleteUser(User user)
        {
            var assignedProjects = await context.AssignedProjects.Where(e => e.UserId == user.UserId).ToListAsync();
            var assignedTickets = await context.Tickets.Where(e=> e.AssignedToNavigation.UserId == user.UserId).ToListAsync();
            context.Tickets.RemoveRange(assignedTickets);
            context.AssignedProjects.RemoveRange(assignedProjects);
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task UpdateRole(Guid RoleId,User user)
        {
           user.RoleId = RoleId;
           await context.SaveChangesAsync();
        }

        public async Task<User> GetUserById(Guid id)
        {
            var findUser = await context.Users
               .Include(x => x.Role)
               .FirstOrDefaultAsync(e => e.UserId == id);
            return findUser;
        }
    }
}
