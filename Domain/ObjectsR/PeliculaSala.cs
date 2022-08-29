using Cine.Domain.Objects;

namespace Cine.Domain.ObjectsR
{
    public class PeliculaSala
    {
        public int PeliculaID { get; set; }
        public int SalaID     { get; set; }

        public OPelicula PeliculaE { get; set; }
        public OSala     SalaE     { get; set; }
    }
}
