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

            CreateMap<UserNewDTO, User>();
            CreateMap<User, UserResponseDTO>()
                .ForMember(x => x.Responsability, options => options.MapFrom(MapResponsability));
            CreateMap<ResponsabilityNewDTO, Responsability>();
            CreateMap<Responsability, ResponsabilityRespDTO>();
            CreateMap<JobNewDTO, Job>();
            CreateMap<Job, JobRespDTO>();
            CreateMap<ProjectNewDTO, Project>();
            CreateMap<Project, ProjectRespDTO>()
                .ForMember(x => x.Leader, options => options.MapFrom(MapLeader))
                .ForMember(x=> x.Job, options => options.MapFrom(MapJob));

           

        }


        private Job MapJob(Project project, ProjectRespDTO projectResponseDto)
        {

            //acá puedo agregar lógica de mapeo entre los dto

            return project.Job;
        }

        private User MapLeader(Project project, ProjectRespDTO projectResponseDto)
        {

            //acá puedo agregar lógica de mapeo entre los dto

            return project.Leader;
        }

        private Responsability MapResponsability(User user, UserResponseDTO userResponseDto)
        {
   
            //acá puedo agregar lógica de mapeo entre los dto

            return user.Responsability;
        }
    }
}
