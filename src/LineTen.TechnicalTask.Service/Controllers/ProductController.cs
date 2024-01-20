using AutoMapper;
using LineTen.TechnicalTask.Domain.Models;
using LineTen.TechnicalTask.Service.Domain.Models;
using LineTen.TechnicalTask.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace LineTen.TechnicalTask.Service.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, IMapper mapper, ILogger<ProductController> logger)
        {
            ArgumentNullException.ThrowIfNull(productService);
            ArgumentNullException.ThrowIfNull(mapper);
            ArgumentNullException.ThrowIfNull(logger);

            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProductAsync([FromBody] AddProductRequest addProductRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newProduct = _mapper.Map<Product>(addProductRequest);
                var result = await _productService.AddProductAsync(newProduct, cancellationToken).ConfigureAwait(false);
                var resultModel = _mapper.Map<ProductResponse>(result);

                return Ok(resultModel);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid arguments in AddProductAsync");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AddProductAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ProductResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProductsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _productService.GetAllProductsAsync(cancellationToken).ConfigureAwait(false);
                var resultModels = _mapper.Map<List<ProductResponse>>(result);

                return Ok(resultModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GetAllProductsAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id == default)
                {
                    return BadRequest("Invalid Id in the request path.");
                }

                var result = await _productService.GetProductAsync(id, cancellationToken).ConfigureAwait(false);

                if (result is null)
                {
                    return NotFound();
                }

                var resultModel = _mapper.Map<ProductResponse>(result);

                return Ok(resultModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in GetProductAsync for product ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductRequest updateProductRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedProduct = _mapper.Map<Product>(updateProductRequest);
                var result = await _productService.UpdateProductAsync(updatedProduct, cancellationToken).ConfigureAwait(false);

                if (result is null)
                {
                    return NotFound();
                }

                var resultModel = _mapper.Map<ProductResponse>(result);

                return Ok(resultModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in UpdateProductAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProductAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(id, cancellationToken).ConfigureAwait(false);
                return result ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in DeleteProductAsync for product ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}