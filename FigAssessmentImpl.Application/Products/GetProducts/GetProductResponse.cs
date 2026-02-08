using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigAssessmentImpl.Application.Products.GetProducts
{
    public sealed class GetProductResponse
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public string? Description { get; init; }
        public required decimal Price { get; init; }
        public string? Category { get; init; }
        public bool? InStock { get; init; }
        public required DateTime CreatedDate { get; init; }
    }
}
