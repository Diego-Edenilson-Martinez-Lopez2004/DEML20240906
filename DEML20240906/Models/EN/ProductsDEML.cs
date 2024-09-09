
using System.ComponentModel.DataAnnotations;

namespace DEML20240906.Models.EN
{
    public class ProductsDEML
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public int Id { get; set; }

        public string NombreDEML { get; set; }

        public string DescripcionDEML { get; set; }

        public decimal Precio { get; set; }

    }
}

