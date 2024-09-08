using DEML20240906.Models.EN;
using Microsoft.EntityFrameworkCore;

namespace DEML20240906.Models.DAL
{
    public class DEMLContext : DbContext
    {
        public DEMLContext(DbContextOptions<DEMLContext> options) : base(options)
        {

        }

        public DbSet<ProductsDEML> productsDEML { get; set; }
    }
}
