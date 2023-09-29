using FinalApi.Dto;
using Microsoft.AspNetCore.Mvc;
using FinalApi.Models;
using FinalApi.Services;
using FinalApi.FilterHeader;
using FinalApi.Response;

namespace FinalApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
     [ApiVersion("1.2")]
    [ServiceFilter(typeof(SecretKeyFilter))]

    public class OrdersController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        private readonly ICustomerService _customerService;
        private readonly projectDemoContext _context;
        public OrdersController(IOrderServices orderServices, ICustomerService customerService, projectDemoContext context)
        {
            _orderServices = orderServices;
            _customerService = customerService;
            _context = context;
        }
        [HttpGet("GetOrders/{page}")]
        public IActionResult GetAllOrders(string page)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(page))
                {
                    return BadRequest("Page is empty or whitespace.");
                }

                if (!int.TryParse(page, out int pageNumber) || pageNumber <= 0)
                {
                    return BadRequest("Page is not a valid positive integer and more than 0.");
                }

                var pageResults = 3;
                var totalOrders = _context.Orders.Count();
                var pageCount = (int)Math.Ceiling((double)totalOrders/pageResults);

                if (pageNumber > pageCount)
                {
                    return BadRequest("Page is out of range.");
                }

                var orderRequests = _orderServices.GetOrders()
                    .Skip((pageNumber - 1) * pageResults)
                    .Take(pageResults)
                    .ToList();

                var response = new OrderResponse
                {
                    Orders = orderRequests,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("GetOrdersss/{id}")]
        public IActionResult GetOrderById(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest("Id is empty or whitespace.");
                }

                if (id.All(char.IsLetter))
                {
                    return BadRequest("Id contains only letters and is invalid.");
                }

                if (!int.TryParse(id, out int numericId) && numericId < 0)
                {
                    return BadRequest("Id is not a valid integer.");
                }

                var FindId = _orderServices.GetOrderById(numericId);

                if (FindId == null)
                {
                    return NotFound("Not Found.");
                }

                return Ok(FindId);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request: " + ex.Message);
            }
        }
        [HttpGet("GetOrderBy/{keyword}")]
        public IActionResult GetOrderByItemName(string keyword)
        {
            try
            {
                var getName = _orderServices.GetOrderByItem(keyword);

                if (getName == null)
                {
                    return NotFound("Not Found.");
                }

                return Ok(getName);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request: " + ex.Message);
            }
        }
        [HttpPut("CreateCustomer-vip")]
        public IActionResult CreateCustomerVip()
        {
            var CustomerVip = _customerService.CreateCustomerVipAsync();

            return Ok(CustomerVip);
        }
        [HttpPost("CreateOrders")]
        public IActionResult CreateOrders([FromBody] CreateOrderRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Invalid input: Request body is empty.");
                }
                var createdOrderId = _orderServices.CreateOrders(request);

               
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpDelete("Delete/{idOrder},{idItem}")]
        public IActionResult DeleteItemInOrder(int idOrder, int idItem)
        {
             try
            {
                _orderServices.RemoveItemFromOrder(idOrder, idItem);
              
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); 
            }
            return Ok();
        }

    }


}
