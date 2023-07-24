using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductManagement.Data.Entity;
using ProductManagement.Models;

namespace ProductManagement.Data
{
    public class ProductContext: DbContext
    {
        //private readonly IConfiguration configuration;
        //public ProductContext(IConfiguration configuration) { 
        //    this.configuration = configuration;
        //}

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    // connect to sql server with connection string from app settings
        //    options.UseSqlServer(configuration.GetConnectionString("WebApiDatabase"));
        //}

        public DbSet<Product> Products { get; set; } = null;
    }
}