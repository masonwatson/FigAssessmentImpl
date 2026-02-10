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
            try
            {
                var result = await _productService.GetProductsAsync(options, ct);
                return Ok(result);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                return StatusCode(499, "Client canceled request");
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
            try
            {
                await _productService.CreateProductAsync(request, ct);
                return Ok();
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                return StatusCode(499, "Client canceled request");
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
