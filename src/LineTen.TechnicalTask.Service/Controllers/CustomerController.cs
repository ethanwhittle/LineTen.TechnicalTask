using AutoMapper;
using LineTen.TechnicalTask.Domain.Models;
using LineTen.TechnicalTask.Service.Domain.Models;
using LineTen.TechnicalTask.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace LineTen.TechnicalTask.Service.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, IMapper mapper, ILogger<CustomerController> logger)
        {
            ArgumentNullException.ThrowIfNull(customerService);
            ArgumentNullException.ThrowIfNull(mapper);
            ArgumentNullException.ThrowIfNull(logger);

            _customerService = customerService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCustomerAsync([FromBody] AddCustomerRequest addCustomerRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newCustomer = _mapper.Map<Customer>(addCustomerRequest);
                var result = await _customerService.AddCustomerAsync(newCustomer, cancellationToken).ConfigureAwait(false);
                var resultModel = _mapper.Map<CustomerResponse>(result);

                return Ok(resultModel);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid arguments in AddCustomerAsync");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AddCustomerAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<CustomerResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCustomersAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _customerService.GetAllCustomersAsync(cancellationToken).ConfigureAwait(false);
                var resultModels = _mapper.Map<List<CustomerResponse>>(result);

                return Ok(resultModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GetAllCustomersAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id == default)
                {
                    return BadRequest("Invalid Id in the request path.");
                }

                var result = await _customerService.GetCustomerAsync(id, cancellationToken).ConfigureAwait(false);

                if (result is null)
                {
                    return NotFound();
                }

                var resultModel = _mapper.Map<CustomerResponse>(result);

                return Ok(resultModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in GetCustomerAsync for customer ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] UpdateCustomerRequest updateCustomerRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedCustomer = _mapper.Map<Customer>(updateCustomerRequest);
                var result = await _customerService.UpdateCustomerAsync(updatedCustomer, cancellationToken).ConfigureAwait(false);

                if (result is null)
                {
                    return NotFound();
                }

                var resultModel = _mapper.Map<CustomerResponse>(result);

                return Ok(resultModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in UpdateCustomerAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomerAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _customerService.DeleteCustomerAsync(id, cancellationToken).ConfigureAwait(false);
                return result ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in DeleteCustomerAsync for customer ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}