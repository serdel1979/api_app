using api_app.DTO;
using api_app.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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

            return Ok(projectResponse);
        }

    }
}
