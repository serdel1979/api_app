using api_app.DTO;
using api_app.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_app.Controllers
{

    [ApiController]
    [Route("project")]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ProjectController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpPost("add")]
        public async Task<ActionResult> Add(ProjectNewDTO projectNewDto)
        {
            var project = mapper.Map<Project>(projectNewDto);

            context.Add(project);
            await context.SaveChangesAsync();
            var projectResponse = mapper.Map<ProjectRespDTO>(project);

            return Ok("El proyecto fue agregado!!!");
        }


        [HttpGet("getProject/{id:int}")]
        public async Task<ActionResult<ProjectRespDTO>> GetProject(int id)
        {
            var project = await context.Projects
                                .Include(x => x.Leader)
                                .Include(x => x.Job)
                                .FirstOrDefaultAsync(x => x.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<ProjectRespDTO>(project);
            return dto;
        }

    }
}
