using System.ComponentModel.DataAnnotations;

namespace DEML.DTOs.ProductsDEMLDTOs
{
    public class SearchQueryProductsDEMLDTO
    {
        [Display(Name = "Nombre")]
        public string? NombreDEML_Like { get; set; }

        [Display(Name = "Descripción")]
        public string? DescripcionDEML_Like { get; set; }

        [Display(Name = "Página")]
        public int Skip { get; set; }

        [Display(Name = "CantReg X Página")]
        public int Take { get; set; }

        /// <summary>
        /// 1 = No se cuenta los resultados de la búsqueda
        /// 2 = Cuenta los resultados de la búsqueda
        /// </summary>
        public byte SendRowCount { get; set; }
    }
}
