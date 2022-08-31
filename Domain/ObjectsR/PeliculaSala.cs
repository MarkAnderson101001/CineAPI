using Cine.Domain.Objects;

namespace Cine.Domain.ObjectsR
{
    public class PeliculaSala
    {
        public int PeliculaID { get; set; }
        public int SalaID     { get; set; }

        public OPelicula Pelicula { get; set; }
        public OSala     Sala     { get; set; }
    }
}
