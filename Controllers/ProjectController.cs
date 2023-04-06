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

            Console.WriteLine("Actividades desarrolladas----");
            for (int i=0; i < report.Activities_developed.Length; i++)
            {
                Console.WriteLine($"    {report.Activities_developed[i].Description}");
            }

            Console.WriteLine("Actividades a desarrollar mañana----");

            for (int i = 0; i < report.Activity_to_Dev.Length; i++)
            {
                Console.WriteLine($"    {report.Activity_to_Dev[i].Description}");
            }

            Console.WriteLine("Necesidades para mañana----");

            for (int i = 0; i < report.Need_next_day.Length; i++)
            {
                Console.WriteLine($"    {report.Need_next_day[i].Description}");
            }


            Console.WriteLine("Observaciones----");

            for (int i = 0; i < report.Observations.Length; i++)
            {
                Console.WriteLine(report.Observations[i].Description);

                Console.WriteLine("Fotos incluidas");
                for(int j=0; j < report.Observations[i].Photos.Length; j++)
                {
                    //Console.Write('Foto --> ');
                    Console.WriteLine($"            {report.Observations[i].Photos[j].Description}");
                }
            }

            return Ok(report);
        }

    }
}
