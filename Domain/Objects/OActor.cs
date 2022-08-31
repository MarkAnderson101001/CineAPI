using Cine.Domain.ObjectsR;
using System.ComponentModel.DataAnnotations;

namespace Cine.Domain.Objects
{
    public class OActor
    {
        public int Id                    { get; set; }
        [Required]
        [StringLength(40)]
        public string   NombreA          { get; set; }
        public DateTime FechaNacimientoA { get; set; }
        public string   FotoA            { get; set; }
        ///////////////////////////////////////////////////////
       
        public List<PeliculaActor> PeliculaActor { get; set; }
    }
}
