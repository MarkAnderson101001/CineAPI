using Cine.Domain.Objects;

namespace Cine.Domain.ObjectsR
{
    public class PeliculaActor
    {
        public int PeliculaID   { get; set; }
        public int ActorID      { get; set; }

        public string Personaje { get; set; }
        public int    Orden     { get; set; }

        public OPelicula PeliculaE { get; set; }
        public OActor ActorE       { get; set; }
    }
}
