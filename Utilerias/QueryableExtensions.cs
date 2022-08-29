using Cine.DTO.DTOZ;

namespace Cine.Utilerias
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> _queryable, DTOPaginacion _paginaciondto)
        {
            return _queryable
                .Skip((_paginaciondto.Pagina - 1) * _paginaciondto.CantidadRegistrosPP)
                .Take(_paginaciondto.CantidadRegistrosPP);

        }
    }
}
