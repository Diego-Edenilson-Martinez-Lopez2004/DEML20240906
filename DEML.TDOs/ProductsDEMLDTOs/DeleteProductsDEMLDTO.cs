using System.ComponentModel.DataAnnotations;

namespace DEML.DTOs.ProductsDEMLDTOs
{
    public class DeleteProductsDEMLDTO
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public int Id { get; set; }
    }
}
