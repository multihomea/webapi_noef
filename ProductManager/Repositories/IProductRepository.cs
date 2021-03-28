using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManager
{
    public interface IProductRepository
    {
        Task<Guid> Add(Product product);
        IEnumerable<Product> GetProducts();
    }
}