using FinalApi.Services.Repository;
using FinalApi.Dto;
using Microsoft.AspNetCore.Mvc;
using FinalApi.Models;
using Quartz.Util;
using Humanizer;
using FinalApi.FilterHeader;

namespace FinalApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [ApiVersion("1.0")]
    [ServiceFilter(typeof(SecretKeyFilter))]

    public class DemoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DemoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetOrders")]
        public ActionResult GetAllOrders()
        {
            try
            {
                var Order = _unitOfWork.GetOrderRequest.GetOrders();
                return Ok(Order);
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

                var FindId = _unitOfWork.GetOrderRequest.GetOrderById(numericId);

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
                var getName = _unitOfWork.OrderDto.GetOrderByItem(keyword);

                if (getName == null && keyword == null)
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
            var CustomerVip = _unitOfWork.CustomerRequest.CreateCustomerVipAsync();

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

                var createdOrderId = _unitOfWork.CreateOrderRequest.CreateOrder(request);

               
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
                _unitOfWork.OrderDeleteRequest.RemoveItemFromOrder(idOrder, idItem);
              
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
