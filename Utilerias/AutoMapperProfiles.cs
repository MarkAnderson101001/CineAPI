using AutoMapper;
using Cine.Domain.Objects;
using Cine.DTO.DTOActor;

namespace Cine.Utilerias
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
            CreateMap<OActor, DTOActor >().ReverseMap();
            CreateMap<OActor, DTOActorC>().ReverseMap();
        }
    }
}
