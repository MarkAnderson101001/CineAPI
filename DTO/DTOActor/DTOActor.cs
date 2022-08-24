using System.ComponentModel.DataAnnotations;

namespace Cine.DTO.DTOActor
{
    public class DTOActor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string NombreA { get; set; }
        public DateTime FechaNacimientoA { get; set; }
        
        public string FotoA { get; set; }

    }
}
