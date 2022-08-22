using System.ComponentModel.DataAnnotations;

namespace Cine.Domain.Objects
{
    public class OPelicula
    {
        public int      Id            { get; set; }
         [Required]
        public string   NombreP       { get; set; }
        public DateTime FechaEstrenoP { get; set; }
        public string   FotoP         { get; set; }

    }
}
