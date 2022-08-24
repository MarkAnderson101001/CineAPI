using System.ComponentModel.DataAnnotations;

namespace Cine.DTO.DTOPelicula
{
    public class DTOPeliculaC
    {
        public int Id { get; set; }
        [Required]
        public string    NombreP { get; set; }
        public DateTime  FechaEstrenoP { get; set; }
        public IFormFile FotoP { get; set; }
    }
}
