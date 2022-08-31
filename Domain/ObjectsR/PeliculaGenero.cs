using Cine.Domain.Objects;

namespace Cine.Domain.ObjectsR
{
    public class PeliculaGenero
    {
        public int PeliculaID { get; set; }
        public int GeneroID   { get;set; }

        public OPelicula Pelicula { get; set; }
        public OGenero   Genero   { get;set; }
    }
}
