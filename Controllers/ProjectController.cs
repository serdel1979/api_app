using api_app.DTO;
using api_app.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace api_app.Controllers
{

    [ApiController]
    [Route("project")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    // [Authorize(Policy = "EsAdmin")]
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

        [HttpPost("report")]
        public async Task<ActionResult> Report(ReportNewDTO report)
        {
            //primero tengo que guardar un nuevo reporte con fecha actual perteneciente a un proyecto y desp obtener id de reporte para el detalle
            Console.WriteLine($"id del proyecto: {report.ProjectId}");
            Console.WriteLine($"Reporte: {report.Report}");
            for (int i = 0; i < report.Detail.Length; i++)
            {
                Console.WriteLine($"Empleado {report.Detail[i].UserId} {report.Detail[i].Entry_time} {report.Detail[i].Departure_time}");
            }

            Console.WriteLine("Actividades desarrolladas");
            for (int i=0; i < report.Activities_developed.Length; i++)
            {
                Console.WriteLine(report.Activities_developed[i].Description);
            }

            return Ok(report);
        }

    }
}
