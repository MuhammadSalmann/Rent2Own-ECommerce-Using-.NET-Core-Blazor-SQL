using Microsoft.AspNetCore.Mvc;
using Rent2Own_Business;
using Rent2Own_Models;

namespace Rent2Own_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(ProductOperations.GetAll());
        }

        [HttpGet("{productId}")]
        public IActionResult Get(int? productId)
        {
            if(productId == null || productId == 0)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid ID",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var product = ProductOperations.Get(productId.Value);
            if(product == null)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid ID",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(product);
        }
    }
}
