using DEML.DTOs.ProductsDEMLDTOs;
using Microsoft.AspNetCore.Mvc;

namespace DEML.AppWedMVC.Controllers
{
    public class ProductsDEMLController : Controller
    {
        private readonly HttpClient _httpClientDEML;

        // Constructor que recibe una instancia de IHttpClientFactory para crear el cliente HTTP
        public ProductsDEMLController(IHttpClientFactory httpClientFactory)
        {
            _httpClientDEML = httpClientFactory.CreateClient("DEMLAPI");
        }

        // Método para mostrar la lista de productos
        public async Task<IActionResult> Index(SearchQueryProductsDEMLDTO searchQueryDEMLDTO, int countRow = 0)
        {
            if (searchQueryDEMLDTO.SendRowCount == 0)
                searchQueryDEMLDTO.SendRowCount = 2;
            if (searchQueryDEMLDTO.Take == 0)
                searchQueryDEMLDTO.Take = 10;

            var result = new SearchResultProductsDEMLDTO();

            // Realizar una solicitud HTTP POST para buscar productos en el servicio web
            var response = await _httpClientDEML.PostAsJsonAsync("/product/search", searchQueryDEMLDTO);

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<SearchResultProductsDEMLDTO>();
            }

            ViewBag.CountRow = result.CountRow;
            searchQueryDEMLDTO.SendRowCount = 0;
            ViewBag.SearchQuery = searchQueryDEMLDTO;

            return View(result);
        }

        // Método para mostrar los detalles de un producto
        public async Task<IActionResult> Details(int id)
        {
            var result = new GetIdResultProductsDEMLDTO();

            // Realizar una solicitud HTTP GET para obtener los detalles del producto por ID
            var response = await _httpClientDEML.GetAsync("/product/" + id);

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<GetIdResultProductsDEMLDTO>();
            }

            return View(result);
        }

        // Método para mostrar el formulario de creación de un producto
        public ActionResult Create()
        {
            return View();
        }

        // Método para procesar la creación de un producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductsDEMLDTO createProductDEMLDTO)
        {
            try
            {
                var response = await _httpClientDEML.PostAsJsonAsync("/product", createProductDEMLDTO);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar guardar el registro";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // Método para mostrar el formulario de edición de un producto
        public async Task<IActionResult> Edit(int id)
        {
            var result = new GetIdResultProductsDEMLDTO();

            var response = await _httpClientDEML.GetAsync("/product/" + id);

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<GetIdResultProductsDEMLDTO>();
            }

            return View(new EditProductsDEMLDTO(result));
        }

        // Método para procesar la edición de un producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditProductsDEMLDTO editProductDEMLDTO)
        {
            try
            {
                var response = await _httpClientDEML.PutAsJsonAsync("/product", editProductDEMLDTO);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar editar el registro";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // Método para mostrar la página de confirmación de eliminación de un producto
        public async Task<IActionResult> Delete(int id)
        {
            var result = new GetIdResultProductsDEMLDTO();

            var response = await _httpClientDEML.GetAsync("/product/" + id);

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<GetIdResultProductsDEMLDTO>();
            }

            return View(result);
        }

        // Método para procesar la eliminación de un producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, GetIdResultProductsDEMLDTO getIdResultDEMLDTO)
        {
            try
            {
                var response = await _httpClientDEML.DeleteAsync("/product/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar eliminar el registro";
                return View(getIdResultDEMLDTO);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(getIdResultDEMLDTO);
            }
        }

    }
}
