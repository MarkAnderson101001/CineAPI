using System.ComponentModel.DataAnnotations;

namespace Cine.Utilerias.Validations
{
    public class TipoArchivo : ValidationAttribute
    {
        private readonly string[] validos;

        public TipoArchivo(string[] Validos)
        {
            validos = Validos;
        }

        public TipoArchivo(GrupoTipoArchivo Grupotipoarchivo)
        {
            if(Grupotipoarchivo == GrupoTipoArchivo.Imagen)
            {
                validos = new string[] { "image/jpeg","image/jpg","image/png", "image/gif" };
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            IFormFile formfile = value as IFormFile;
            if (formfile == null)
            {
                return ValidationResult.Success;
            }

            if (!validos.Contains(formfile.ContentType))
            {
                return new ValidationResult($"el tipo de archivo es incompatible, coloque uno de los siguientes:{string.Join(", ",validos )}");
            }
            return ValidationResult.Success;
        }
    }
}
