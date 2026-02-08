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
            await _productRepository.InsertProductAsync(request, ct);
        }
    }
}
