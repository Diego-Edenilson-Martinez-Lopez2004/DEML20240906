using DEML.DTOs.ProductsDEMLDTOs;
using DEML20240906.Models.DAL;
using DEML20240906.Models.EN;

namespace DEML20240906.Endpoints
{
    public static class ProductsDEMLEndpoint
    {
        public static void AddProductEndpoints(this WebApplication app)
        {
            // Configurar un endpoint de tipo POST para buscar productos
            app.MapPost("/product/search", async (SearchQueryProductsDEMLDTO productDTO, ProductsDEMLDAL productDAL) =>
            {
                var product = new ProductsDEML
                {
                    NombreDEML = productDTO.NombreDEML_Like ?? string.Empty,
                    DescripcionDEML = productDTO.DescripcionDEML_Like ?? string.Empty
                };

                var products = new List<ProductsDEML>();
                int countRow = 0;

                if (productDTO.SendRowCount == 2)
                {
                    products = await productDAL.Search(product, skip: productDTO.Skip, take: productDTO.Take);
                    if (products.Count > 0)
                        countRow = await productDAL.CountSearch(product);
                }
                else
                {
                    products = await productDAL.Search(product, skip: productDTO.Skip, take: productDTO.Take);
                }

                var productResult = new SearchResultProductsDEMLDTO
                {
                    Data = products.Select(p => new SearchResultProductsDEMLDTO.ProductsDEMLDTO
                    {
                        Id = p.Id,
                        NombreDEML = p.NombreDEML,
                        DescripcionDEML = p.DescripcionDEML,
                        Precio = p.Precio
                    }).ToList(),
                    CountRow = countRow
                };

                return productResult;
            });

            // Configurar un endpoint de tipo GET para obtener un producto por ID
            app.MapGet("/product/{id}", async (int id, ProductsDEMLDAL productDAL) =>
            {
                var product = await productDAL.GetById(id);

                if (product.Id > 0)
                {
                    var productResult = new GetIdResultProductsDEMLDTO
                    {
                        Id = product.Id,
                        NombreDEML = product.NombreDEML,
                        DescripcionDEML = product.DescripcionDEML,
                        Precio = product.Precio
                    };
                    return Results.Ok(productResult);
                }

                return Results.NotFound();
            });

            // Configurar un endpoint de tipo POST para crear un nuevo producto
            app.MapPost("/product", async (CreateProductsDEMLDTO productDTO, ProductsDEMLDAL productDAL) =>
            {
                var product = new ProductsDEML
                {
                    NombreDEML = productDTO.NombreDEML,
                    DescripcionDEML = productDTO.DescripcionDEML,
                    Precio = productDTO.Precio
                };

                int result = await productDAL.Create(product);
                return result != 0 ? Results.Ok(result) : Results.StatusCode(500);
            });

            // Configurar un endpoint de tipo PUT para editar un producto existente
            app.MapPut("/product", async (EditProductsDEMLDTO productDTO, ProductsDEMLDAL productDAL) =>
            {
                var product = new ProductsDEML
                {
                    Id = productDTO.Id,
                    NombreDEML = productDTO.NombreDEML,
                    DescripcionDEML = productDTO.DescripcionDEML,
                    Precio = productDTO.Precio
                };

                int result = await productDAL.Edit(product);
                return result != 0 ? Results.Ok(result) : Results.StatusCode(500);
            });

            // Configurar un endpoint de tipo DELETE para eliminar un producto por ID
            app.MapDelete("/product/{id}", async (int id, ProductsDEMLDAL productDAL) =>
            {
                int result = await productDAL.Delete(id);
                return result != 0 ? Results.Ok(result) : Results.StatusCode(500);
            });
        }
    }
}
