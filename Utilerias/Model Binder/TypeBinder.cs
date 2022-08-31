using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Cine.Utilerias.Model_Binder
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nombrepropiedad  = bindingContext.ModelName;
            var proveedorvalores = bindingContext.ValueProvider.GetValue(nombrepropiedad);
            //////////////////////////////////////////////////
            if( proveedorvalores == ValueProviderResult.None){
                return Task.CompletedTask;
            }

            try
            {
                var deserializado     = JsonConvert.DeserializeObject<T>(proveedorvalores.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(deserializado);
            }
            catch{
                bindingContext.ModelState.TryAddModelError(nombrepropiedad, "valor invalido para la lista");
            }
            /////////////////////////////////////////
            return Task.CompletedTask;
        }
    }
}
