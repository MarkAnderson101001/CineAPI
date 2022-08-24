using System.ComponentModel.DataAnnotations;

namespace Cine.DTO.DTOGenero
{
    public class DTOGeneroC
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Genero { get; set; }
    }
}
