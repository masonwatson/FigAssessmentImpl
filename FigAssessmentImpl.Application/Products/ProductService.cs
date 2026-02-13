using FigAssessmentImpl.Application.Products.CreateProducts;
using FigAssessmentImpl.Application.Products.GetProducts;
using FigAssessmentImpl.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigAssessmentImpl.Application.Products
{
    public sealed class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(
            IProductRepository productRepository
        ) 
        {
            _productRepository = productRepository;
        }

        public async Task<IReadOnlyList<GetProductResponse>?> GetProductsAsync(GetProductQueryOptions options, CancellationToken ct)
        {
            // Validate required fields, min/max lengths, and ranges
            if (options.Page <= 0)
                throw new ArgumentException("Page number must be greater than 0");

            if (options.PageSize <= 0)
                throw new ArgumentException("Page size must be greater than 0");

            var products = await _productRepository.GetProductsAsync(options, ct);

            return products?.Select(p => new GetProductResponse 
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Category = p.Category,
                InStock = p.InStock,
                CreatedDate = p.CreatedDate,
            }).ToList();
        }

        public async Task CreateProductAsync(CreateProductRequest request, CancellationToken ct)
        {
            // Validate required fields, min/max lengths, and ranges
            if (string.IsNullOrEmpty(request.Name))
                throw new ArgumentException("Product name is required");

            if (request.Name.Length < 3)
                throw new ArgumentException("Product name must be at least 3 characters");

            if (request.Name.Length > 200)
                throw new ArgumentException("Product name must be at most 200 characters");

            if (!string.IsNullOrEmpty(request.Description) && request.Description.Length > 200)
                throw new ArgumentException("Product description must be at most 200 characters");

            if (!request.Price.HasValue)
                throw new ArgumentException("Product price is required");

            if (request.Price.Value < 0)
                throw new ArgumentException("Product price must be greater than or equal to 0");

            if (string.IsNullOrEmpty(request.Category))
                throw new ArgumentException("Product category is required");

            if (request.Category.Length > 500)
                throw new ArgumentException("Product category must be at most 500 characters");

            await _productRepository.InsertProductAsync(request, ct);
        }
    }
}
