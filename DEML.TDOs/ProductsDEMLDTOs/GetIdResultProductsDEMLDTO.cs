using System.ComponentModel.DataAnnotations;

namespace DEML.DTOs.ProductsDEMLDTOs
{
    public class GetIdResultProductsDEMLDTO
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string NombreDEML { get; set; }

        [Display(Name = "Descripción")]
        public string DescripcionDEML { get; set; }

        [Display(Name = "Precio")]
        public decimal Precio { get; set; }
    }
}
