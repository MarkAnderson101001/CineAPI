using Cine.DTO.DTO;
using Cine.Utilerias.Model_Binder;
using Cine.Utilerias.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Cine.DTO.DTOPelicula
{
    public class DTOPeliculaC : DTOPeliculaP
    {
    

        [PesoArchivoValidacion(maxpeso:4)]
        [TipoArchivo(GrupoTipoArchivo.Imagen)]
        public IFormFile FotoP                 { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GeneroIDs             { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<DTOPeliculaActor>>))]
        public List<DTOPeliculaActor> ActoresE { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> SalasIDs              { get; set; }
    }
}
