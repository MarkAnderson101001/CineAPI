using System.ComponentModel.DataAnnotations;

namespace Cine.DTO.DTOPelicula
{
    public class DTOPeliculaP
    {
        [Required]
        [StringLength(50)]
        public string NombreP { get; set; }
        public DateTime FechaEstrenoP { get; set; }
        public bool Encine { get; set; }
    }
}
