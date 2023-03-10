using api_app.DTO;
using api_app.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace api_app.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private IMapper mapper;

        public IConfiguration Configuration { get; private set; }

        private HttpClient _client;

        public UserController(ApplicationDbContext context, IMapper mapper, IConfiguration Configuration) {
            this.context = context;
            this.mapper = mapper;

            this.Configuration = Configuration;

            this._client = new HttpClient();
        }

        [HttpPost("add")]
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

            return Ok("Usuario agregado!!!");
        }


        [HttpGet("getUser/{id:int}")]
        public async Task<ActionResult<UserResponseDTO>> GetUser(int id)
        {
            var user = await context.Users.Include(x=>x.Responsability)
                           .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<UserResponseDTO>(user);
            return dto;
        }


        //endpoint de prueba
        [HttpGet("getUsers/{page:int}/{count:int}")]
        public async Task<ActionResult<UserResponseDTO>> GetUsers(int page, int count)
        {
            try
            {
                var key = Configuration.GetValue<string>("ApiKey");
                var response = await _client.GetAsync($"https://randomuser.me/api?page={page}&results={count}");
                var content = await response.Content.ReadAsStringAsync();

                return Ok(content);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
