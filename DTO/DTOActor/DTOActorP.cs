using System.ComponentModel.DataAnnotations;

namespace Cine.DTO.DTOActor
{
    public class DTOActorP
    {
        [Required]
        [StringLength(40)]
        public string NombreA { get; set; }
        public DateTime FechaNacimientoA { get; set; }
    }
}
