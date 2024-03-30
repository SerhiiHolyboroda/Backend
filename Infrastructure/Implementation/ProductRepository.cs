using Abackend.Data;
using Abackend.Repositories.Interface;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Abackend.Repositories.Implementation {
    public class ProductRepository : IProductRepository
    {
        public readonly ApplicationDbContext dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

         public async Task<IReadOnlyList<Product>> GetProductsAsync() {

            return await dbContext.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
        }

        public async Task<Product> GetProductAsync(int id) {
            return await dbContext.Products
                 .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateProductAsync(Product product) {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync() {
           return await dbContext.ProductTypes.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync() {
            return await dbContext.ProductBrands.ToListAsync();
        }
    }
}
