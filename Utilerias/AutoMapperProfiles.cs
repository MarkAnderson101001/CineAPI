using AutoMapper;
using Cine.Domain.Objects;
using Cine.Domain.ObjectsR;
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
            CreateMap<OPelicula, DTOPeliculaC>().ReverseMap().ForMember(x => x.FotoP         , opt => opt.Ignore())
                                                             .ForMember(x => x.PeliculaActor , opt => opt.MapFrom(MapPA))
                                                             .ForMember(x => x.PeliculaGenero, opt => opt.MapFrom(MapPG))
                                                             .ForMember(x => x.PeliculaSala  , opt => opt.MapFrom(MapPS));

            CreateMap<OPelicula, DTOPeliculaP>().ReverseMap();
            //////////////////////////////////////////////////////////////////////////////////////////////////
            CreateMap<OReview, DTOReview>().ReverseMap();
            //////////////////////////////////////////////////////////////////////////////////////////////////
            CreateMap<OSala, DTOSala>().ReverseMap();
            //////////////////////////////////////////////////////////////////////////////////////////////////
            CreateMap<OUsuario, DTOUsuario>().ReverseMap();
        }
        #region METODOS
        private List<PeliculaActor> MapPA(DTOPeliculaC dtoPC, OPelicula pelicula)
        {
            var result = new List<PeliculaActor>();
            //////////////////////////////////////////////////////////////////
            if (dtoPC.ActoresE == null)
            {
                return result;
            }

            //////////////////////////////////////////////////////////////////
            foreach (var actor in dtoPC.ActoresE)
            {
                result.Add(new PeliculaActor() { ActorID = actor.ActorID, Personaje = actor.Personaje });
            }
            /////////////////////////////////////////////////////////////////
            return result;
        }

        private List<PeliculaGenero> MapPG(DTOPeliculaC dtoPC, OPelicula pelicula)
        {
            var result = new List<PeliculaGenero>();
            //////////////////////////////////////////////////////////

            if (dtoPC.GeneroIDs == null)
            {
                return result;
            }
            foreach (var id in dtoPC.GeneroIDs)
            {
                result.Add(new PeliculaGenero() { GeneroID = id });
            }
            //////////////////////////////////////////////////////////

            return result;
        }
        private List<PeliculaSala> MapPS(DTOPeliculaC dtoPC, OPelicula pelicula)
        {
            var result = new List<PeliculaSala>();
            //////////////////////////////////////////////////////////

            if (dtoPC.SalasIDs == null)
            {
                return result;
            }
            foreach (var id in dtoPC.SalasIDs)
            {
                result.Add(new PeliculaSala() { SalaID = id });
            }
            //////////////////////////////////////////////////////////

            return result;
        }
        #endregion

    }
}

