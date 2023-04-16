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


       [HttpPost("confirmObservations")]
       public async Task<ActionResult> ConfirmObservations(ObservationReportDTO observationsDTO)
       {

            var reportDetail = await context.Reports_detail.Include(r => r.Report).Where(repo => repo.Report.Date == DateTime.Today)
                       .FirstOrDefaultAsync(rd => rd.UserId == observationsDTO.UserId);

            if (reportDetail == null)
            {
                return BadRequest("Ud. no tiene reporte generado para hoy");
            }

            foreach (var observation in observationsDTO.Observations)
            {
                
                var observationNew = new Observation
                {
                    Description = observation.Description,
                    Report = reportDetail.Report
                };
                context.Add(observationNew);
                foreach (var photo in observation.Photos)
                {
                    var photoNew = new Photo
                    {
                        Image = photo.Image,
                        Description = photo.Description,
                        Observation = observationNew
                    };
                    context.Add(photoNew);
                }

            }
            await context.SaveChangesAsync();
            return Ok(observationsDTO);
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
                        .Include(r=>r.Report)
                        .FirstOrDefaultAsync(da => da.Description == activity.Description 
                          && da.ReportId == todayReport.Id);
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


                    var reportDetailexist = await context.Reports_detail.Include(r=>r.Report).Where(repo=>repo.ReportId == todayReport.Id)
                       .FirstOrDefaultAsync(rd=>rd.UserId  == staff.UserId);

              

                    if (reportDetailexist == null)
                    {
                        var reportDetail = new Report_detail
                        {
                            UserId = staff.UserId,
                            ReportId = todayReport.Id,
                            Entry_Time = DateTime.ParseExact(staff.Entry_time, "HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                            Departure_time = DateTime.ParseExact(staff.Departure_time, "HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                            Report = todayReport
                        };
                        context.Reports_detail.Add(reportDetail);
                    }
                    else
                    {
                        var Entry_Time = DateTime.ParseExact(staff.Entry_time, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                        var Departure_time = DateTime.ParseExact(staff.Departure_time, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                        reportDetailexist.Entry_Time = Entry_Time;
                        reportDetailexist.Departure_time = Departure_time;
                        context.Reports_detail.Update(reportDetailexist);
                    }
                   

                    //consultar si existe la actividad para el usuario
                    var existingAssignment = await context.Assigned_Activities
                         .FirstOrDefaultAsync(a => a.UserId == staff.UserId && a.Developed_ActivityId == developedActivity.Id
                         && a.Developed_Activity.ReportId == todayReport.Id);

                    if (existingAssignment == null)
                    {
                        var assignedActivity = new Assigned_Activity
                        {
                            UserId = user.Id,
                            Developed_Activity = developedActivity
                        };
                        context.Assigned_Activities.Add(assignedActivity);
                    }
                    

                }
                var activitiesAssiged = await context.Assigned_Activities
                    .Include(a => a.Developed_Activity)
                    .Where(a => a.UserId == staff.UserId && a.Developed_Activity.ReportId == todayReport.Id).ToListAsync();
                
                foreach (var ativityAsignDB in activitiesAssiged)
                {
                    var contain = false;
                    foreach(var staffActivity in staff.Activities)
                    {
                        if (staffActivity.Description == ativityAsignDB.Developed_Activity.Description)
                        {
                            contain = true;
                        }
                    }
                    if(!contain )
                    {
                        context.Assigned_Activities.Remove(ativityAsignDB);
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

            var reportExist = await context.Reports.Where(r => r.UserId == report.UserId && r.Date == DateTime.Today)
                .FirstOrDefaultAsync();

            if (reportExist == null)
            {
                return BadRequest("No existe un reporte creado!!!");
            }
            Console.WriteLine($"id del proyecto: {report.ProjectId}");
            Console.WriteLine($"Reporte: {report.Reported}");



           
            Console.WriteLine("Actividades a desarrollar mañana----");

            for (int i = 0; i < report.Activity_to_Dev.Length; i++)
            {
                Console.WriteLine($"    {report.Activity_to_Dev[i].Description}");
                var activityNextDay = new Activity_next_day
                {
                    Description = report.Activity_to_Dev[i].Description,
                    Report = reportExist
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
                    Report = reportExist
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
                    Report = reportExist
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
