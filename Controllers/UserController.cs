using api_app.DTO;
using api_app.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;

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

        public class ResponseRole
        {
            public string Role { get; set; }
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            var role = "nullable";
            var entidadSolicitud = await context.Users.FirstOrDefaultAsync(solicitud => solicitud.Email == loginDTO.Email);
            if (entidadSolicitud != null)
            {
                if (entidadSolicitud.IsAdmin)
                { role = "isadmin"; }
            }
            byte[] bytes = Encoding.UTF8.GetBytes(role);
            string base64 = Convert.ToBase64String(bytes);
            var response = new ResponseRole
            {
                Role = base64,
            };
            return Ok(response);
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

            return Ok("Usuario agregado...");
        }


        [HttpGet("getUser/{id}")]
        public async Task<ActionResult<UserResponseDTO>> GetUser(string id)
        {

            Microsoft.Extensions.Primitives.StringValues headerValue;
            if (Request.Headers.TryGetValue("clm", out headerValue))
            {
                // Use headerValue
                string decodedString = Encoding.UTF8.GetString(Convert.FromBase64String(headerValue.ToString()));
                Console.WriteLine(decodedString);
            }

            var user = await context.Users.Include(x => x.Responsability)
                           .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }



            var dto = mapper.Map<UserResponseDTO>(user);
            return dto;
            return Ok();
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
