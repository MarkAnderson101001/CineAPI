 using Cine.Domain.Objects;

namespace Cine.Domain.ObjectsR
{
    public class PeliculaActor
    {
        public int PeliculaID   { get; set; }
        public int ActorID      { get; set; }

        public string Personaje { get; set; }
        public int    Orden     { get; set; }

        public OPelicula Pelicula { get; set; }
        public OActor Actor       { get; set; }
    }
}
