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
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>?> GetProductsAsync(GetProductQueryOptions options, CancellationToken ct);
        Task InsertProductAsync(CreateProductRequest request, CancellationToken ct);
    }
}
