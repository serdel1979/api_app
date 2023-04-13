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
                                .Include(x=> x.Users)
                                .FirstOrDefaultAsync(x => x.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<ProjectRespDTO>(project);
            return dto;
        }

        [HttpPost("assign/{idProject:int}/user/{idUser}")]
        public async Task<ActionResult> Assign(int idProject, string idUser)
        {
            var user = await context.Users.Include(x => x.Responsability).Include(x => x.Project)
                           .FirstOrDefaultAsync(x => x.Id == idUser);

            if (user == null)
            {
                return NotFound("El usuario no existe");
            }

            if(user.Project == null)
            {
                user.ProjectId = idProject;
                await context.SaveChangesAsync();
            }
            return Ok();
        }

       
       [HttpPost("confirmstaff")]
       public async Task<ActionResult> ConfirmStaff(ConfirmStaffDTO staffActivities)
       {

            // crear o recuperar el reporte del día de hoy
            var todayReport = await context.Reports.FirstOrDefaultAsync(r => r.Date == DateTime.Today);
            if (todayReport == null)
            {
                todayReport = new Report
                {
                    Date = DateTime.Today,
                    ProjectId = staffActivities.ProjectId,
                    UserId = staffActivities.UserId,
                    Status = "PENDIENTE"
                };
                await context.Reports.AddAsync(todayReport);
                await context.SaveChangesAsync();
            }

            foreach (var staff in staffActivities.Staff)
            {
                // asignar el projecto a cada usuario
                var user = await context.Users.FirstOrDefaultAsync(u => u.Id == staff.UserId);
                if (user != null)
                {
                    user.ProjectId = staffActivities.ProjectId;
                    context.Users.Update(user);
                }

                // asignar el reporte a cada developed activity
                foreach (var activity in staff.Activities)
                {
                    var developedActivity = await context.Developed_activities
                        .FirstOrDefaultAsync(da => da.Description == activity.Description);
                    if (developedActivity == null)
                    {
                        // si la actividad no existe, crearla
                        developedActivity = new Developed_Activity
                        {
                            Description = activity.Description,
                            ReportId = todayReport.Id
                        };
                        context.Developed_activities.Add(developedActivity);
                        await context.SaveChangesAsync();
                    }

                    // agregar el report detail al reporte del día de hoy
                    var reportDetail = new Report_detail
                    {
                        UserId = staffActivities.UserId,
                        ReportId = todayReport.Id,
                        Entry_Time = DateTime.ParseExact(staff.Entry_time, "HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                        Departure_time = DateTime.ParseExact(staff.Departure_time,"HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                        Report = todayReport
                    };
                    context.Reports_detail.Add(reportDetail);

                    //consultar si existe la actividad para el usuario
                    var existingAssignment = await context.Assigned_Activities
                         .FirstOrDefaultAsync(a => a.UserId == staff.UserId && a.Developed_ActivityId == developedActivity.Id);

                    if (existingAssignment != null)
                    {
                        var assignedActivity = new Assigned_Activity
                        {
                            UserId = user.Id,
                            Developed_Activity = developedActivity
                        };
                        context.Assigned_Activities.Add(assignedActivity);
                    }
                    

                }

            }

            await context.SaveChangesAsync();

            return Ok();
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
                Status = "PENDIENTE",
                UserId = report.UserId,
                Date = DateTime.Now.Date,
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
