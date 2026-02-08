using FigAssessmentImpl.Application.Products;
using FigAssessmentImpl.Application.Products.CreateProducts;
using FigAssessmentImpl.Application.Products.GetProducts;
using FigAssessmentImpl.Domain.Products;
using Microsoft.AspNetCore.Mvc;

namespace FigAssessmentImpl.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/products
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetProductResponse>>> GetProductsAsync([FromQuery] GetProductQueryOptions options, CancellationToken ct)
        {
            // Validate required fields, min/max lengths, and ranges
            if (options.Page < 0) 
                return BadRequest("Page number must be greater than 0");

            if (options.PageSize < 0)
                return BadRequest("Page size must be greater than 0");

            try
            {
                var result = await _productService.GetProductsAsync(options, ct);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                // Log with logger

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log with logger

                return StatusCode(500);
            }
        }

        // POST api/products
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequest request, CancellationToken ct)
        {
            // Validate required fields, min/max lengths, and ranges
            if (string.IsNullOrEmpty(request.Name)) 
                return BadRequest("Product name is required");

            if (request.Name.Length <= 3) 
                return BadRequest("Product name must be at least 3 characters");

            if (request.Name.Length >= 200) 
                return BadRequest("Product name must be at most 200 characters");

            if (!string.IsNullOrEmpty(request.Description) && request.Description.Length > 200) 
                return BadRequest("Product description must be at most 200 characters");

            if (!request.Price.HasValue)
                return BadRequest("Product price is required");

            if (request.Price.Value <= 0) 
                return BadRequest("Product price must be greater than or equal to 0");

            if (string.IsNullOrEmpty(request.Category)) 
                return BadRequest("Product category is required");
            
            if (request.Category.Length >= 500)
                return BadRequest("Product category must be at most 500 characters");

            try
            {
                await _productService.CreateProductAsync(request, ct);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                // log with logger

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // log with logger

                return StatusCode(500);
            }
        }
    }
}
