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
                              //  .Include(x => x.Leader)
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

            //context.Add(project);
            //await context.SaveChangesAsync();

            //primero tengo que guardar un nuevo reporte con fecha actual perteneciente a un proyecto
            Console.WriteLine($"id del proyecto: {report.ProjectId}");
            Console.WriteLine($"Reporte: {report.Reported}");

            var reportNew = new Report
            {
                ProjectId = report.ProjectId,
                UserId = report.UserId,
                Date = DateTime.Now,
            };
            context.Add(reportNew);
            await context.SaveChangesAsync();
            //se guardó entidad reporte
            //recorro el detalle recibido

            for (int i = 0; i < report.Detail.Length; i++)
            {
                Console.WriteLine($"Empleado {report.Detail[i].UserId} {report.Detail[i].Entry_time} {report.Detail[i].Departure_time}");
                // DateTime.ParseExact(hora,"HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture);
                var report_detail = new Report_detail
                {
                    UserId = report.Detail[i].UserId,
                    Report = reportNew,
                    Entry_Time = DateTime.ParseExact(report.Detail[i].Entry_time, "HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                    Departure_time = DateTime.ParseExact(report.Detail[i].Departure_time, "HH:mm", System.Globalization.CultureInfo.InvariantCulture)
                };
                context.Add(report_detail);
                await context.SaveChangesAsync();
            }

            Console.WriteLine("Actividades desarrolladas----");
            for (int i=0; i < report.Activities_developed.Length; i++)
            {
                Console.WriteLine($"    {report.Activities_developed[i].Description}");

                var activityDeveloped = new Developed_Activity
                {
                    Description = report.Activities_developed[i].Description,
                    Report = reportNew
                };
                context.Add(activityDeveloped);
                await context.SaveChangesAsync();
            }

            Console.WriteLine("Actividades a desarrollar mañana----");

            for (int i = 0; i < report.Activity_to_Dev.Length; i++)
            {
                Console.WriteLine($"    {report.Activity_to_Dev[i].Description}");
                var activityNextDay = new Activity_next_day
                {
                    Description = report.Activity_to_Dev[i].Description,
                    Report = reportNew
                };
                context.Add(activityNextDay);
                await context.SaveChangesAsync();
            }

            Console.WriteLine("Necesidades para mañana----");

            for (int i = 0; i < report.Need_next_day.Length; i++)
            {
                Console.WriteLine($"    {report.Need_next_day[i].Description}");
                var needNextDay = new Need_next_day
                {
                    Description = report.Need_next_day[i].Description,
                    Report = reportNew
                };
                context.Add(needNextDay);
                await context.SaveChangesAsync();
            }


            Console.WriteLine("Observaciones----");

            for (int i = 0; i < report.Observations.Length; i++)
            {
                Console.WriteLine(report.Observations[i].Description);
                var observation = new Observation
                {
                    Description = report.Observations[i].Description,
                    Report = reportNew
                };
                context.Add(observation);
                await context.SaveChangesAsync();

                Console.WriteLine("Fotos incluidas");
                for(int j=0; j < report.Observations[i].Photos.Length; j++)
                {
                    //Console.Write('Foto --> ');
                    Console.WriteLine($"            {report.Observations[i].Photos[j].Description}");

                    var photo = new Photo
                    {
                        Image = report.Observations[i].Photos[j].Image,
                        Description = report.Observations[i].Photos[j].Description,
                        Observation = observation
                    };
                    context.Add(photo);
                    await context.SaveChangesAsync();
                }
            }

            return Ok(report);
        }

    }
}
