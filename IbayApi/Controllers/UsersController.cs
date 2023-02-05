using BLL;
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
    public class UsersController : ControllerBase
    {

        private BLL.Data.UsersBLL _BLL;
        private IConfiguration _config;

        public UsersController(DatabaseContext context, IConfiguration config)
        {

            _BLL = new BLL.Data.UsersBLL(context);
            _config = config;
        }


        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(UsersModel), 200)]
        [ProducesResponseType(401), ProducesResponseType(403), ProducesResponseType(404)]
        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult<List<UsersModel>> GetUser()
        {

            return Ok(_BLL.GetUsers());

        }


        /// <summary>
        /// Register an user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        [ProducesResponseType(typeof(UsersModel), 201)]
        [ProducesResponseType(401), ProducesResponseType(403), ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<UsersModel> Register([FromForm] UserInputRegister user)
        {
            if (user == null)
            {
                return BadRequest("");
            }

            UsersModel usersModel = new UsersModel();
            usersModel.Pseudo = user.Pseudo;
            usersModel.Email = user.Email;
            usersModel.Password = user.Password;
            usersModel.role = user.role;



            UsersModel? userCreate = _BLL.Register(usersModel);
            if (userCreate == null)
            {
                return BadRequest();
            }

            return Created("Register", userCreate);
        }

        /// <summary>
        /// Authentication
        /// </summary>
        /// <param name="users"></param>
        /// <returns>Token</returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401), ProducesResponseType(403), ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<string> Login([FromForm] UserInputLogin users)
        {
            var userData = new UsersModel { Email = users.Email, Password = users.Password };

            var token = _BLL.Login(userData, _config);
            if (string.IsNullOrEmpty(token))
                return NotFound("Authentication error please check the information entered");

            return Ok(token);
        }

        /// <summary>
        /// Update yourself Or update an user if you are admin
        /// </summary>
        /// <param name="id">User ID to update</param>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPut("Update"), Authorize]
        [ProducesResponseType(typeof(UsersModel), 200)]
        [ProducesResponseType(401), ProducesResponseType(403), ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<UsersModel> Update([FromQuery] int id, [FromForm] UserInput users)
        {

            if (users == null)
                return BadRequest();

            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (userId == null)
                return StatusCode(500, "Error retrieving token ! ");

            if (userId != id)
            {
                if (Role.Admin.ToString() != User.FindFirst(ClaimTypes.Role).Value)
                    return Unauthorized("You do not have rights to update this person.");
            }

            var user = new UsersModel { Id = id, Pseudo = users.Pseudo, Email = users.Email, Password = users.Password, role = users.role };

            try
            {
                var userUpdated = _BLL.Update(user);
                if (userUpdated == null)
                {
                    return NotFound("Please check your input !");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return Ok(user);
        }

        /// <summary>
        /// Delete yourself or delete an user if you are admin
        /// </summary>
        /// <param name="id">User ID to delete</param>
        /// <returns></returns>
        [HttpDelete("Delete"), Authorize]
        [ProducesResponseType(typeof(UsersModel), 200)]
        [ProducesResponseType(401), ProducesResponseType(403), ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete([FromQuery] int id)
        {
            var user = new UsersModel();

            if (User.FindFirst(ClaimTypes.NameIdentifier) == null)
                return StatusCode(500, "Error retrieving token ! ");

            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


            if (userId != id)
            {
                if (Role.Admin.ToString() != User.FindFirst(ClaimTypes.Role).Value)
                    return Unauthorized("You do not have permission to delete this person.");
            }

            try
            {

                var data = _BLL.Delete(id);
                if (data == null)
                    return BadRequest("User not found !");
                else
                    return Ok("User deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
