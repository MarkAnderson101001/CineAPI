using System.ComponentModel.DataAnnotations;

namespace Cine.Domain.Objects
{
    public class OGenero{

        public int Id           { get; set; }
        [Required]
        [StringLength(40)]
        public string Genero    { get; set; }
    }    
}
