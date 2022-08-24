using System.ComponentModel.DataAnnotations;

namespace Cine.DTO.DTOGenero
{
    public class DTOGenero
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Genero { get; set; }
    }
}
