using api_app.DTO;
using api_app.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace api_app.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<UserNewDTO, User>();
            CreateMap<User, UserResponseDTO>();
            CreateMap<ResponsabilityNewDTO, Responsability>();
            CreateMap<Responsability, ResponsabilityRespDTO>();
            CreateMap<JobNewDTO, Job>();
            CreateMap<Job, JobRespDTO>();
            //CreateMap<Solicitud, SolicitudDTO>().ReverseMap();
            //CreateMap<NuevaSolicitudDTO, Solicitud>();
            //CreateMap<IdentityUser, UsuarioDTO>().ReverseMap(); ;
            //CreateMap<Equipo, EquipoDTO>().ReverseMap();


        }
    }
}
