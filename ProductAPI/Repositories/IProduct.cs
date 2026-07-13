using ProductAPI.Models.DTOs;
using ProductAPI.Models.Entities;

namespace ProductAPI.Repositories
{
    public interface IProduct
    {
        Task<Product> CreateProduct(CreateProductRequest request);
    }
}
