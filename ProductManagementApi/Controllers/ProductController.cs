using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models;
using ProductManagement.Services;

namespace ProductManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await productService.GetAll());
        }

        // GET: api/products/5
        #region GetByID
        [HttpGet("{code}")]
        public async Task<IActionResult> GetProduct(string code)
        {
            var product = await productService.GetByCode(code);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        #endregion

        // PUT: api/products/5
        #region Update
        [HttpPut("{code}")]
        public async Task<IActionResult> PutProduct(string code, ProductModel model)
        {
            if (code != model.Code)
            {
                return BadRequest();
            }

            if (!(await productService.ProductExists(model.Code)))
            {
                return BadRequest();
            }
            return Ok(productService.UpdateProduct(model));
        }
        #endregion

        // POST: api/products
        #region Create
        [HttpPost]
        public async Task<IActionResult> PostProduct(ProductModel model)
        {
            if (await productService.ProductExists(model.Code))
            {
                return BadRequest("Product with same code already exists.");
            }
            return Ok(await productService.AddProduct(model));
        }
        #endregion

        // DELETE: api/products/5
        #region Delete
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteProduct(string code)
        {
            if (!(await productService.ProductExists(code)))
            {
                return NotFound();
            }

            return Ok(await productService.DeleteProduct(code));
        }
        #endregion
    }
}
