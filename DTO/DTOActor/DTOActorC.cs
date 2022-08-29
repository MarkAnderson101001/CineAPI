using Cine.Utilerias.Validations;
using System.ComponentModel.DataAnnotations;

namespace Cine.DTO.DTOActor
{
    public class DTOActorC : DTOActorP
    {
        [PesoArchivoValidacion(maxpeso:4)]
        [TipoArchivo(Grupotipoarchivo:GrupoTipoArchivo.Imagen)]
        public IFormFile FotoA { get; set; }
    }
}
