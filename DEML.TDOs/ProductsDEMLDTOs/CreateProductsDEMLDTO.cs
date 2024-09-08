using System.ComponentModel.DataAnnotations;

namespace DEML.DTOs.ProductsDEMLDTOs
{
    public class CreateProductsDEMLDTO
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo Nombre no puede tener más de 100 caracteres.")]
        public string NombreDEML { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
        [MaxLength(255, ErrorMessage = "El campo Descripción no puede tener más de 255 caracteres.")]
        public string DescripcionDEML { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El campo Precio debe ser un valor positivo.")]
        public decimal Precio { get; set; }
    }
}
