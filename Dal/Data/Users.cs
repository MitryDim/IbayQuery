using Dal.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Data
{


        public class Users
    {

        private readonly IConfiguration _config;

        DatabaseContext _context;


        public  Users(DatabaseContext context, IConfiguration config)
        {

            _context = context;
            _config = config;

        }

        /// <summary>
        /// Récupération de tous les utilisateurs
        /// </summary>
        /// <returns></returns>
        public List<UsersEntities> GetAllUsers()
        {
            return _context.Users.ToList();
        }


        public UsersEntities SearchUser(string email)
        {
            return _context.Users.Where(c => c.Email == email).FirstOrDefault();
        }

        public UsersEntities SearchUser(int id)
        {
            return _context.Users.Where(c => c.Id == id).FirstOrDefault();
        }




        public UsersEntities? Insert(UsersEntities user)
        {
            
            _context.Users.AddAsync(user);
            _context.SaveChanges();
            return user;
          
        }  
        
        public UsersEntities? Update(UsersEntities user)
        {
            var currentdata = _context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
           if (currentdata == null)
            {
                return currentdata;
            }

            _context.Entry(currentdata).CurrentValues.SetValues(user);
            _context.SaveChangesAsync();
            return user;
        }

        public void Delete (UsersEntities user) {
            _context.Remove(user);
            _context.SaveChangesAsync();
        }
    }
}
