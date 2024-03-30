
 
using Abackend.Repositories.Interface;
using AutoMapper;
using Azure;
using Backend.Controllers;
using Backend.Dtos;
using Backend.Errors;
using Backend.Helpers;
using Core.Interface;
using Core.Models;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Abackend.Controllers {
  
    public class ProductsController : BaseApiController {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        // private readonly IProductRepository _productRepository;


        public ProductsController(IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper
            ) {
      
          //  _productRepository = productRepository;
           _productRepo = productRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }
  
        [HttpGet]
        //   public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
    [FromQuery] ProductSpecParams productParams
  
            ) {
          
         var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductWithFiltersForCountSpecificication(productParams);

            var totalItems = await _productRepo.CountAsync(countSpec);
            //  var products = await _productRepo.ListAllAsync();
            var products = await _productRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,
                productParams.PageSize, totalItems, data ));
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id) {
            // return await _productRepo.GetByIdAsync(id);
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productRepo.GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product request) {
            // Map DTO to Domain Model
            var product = new Product
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                PictureUrl = request.PictureUrl,
                ProductType = new ProductType
                {
                    Id = request.ProductType.Id,
                    Name = request.ProductType.Name
                },
                ProductBrand = new ProductBrand
                {
                    Id = request.ProductBrand.Id,
                    Name = request.ProductBrand.Name
                }
            };

        //@!!!!    await _productRepository.CreateProductAsync(product);
            // map domain model to dto
            //     var response = new Product
            //      {
            //         Id = product.Id,
            //         Name = product.Name,
            //         UrlHandle = category.UrlHandle
            //     };
            //     return Ok(response);
            return Ok();
        }



        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() {

            return Ok(await _productBrandRepo.ListAllAsync());
            
      }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes() {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }

}


