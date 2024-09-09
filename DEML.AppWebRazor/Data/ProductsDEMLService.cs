using DEML.DTOs.ProductsDEMLDTOs;

namespace DEML.AppWebRazor.Data
{
    public class ProductsDEMLService
    {
        private readonly HttpClient _httpClientDEML;

        // Constructor que recibe una instancia de IHttpClientFactory para crear el cliente HTTP
        public ProductsDEMLService(IHttpClientFactory httpClientFactory)
        {
            _httpClientDEML = httpClientFactory.CreateClient("DEMLAPI");
        }

        // Método para buscar productos utilizando una solicitud HTTP POST
        public async Task<SearchResultProductsDEMLDTO> Search(SearchQueryProductsDEMLDTO searchQueryDEMLDTO)
        {
            var response = await _httpClientDEML.PostAsJsonAsync("/product/search", searchQueryDEMLDTO);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<SearchResultProductsDEMLDTO>();
                return result ?? new SearchResultProductsDEMLDTO();
            }
            return new SearchResultProductsDEMLDTO(); // Devolver un objeto vacío en caso de error o respuesta no exitosa
        }

        // Método para obtener un producto por su ID utilizando una solicitud HTTP GET
        public async Task<GetIdResultProductsDEMLDTO> GetById(int id)
        {
            var response = await _httpClientDEML.GetAsync("/product/" + id);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetIdResultProductsDEMLDTO>();
                return result ?? new GetIdResultProductsDEMLDTO();
            }
            return new GetIdResultProductsDEMLDTO(); // Devolver un objeto vacío en caso de error o respuesta no exitosa
        }

        // Método para crear un nuevo Producto utilizando una solicitud HTTP POST
        public async Task<int> Create(CreateProductsDEMLDTO createDEMLDTO)
        {
            int result = 0;
            var response = await _httpClientDEML.PostAsJsonAsync("/product", createDEMLDTO);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }

        // Método para editar un producto existente utilizando una solicitud HTTP PUT
        public async Task<int> Edit(EditProductsDEMLDTO editDEMLDTO)
        {
            int result = 0;
            var response = await _httpClientDEML.PutAsJsonAsync("/product", editDEMLDTO);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }

        // Método para eliminar un producto por su ID utilizando una solicitud HTTP DELETE
        public async Task<int> Delete(int id)
        {
            int result = 0;
            var response = await _httpClientDEML.DeleteAsync("/product/" + id);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }

    }
}
