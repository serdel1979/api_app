using api_app.DTO;
using api_app.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api_app.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public UserController(
            UserManager<IdentityUser> userManager
            ) {
            this.userManager = userManager;
        }

        [HttpPost("add")]
        [AllowAnonymous]
        //  [Authorize(Policy = "EsAdmin")]
        public async Task<ActionResult> RegistraUsuario(UserNewDTO userNew)
        {
            var user = new User
            {
                Email = userNew.Email,
                Leader = true
            };

            Console.WriteLine(user);
            
            return Ok(userNew);
        }


    }
}
