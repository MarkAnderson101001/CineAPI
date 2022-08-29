namespace Cine.DTO.DTOZ
{
    public class DTOPaginacion
    {
        public int Pagina { get; set; } = 1;

        private int cantidadRegistrosPP    = 10;
        private readonly int cantidadmaxPP = 50;

        public int CantidadRegistrosPP
        {
            get => cantidadRegistrosPP;
            set { cantidadRegistrosPP = (value > cantidadmaxPP) ? cantidadmaxPP : value; }
        }
    
    }
}
