using AutoMapper;
using LineTen.TechnicalTask.Domain.Models;
using LineTen.TechnicalTask.Service.Domain.Models;
using LineTen.TechnicalTask.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace LineTen.TechnicalTask.Service.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, IMapper mapper, ILogger<OrderController> logger)
        {
            ArgumentNullException.ThrowIfNull(orderService);
            ArgumentNullException.ThrowIfNull(mapper);
            ArgumentNullException.ThrowIfNull(logger);

            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOrderAsync([FromBody] AddOrderRequest addOrderRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newOrder = _mapper.Map<Order>(addOrderRequest);
                var result = await _orderService.AddOrderAsync(newOrder, cancellationToken).ConfigureAwait(false);
                var resultModel = _mapper.Map<OrderResponse>(result);

                return Ok(resultModel);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid arguments in AddOrderAsync");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AddOrderAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<OrderResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrdersAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _orderService.GetAllOrdersAsync(cancellationToken).ConfigureAwait(false);
                var resultModels = _mapper.Map<List<OrderResponse>>(result);

                return Ok(resultModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GetAllOrdersAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id == default)
                {
                    return BadRequest("Invalid Id in the request path.");
                }

                var result = await _orderService.GetOrderAsync(id, cancellationToken).ConfigureAwait(false);

                if (result is null)
                {
                    return NotFound();
                }

                var resultModel = _mapper.Map<OrderResponse>(result);

                return Ok(resultModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in GetOrderAsync for order ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrderAsync([FromBody] UpdateOrderRequest updateOrderRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedOrder = _mapper.Map<Order>(updateOrderRequest);
                var result = await _orderService.UpdateOrderAsync(updatedOrder, cancellationToken).ConfigureAwait(false);

                if (result is null)
                {
                    return NotFound();
                }

                var resultModel = _mapper.Map<OrderResponse>(result);

                return Ok(resultModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in UpdateOrderAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrderAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _orderService.DeleteOrderAsync(id, cancellationToken).ConfigureAwait(false);
                return result ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in DeleteOrderAsync for order ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}