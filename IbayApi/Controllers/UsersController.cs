using BLL;
using BLL.Data;
using BLL.Models;
using Dal;
using Dal.Data;
using Dal.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IbayApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase { 

        private BLL.Data.Users _BLL;

        public UsersController(DatabaseContext context, IConfiguration config)
        {

            _BLL =  new BLL.Data.Users(context, config);
        }

        [AllowAnonymous]
        [HttpPost("~/login")]
        public ActionResult Login([FromBody] UsersModel users)
        {
            
            var userAuth = _BLL.Authenticate(users);
            if (userAuth != null)
            {
                var token = _BLL.GenerateToken(userAuth);
                return Ok(token);
            }

            return NotFound("Invalid informations");
        }

        [AllowAnonymous]
        [HttpPost("~/register")]
        public ActionResult<UsersEntities> Register([FromBody] UsersModel user)
        {
            if (user == null)
            {
                return BadRequest("");
            }

            UsersEntities? userCreate = _BLL.Register(user);
            if (userCreate == null)
            {
                return BadRequest();
            }

            return Ok(userCreate);
        }

        [HttpPut("~/update"), Authorize]
        public ActionResult Update([FromForm] UsersModel users )
        {


            return BadRequest("test");
        }
    }
}
