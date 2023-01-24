﻿using BLL;
using BLL.Data;
using BLL.Models;
using Dal;
using Dal.Data;
using Dal.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;

namespace IbayApi.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase { 

        private BLL.Data.Users _BLL;

        public UsersController(DatabaseContext context, IConfiguration config)
        {

            _BLL =  new BLL.Data.Users(context, config);
        }

        [AllowAnonymous]
        [HttpPost("~/login")]
        public ActionResult Login([FromForm] UserInputLogin users)
        {
          
           var userData = new UsersModel { Email= users.Email, Password = users.Password  };
            var userAuth = _BLL.Authenticate(userData);
            if (userAuth != null)
            {
                var token = _BLL.GenerateToken(userAuth);
                return Ok(token);
            }

            return NotFound("Invalid informations");
        }

        /// <summary>
        /// Register an user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("~/register")]
        public ActionResult<UsersModel> Register([FromBody] UserInput user)
        {
            if (user == null)
            {
                return BadRequest("");
            }

            UsersModel usersModel = new UsersModel();
            usersModel.Pseudo = user.Pseudo;
            usersModel.Email = user.Email;
            usersModel.Password = user.Password;



            UsersModel? userCreate = _BLL.Register(usersModel);
            if (userCreate == null)
            {
                return BadRequest();
            }

            return Ok(userCreate);
        }


        [HttpPut("~/update"),Authorize]
        public ActionResult<UsersModel> Update([FromQuery] int id, [FromForm] UserInput users)
        {

            if (users == null)
                return BadRequest();

            //var currentUser = _BLL.GetUsersById(id);

            //if (currentUser == null)
            //    return  NotFound();

          //  var claims = HttpContext.User.Claims;
           // var email = claims.FirstOrDefault(c => c.Type == "Email").Value;

            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (userId == null)
                return StatusCode(500, "Error when reading id in token information !");

            if (userId != id )
            {
                if (Role.Admin.ToString() != User.FindFirst(ClaimTypes.Role).Value)
                    return Unauthorized("You don't have permissions to update this person !");
            }

            var user = new UsersModel { Id= id, Pseudo = users.Pseudo, Email = users.Email,Password = users.Password, role= users.role };
           
            try
            {
                user = _BLL.Update(user);

            } catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
                return Ok(user);
        }

        [HttpDelete,Authorize]
        public ActionResult<UsersModel> Delete([FromQuery] int id)
        {
            var user = new UsersModel();
            try
            {
                user = _BLL.Delete(id);
            }
            catch(Exception ex)
            {
               
                return StatusCode(500,ex.Message);
            }

            return Ok(user);
        }


    }
}
