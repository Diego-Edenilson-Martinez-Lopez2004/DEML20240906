using DEML20240906.Models.EN;
using Microsoft.EntityFrameworkCore;

namespace DEML20240906.Models.DAL
{
    public class ProductsDEMLDAL
    {

        readonly DEMLContext _context;

        // Constructor que recibe un objeto DEMLContext para
        // interactuar con la base de datos.
        public ProductsDEMLDAL(DEMLContext context)
        {
            _context = context;
        }

        // Método para crear un nuevo producto en la base de datos.
        public async Task<int> Create(ProductsDEML product)
        {
            _context.productsDEML.Add(product);
            return await _context.SaveChangesAsync();
        }

        // Método para obtener un producto por su ID.
        public async Task<ProductsDEML> GetById(int id)
        {
            var product = await _context.productsDEML.FirstOrDefaultAsync(p => p.Id == id);
            return product != null ? product : new ProductsDEML();
        }

        // Método para editar un producto existente en la base de datos.
        public async Task<int> Edit(ProductsDEML product)
        {
            int result = 0;
            var productUpdate = await GetById(product.Id);
            if (productUpdate.Id != 0)
            {
                // Actualiza los datos del producto.
                productUpdate.NombreDEML = product.NombreDEML;
                productUpdate.DescripcionDEML = product.DescripcionDEML;
                productUpdate.Precio = product.Precio;
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        // Método para eliminar un producto de la base de datos por su ID.
        public async Task<int> Delete(int id)
        {
            int result = 0;
            var productDelete = await GetById(id);
            if (productDelete.Id > 0)
            {
                _context.productsDEML.Remove(productDelete);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        // Método privado para construir una consulta IQueryable para buscar productos con filtros.
        private IQueryable<ProductsDEML> Query(ProductsDEML product)
        {
            var query = _context.productsDEML.AsQueryable();
            if (!string.IsNullOrWhiteSpace(product.NombreDEML))
                query = query.Where(p => p.NombreDEML.Contains(product.NombreDEML));
            if (!string.IsNullOrWhiteSpace(product.DescripcionDEML))
                query = query.Where(p => p.DescripcionDEML.Contains(product.DescripcionDEML));
            return query;
        }

        // Método para contar la cantidad de resultados de búsqueda con filtros.
        public async Task<int> CountSearch(ProductsDEML product)
        {
            return await Query(product).CountAsync();
        }

        // Método para buscar productos con filtros, paginación y ordenamiento.
        public async Task<List<ProductsDEML>> Search(ProductsDEML product, int take = 10, int skip = 0)
        {
            take = take == 0 ? 10 : take;
            var query = Query(product);
            query = query.OrderByDescending(p => p.Id).Skip(skip).Take(take);
            return await query.ToListAsync();
        }

    }
}
