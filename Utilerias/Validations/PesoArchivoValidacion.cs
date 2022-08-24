using System.ComponentModel.DataAnnotations;

namespace Cine.Utilerias.Validations
{
    public class PesoArchivoValidacion : ValidationAttribute
    {
        private readonly int maxpeso;

        public PesoArchivoValidacion (int maxpeso)
        {
            this.maxpeso = maxpeso;
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
            if (formfile.Length> maxpeso * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no debe de ser mayor a {maxpeso}mb.");
            }
            return ValidationResult.Success;
        }
    }
}
