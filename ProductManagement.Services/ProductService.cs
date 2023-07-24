using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Data.Entity;
using ProductManagement.Models;

namespace ProductManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductContext _context;
        private readonly Dictionary<string, List<string>> categories = new();

        public ProductService(ProductContext context)
        {
            this._context = context;
            categories.Add("Electronics", new List<string> { "TV", "Mobile", "Refrigerator"});
            categories.Add("Apparel", new List<string> { "Men’s Cloth", "Women’s cloth" });
            categories.Add("Footwear", new List<string> { "Men’s Footwear", "kid’s footwear", "Women's footwear" });
        }

        public Task<bool> AddProduct(ProductModel model)
        {
            var product = ToEntity(model);
            _context.Products.Add(product);
            return Task.FromResult(SaveChanges());
        }

        public async Task<bool> DeleteProduct(string code)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Code == code);
            if (existingProduct == null) return false;
            _context.Products.Remove(existingProduct);
            return SaveChanges();
        }

        public Task<List<ProductModel>> GetAll(string category = "", string subCategory = "")
        {
            var products = _context.Products.Where(x=>(string.IsNullOrEmpty(category) || x.Category==category) && (string.IsNullOrEmpty(subCategory) || x.SubCategory==subCategory))?.Select(ToModel)?.ToList();
            return Task.FromResult(products);
        }

        public async Task<bool> ProductExists(string code)
        {
            return await _context.Products.AnyAsync(x=> x.Code==code);
        }

        public bool IsCategoryMappingCorrect(ProductModel model)
        {
            var cat = categories.Keys.FirstOrDefault(model.Category);
            if (cat==null) return false;
            return categories[cat].Any(x => x == model.SubCategory);
        }

        public async Task<ProductModel> GetByCode(string code)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Code == code);
            if (product == null) return null;
            return ToModel(product);
        }

        public async Task<bool> UpdateProduct(ProductModel model)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Code == model.Code);
            if (existingProduct == null) return false;
            existingProduct.Description = model.Description;
            existingProduct.Category = model.Category;
            existingProduct.SubCategory = model.SubCategory;
            existingProduct.Quantity = model.Quantity;
            existingProduct.Price = model.Price;
            _context.Products.Update(existingProduct);
            return SaveChanges();
        }

        private ProductModel ToModel(Product product)
        {
            if (product == null) return null;
            return new ProductModel { 
                Category = product.Category,
                SubCategory = product.SubCategory,
                Code = product.Code,
                Description = product.Description,
                //Images = product.Images,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity            
            };
        }

        private Product ToEntity(ProductModel model)
        {
            if (model == null) return null;
            return new Product
            {
                Category = model.Category,
                SubCategory = model.SubCategory,
                Code = model.Code,
                Description = model.Description,
                //Images = model.Images,
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity
            };
        }

        private bool SaveChanges()
        {
            var res = _context.SaveChanges();
            return res > 0;
        }
    }
}