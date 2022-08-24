using Cine.Utilerias.Validations;
using System.ComponentModel.DataAnnotations;

namespace Cine.DTO.DTOActor
{
    public class DTOActorC
    {
        [Required]
        [StringLength(40)]
        public string NombreA { get; set; }
        public DateTime FechaNacimientoA { get; set; }
        [PesoArchivoValidacion(maxpeso:4)]
        [TipoArchivo(Grupotipoarchivo:GrupoTipoArchivo.Imagen)]
        public IFormFile FotoA { get; set; }
    }
}
