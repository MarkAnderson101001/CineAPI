using Cine.DTO.DTOZ;

namespace Cine.DTO.DTO
{
    public class DTOPeliculaFiltro
    {
        public int Pagina { get; set; } = 1;
        public int CantidadPP { get; set; } = 10;
        
        public DTOPaginacion paginacion
        {
            get { return new DTOPaginacion() { Pagina = Pagina,CantidadRegistrosPP = CantidadPP }; }
        }

        public string titulo         { get; set; }
        public int    GeneroID       { get; set; }
        public bool   encines        { get; set; }
        public bool   proximoestreno { get; set; }


    }
}
