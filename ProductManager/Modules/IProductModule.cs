using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManager
{
    public interface IProductModule
    {
        IEnumerable<Product> GetProducts();
        Task<Guid> Add(Product product);
    }
}