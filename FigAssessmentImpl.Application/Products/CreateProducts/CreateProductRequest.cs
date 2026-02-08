using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigAssessmentImpl.Application.Products.CreateProducts
{
    public class CreateProductRequest
    {
        public string Name { get; init; } = "";
        public string? Description { get; init; }
        public decimal? Price { get; init; }
        public string Category { get; init; } = "";
        public bool InStock { get; set; } = false;
    }
}
