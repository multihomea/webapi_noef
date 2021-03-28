using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductManager
{
    public class ProductModule : IProductModule
    {
        private readonly IProductRepository _productRepository;
        public ProductModule(IProductRepository productRepository) 
        { 
            _productRepository = productRepository; 
        }

        async Task<Guid> IProductModule.Add(Product product)
        {
            return await _productRepository.Add(product);
        }

        IEnumerable<Product> IProductModule.GetProducts()
        {
            return _productRepository.GetProducts();
        }
    }

}