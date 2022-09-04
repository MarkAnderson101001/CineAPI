using Cine.Domain.ObjectsR;
using System.ComponentModel.DataAnnotations;

namespace Cine.Domain.Objects
{
    public class OPelicula
    {
        public int      Id            { get; set; }
        [Required]
        [StringLength(50)]
        public string   NombreP       { get; set; }
        public DateTime FechaEstrenoP { get; set; }
        public string   FotoP         { get; set; }
        public bool     Encine        { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        public List<PeliculaActor>  PeliculaActor  { get; set; }
        public List<PeliculaGenero> PeliculaGenero { get; set; }
        public List<PeliculaSala>   PeliculaSala   { get; set; }

    }
}
