using api_app.DTO;
using api_app.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_app.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private IMapper mapper;

        public UserController(ApplicationDbContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost("add")]
        [AllowAnonymous]
        //  [Authorize(Policy = "EsAdmin")]
        public async Task<ActionResult> AddUser(UserNewDTO userNewDTO)
        {
            var existe = await context.Users.AnyAsync(x => x.Email == userNewDTO.Email);
            if (existe)
            {
                return BadRequest($"El {userNewDTO.Email} ya existe!!!");
            }

            var user = mapper.Map<User>(userNewDTO);

            context.Add(user);
            await context.SaveChangesAsync();

            return Ok(userNewDTO);
        }


    }
}
