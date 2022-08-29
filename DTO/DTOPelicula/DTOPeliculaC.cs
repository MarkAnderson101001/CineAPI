using Cine.Utilerias.Validations;
using System.ComponentModel.DataAnnotations;

namespace Cine.DTO.DTOPelicula
{
    public class DTOPeliculaC : DTOPeliculaP
    {
    

        [PesoArchivoValidacion(maxpeso:4)]
        [TipoArchivo(GrupoTipoArchivo.Imagen)]
        public IFormFile FotoP { get; set; }
        

    }
}
