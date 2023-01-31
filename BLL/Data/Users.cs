using AutoMapper;
using BLL.Models;
using Dal;
using Dal.Data;
using Dal.Entities;
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
using DevOne.Security.Cryptography.BCrypt;

namespace BLL.Data
{
    public class Users
    {

        private Dal.Data.Users _DAL;
        private Mapper _UserMapper;


        public Users(DatabaseContext context)
        {

            _DAL = new Dal.Data.Users(context);
            var _configUser = new MapperConfiguration(cfg => cfg.CreateMap<UsersEntities, UsersModel>().ReverseMap());
            _UserMapper = new Mapper(_configUser);

        }


        public UsersModel? Register(UsersModel user)
        {

            var userExist = _DAL.SearchUser(user.Email);

            if (userExist != null)
            {
                return null;
            }
            user.Password = HashPassword(user.Password);
            var userMap =  _UserMapper.Map<UsersEntities>(user);
            return _UserMapper.Map<UsersModel>(_DAL.Insert(userMap));

            
        }

        public UsersModel GetUserByEmail(string email)
        {
            
            var userSearch = _DAL.SearchUser(email);
            var user = new UsersModel();
            if (userSearch != null)
            {
                user = _UserMapper.Map<UsersModel>(userSearch);
            }
            return user;

        }

        public UsersModel GetUsersById(int id)
        {

            var userSearch = _DAL.SearchUser(id);
            var user = new UsersModel();
            if (userSearch != null)
            {
                user = _UserMapper.Map<UsersModel>(userSearch);
            }
            return user;

        }

        public List<UsersModel> GetUsers()
        {
            var users = _UserMapper.Map<List<UsersModel>>(_DAL.GetAllUsers());
            return users;
        }

        public bool? Update(UsersModel user) {

            user.Password = HashPassword(user.Password);

            try
            {
                var userUpdated = _DAL.Update(_UserMapper.Map<UsersEntities>(user));

                if (userUpdated == null)
                {
                    return null;
                }
            }
            catch (Exception) {

                return false;
            }

            return true;
        }


        public string? Login(Models.UsersModel user, IConfiguration config)
        {


           

            var userAuth = Authenticate(user);
            if (userAuth != null)
            {
                var token = GenerateToken(userAuth,config);
                return token;
            }

            return null;
        }

        // To generate token
        public string GenerateToken(UsersEntities user, IConfiguration config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.role.ToString())

            };
            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        //To authenticate user
        public UsersEntities Authenticate(UsersModel userLogin)
        {

            var currentUser = _DAL.GetAllUsers().FirstOrDefault(x => x.Email.ToLower() ==
                userLogin.Email.ToLower() && BCryptHelper.CheckPassword(userLogin.Password, x.Password));

            if (currentUser != null)
            {
                UsersEntities userModel = currentUser; 
                return userModel;
            }
            return null;
        }

        public string HashPassword(string password)
        {
            // Generate a new salt
            string salt = BCryptHelper.GenerateSalt(10);

            // Hash the password with the salt
            string hashedPassword = BCryptHelper.HashPassword(password, salt);

            return hashedPassword;
        }

        public UsersModel? Delete(int id)
        {
            var userDeleted = new UsersModel();
            var userExist = _DAL.SearchUser(id);

            if (userExist == null) {
                return null;
            }
           try
            {
                _DAL.Delete(userExist);
            }
            catch
            {
                throw new Exception("Error when delete user");
            }
            
            userDeleted = new UsersModel { Pseudo = userExist.Pseudo, Email = userExist.Pseudo };
            return userDeleted;

        }


    }
}
