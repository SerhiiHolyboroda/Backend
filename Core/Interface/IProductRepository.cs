



using Core.Models;

namespace Abackend.Repositories.Interface {
    public interface IProductRepository {

        Task<Product> CreateProductAsync(Product product);
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(int id);

        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
   
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
    }
}
