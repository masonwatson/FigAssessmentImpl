using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigAssessmentImpl.Application.Products.GetProducts
{
    public sealed class GetProductQueryOptions
    {
        public int? Id { get; init; }
        public string? Name { get; init; }
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }
        public string? Category { get; init; }
        public bool? InStock { get; init; }
        public DateTime? CreatedDate { get; init; }
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string? SearchTerm { get; init; }
    }
}
