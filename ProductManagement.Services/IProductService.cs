using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Services
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetAll(string category = "", string subCategory = "");
        Task<bool> AddProduct(ProductModel model);
        Task<bool> UpdateProduct(ProductModel model);
        Task<bool> DeleteProduct(string code);
        Task<bool> ProductExists(string code);
        Task<ProductModel> GetByCode(string code);
    }
}
