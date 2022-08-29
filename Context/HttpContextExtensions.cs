using Microsoft.EntityFrameworkCore;

namespace Cine.Context
{
    public static class HttpContextExtensions 
    {
        public async static Task InsertarPaginacion<T>(this HttpContext _htppcontext,IQueryable<T> _queryable,int _registrospp)
        {
            double cantidad   = await _queryable.CountAsync();
            double cantidadpp = Math.Ceiling(cantidad/_registrospp);

            _htppcontext.Response.Headers.Add("CantidadPaginas", cantidadpp.ToString());
            
        }
    }
}
