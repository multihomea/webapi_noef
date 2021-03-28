using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProductManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductManagerController : ControllerBase
    {
        private readonly IProductModule _productModule;

        public ProductManagerController(IProductModule productModule)
        {
            _productModule = productModule;
        }

        // GET: api/Product
        [Route("GetProducts")]
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _productModule.GetProducts();
            var productList = products.ToList();
            if (productList == null || productList.Count == 0)
            {
                return NotFound();
            }
            return productList;
        }

        // POST: api/Product
        [Route("PostProduct")]
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            product.ProductId = await _productModule.Add(product);
            return CreatedAtAction("PostProduct", product);
        }
    }
}
