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

namespace BLL.Data
{
    public class Users
    {

        private Dal.Data.Users _DAL;



        private readonly IConfiguration _config;

        private Mapper _UserMapper;


        public Users(DatabaseContext context, IConfiguration config)
        {
            _config = config;
            _DAL = new Dal.Data.Users(context, config);
            var _configUser = new MapperConfiguration(cfg => cfg.CreateMap<UsersEntities, UsersModel>().ReverseMap());
            _UserMapper = new Mapper(_configUser);

        }


        public UsersEntities? Register(UsersModel user)
        {


            var userExist = _DAL.GetUserByEmail(user.Email);

            if (userExist != null)
            {
                return null;
            }
           var userMap =  _UserMapper.Map<UsersEntities>(user);
            return _DAL.Insert(userMap);

            
        }


        public string? Login(Models.UsersModel user)
        {
            var userAuth = Authenticate(user);
            if (userAuth != null)
            {
                var token = GenerateToken(userAuth);
                return token;
            }

            return null;
        }

        // To generate token
        public string GenerateToken(UsersEntities user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Email),
                new Claim(ClaimTypes.Role,user.role.ToString())

            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        //To authenticate user
        public UsersEntities Authenticate(UsersModel userLogin)
        {
            var currentUser = _DAL.GetAllUsers().FirstOrDefault(x => x.Email.ToLower() ==
                userLogin.Email.ToLower() && x.Password == userLogin.Password);

            if (currentUser != null)
            {
                UsersEntities userModel = currentUser; 
                return userModel;
            }
            return null;
        }


    }
}
