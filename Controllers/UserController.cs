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
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace api_app.Controllers
{
    [ApiController]
    [Route("users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    // [Authorize(Policy = "EsAdmin")]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public IConfiguration Configuration { get; private set; }

        private HttpClient _client;

        public UserController(
            ApplicationDbContext context, 
            IMapper mapper, 
            IConfiguration Configuration,
            UserManager<IdentityUser> userManager) {

            this.context = context;
            this.mapper = mapper;

            this.Configuration = Configuration;
            this.userManager = userManager;
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
            user.UserName = user.Email;
            context.Add(user);
            await context.SaveChangesAsync();

            return Ok("Usuario agregado...");
        }


        [HttpGet("getUser/{id}")]
       // [Authorize(Policy = "EsAdmin")]
        public async Task<ActionResult<UserResponseDTO>> GetUser(string id)
        {

            var user = await context.Users.Include(x => x.Responsability).Include(x=> x.Project)
                           .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<UserResponseDTO>(user);
            return dto;
        }

        [HttpGet("getWorkers")]
        public async Task<ActionResult<List<UserResponseDTO>>> GetUserWorker()
        {

            var users = await context.Users.Where(x => x.Leader == false).ToListAsync();

            if (users == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<List<UserResponseDTO>>(users);
            return dto;
        }




        [HttpPost("doAdmin")]
        public async Task<ActionResult> hacerAdmin(LoginDTO doAdmin)
        {
            //var user = await userManager.FindByEmailAsync(doAdmin.Email);
            var user = await context.Users.FirstOrDefaultAsync(usr => usr.Email == doAdmin.Email);
            if(user != null)
            {
                user.UserName = user.Email;
                var claim = await userManager.AddClaimAsync(user, new Claim("esAdmin", "1"));
                return NoContent();
            }

            return BadRequest("Solicitud fallida");
            
        }

        [HttpPost("deleteadmin")]
        public async Task<ActionResult> deleteAdmin(LoginDTO doAdmin)
        {
            //var user = await userManager.FindByEmailAsync(doAdmin.Email);
            var user = await context.Users.FirstOrDefaultAsync(usr => usr.Email == doAdmin.Email);
            if (user != null)
            {
                user.UserName = user.Email;
                var claim = await userManager.RemoveClaimAsync(user, new Claim("esAdmin", "1"));
                return NoContent();
            }
            return BadRequest("Solicitud fallida");  
        }


        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<ActionResult<SigninResponse>> Signin(LoginDTO loginDTO)
        {
            var usrSignin= await context.Users.FirstOrDefaultAsync(usr => usr.Email == loginDTO.Email);

            if (usrSignin == null)
            {
                return BadRequest("No puede ingresar!!!");
            }
            var project = await context.Projects
                .Include(x => x.Job)
                .Include(x => x.Users)
                .FirstOrDefaultAsync(project => project.Id == usrSignin.ProjectId);

            if (project == null)
            {
                return BadRequest("No tiene proyecto asignado!!!");
            }
            var dto = mapper.Map<ProjectRespDTO>(project);
            return await construirToken(loginDTO, usrSignin, dto);
        }


        private async Task<SigninResponse> construirToken(LoginDTO loginDTO, User user, ProjectRespDTO project)
        {

            var claims = new List<Claim>(){
                new Claim("user", user.Id)
            };

            var claimsDB = await userManager.GetClaimsAsync(user);

            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Secret"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddDays(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);


            return new SigninResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Claims = claimsDB.Count(),
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion,
                Project= project
            };

        }


    }
}
