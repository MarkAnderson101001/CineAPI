using AutoMapper;
using Cine.Domain.Objects;
using Cine.DTO.DTOActor;
using Cine.DTO.DTOGenero;
using Cine.DTO.DTOPelicula;
using Cine.DTO.DTOReview;
using Cine.DTO.DTOSala;
using Cine.DTO.DTOUsuario;

namespace Cine.Utilerias
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////
            CreateMap<OActor, DTOActor>().ReverseMap();
            CreateMap<OActor, DTOActorC>().ReverseMap().ForMember(x => x.FotoA, options => options.Ignore());
            CreateMap<OActor, DTOActorP>().ReverseMap();
            //////////////////////////////////////////////////////////////////////////////////////////////////
            CreateMap<OGenero, DTOGenero>().ReverseMap();
            CreateMap<OGenero, DTOGeneroC>().ReverseMap();
            //////////////////////////////////////////////////////////////////////////////////////////////////
            CreateMap<OPelicula, DTOPelicula>().ReverseMap();
            CreateMap<OPelicula, DTOPeliculaC>().ReverseMap().ForMember(x => x.FotoP, options => options.Ignore());
            CreateMap<OPelicula, DTOPeliculaP>().ReverseMap();
            //////////////////////////////////////////////////////////////////////////////////////////////////
            CreateMap<OReview, DTOReview>().ReverseMap();
            //////////////////////////////////////////////////////////////////////////////////////////////////
            CreateMap<OSala, DTOSala>().ReverseMap();
            //////////////////////////////////////////////////////////////////////////////////////////////////
            CreateMap<OUsuario, DTOUsuario>().ReverseMap();
        }
    }
}
