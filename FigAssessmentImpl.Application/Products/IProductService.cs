using FigAssessmentImpl.Application.Products.CreateProducts;
using FigAssessmentImpl.Application.Products.GetProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigAssessmentImpl.Application.Products
{
    public interface IProductService
    {
        Task<IReadOnlyList<GetProductResponse>?> GetProductsAsync(GetProductQueryOptions options, CancellationToken ct);
        Task CreateProductAsync(CreateProductRequest request, CancellationToken ct);
    }
}
