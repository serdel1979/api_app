using api_app.DTO;
using api_app.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace api_app.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            
            CreateMap<Need_next_dayNewDTO, Need_next_day>();
            CreateMap<Need_next_day, Need_next_dayNewDTO>();

            CreateMap<Developed_ActivityNewDTO, Developed_Activity>();
            CreateMap<Developed_Activity, Developed_ActivityNewDTO>();

            CreateMap<Activity_next_dayNewDTO, Activity_next_day>();
            CreateMap<Activity_next_day, Activity_next_dayNewDTO>();

            CreateMap<UserNewDTO, User>();
            CreateMap<User, UserResponseDTO>()
                .ForMember(x => x.Responsability, options => options.MapFrom(MapResponsability))
                .ForMember(x => x.Project, options => options.MapFrom(MapProject));
            CreateMap<ResponsabilityNewDTO, Responsability>();
            CreateMap<Responsability, ResponsabilityRespDTO>();
            CreateMap<JobNewDTO, Job>();
            CreateMap<Job, JobRespDTO>();
            CreateMap<ProjectNewDTO, Project>();
            CreateMap<Project, ProjectRespDTO>()
                .ForMember(x => x.Job, options => options.MapFrom(MapJob))
                .ForMember(x => x.Users, options => options.MapFrom(MapUsers));

        }


        private List<UserResponseDTO> MapUsers(Project project, ProjectRespDTO projectResponseDto)
        {

            var result = new List<UserResponseDTO>();

            if (project.Users == null) { return result; }

            foreach (var user in project.Users)
            {
                result.Add(new UserResponseDTO()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Surname = user.Surname,
                    Dni = user.Dni,
                    ResponsabilityId = (int)user.ResponsabilityId,
                    ProjectId = (int)user.ProjectId,
                    Leader = user.Leader 
                });
            }
            return result;
        }

        private Job MapJob(Project project, ProjectRespDTO projectResponseDto)
        {

            //acá puedo agregar lógica de mapeo entre los dto

            return project.Job;
        }

        //private User MapLeader(Project project, ProjectRespDTO projectResponseDto)
        //{

        //    //acá puedo agregar lógica de mapeo entre los dto

        //    return project.Leader;
        //}

        private Responsability MapResponsability(User user, UserResponseDTO userResponseDto)
        {
   
            //acá puedo agregar lógica de mapeo entre los dto

            return user.Responsability;
        }


        private Project MapProject(User user, UserResponseDTO userResponseDto)
        {

            //acá puedo agregar lógica de mapeo entre los dto

            return user.Project;
        }
    }
}
