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

        private BLL.Data.Users _BLL;
        private IConfiguration _config;

        public UsersController(DatabaseContext context, IConfiguration config)
        {

            _BLL = new BLL.Data.Users(context);
            _config = config;
        }


        /// <summary>
        /// Register an user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(typeof(ProductsInput), 201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
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

            return Created("Register ",userCreate);
        }

        /// <summary>
        /// Authentification
        /// </summary>
        /// <param name="users"></param>
        /// <returns>Token</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(ProductsInput), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<string> Login([FromForm] UserInputLogin users)
        {
            var userData = new UsersModel { Email = users.Email, Password = users.Password };

            var token = _BLL.Login(userData, _config);
            if (string.IsNullOrEmpty(token))
                return NotFound("Invalid informations");

            return Ok(token);
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
        /// Update yourself Or update an user if you are admin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPut("update"), Authorize]
        [ProducesResponseType(typeof(UsersModel), 200)]
        [ProducesResponseType(401), ProducesResponseType(403), ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<UsersModel> Update([FromQuery] int id, [FromForm] UserInput users)
        {

            if (users == null)
                return BadRequest();

            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (userId == null)
                return StatusCode(500, "Error when reading id in token information !");

            if (userId != id)
            {
                if (Role.Admin.ToString() != User.FindFirst(ClaimTypes.Role).Value)
                    return Unauthorized("You don't have permissions to update this person !");
            }

            var user = new UsersModel { Id = id, Pseudo = users.Pseudo, Email = users.Email, Password = users.Password, role = users.role };

            try
            {
               var userUpdated = _BLL.Update(user);
                if (userUpdated == null) {
                    return NotFound("Veuillez vérifier votre saisie !");
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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete"), Authorize]
        [ProducesResponseType(typeof(UsersModel), 200)]
        [ProducesResponseType(401), ProducesResponseType(403), ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<UsersModel> Delete([FromQuery] int id)
        {
            var user = new UsersModel();

            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (userId == null)
                return StatusCode(500, "Error when reading id in token information !");

            if (userId != id)
            {
                if (Role.Admin.ToString() != User.FindFirst(ClaimTypes.Role).Value)
                    return Unauthorized("You don't have permissions to delete this person !");
            }

            try
            {
                user = _BLL.Delete(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            if (userId == null)
                return NotFound("Utilisateur introuvable !");

            return Ok(user);
        }


    }
}
