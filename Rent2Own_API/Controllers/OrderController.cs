using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Rent2Own_Business.Interfaces;
using Rent2Own_Models;
using Stripe.Checkout;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Rent2Own_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderOperations _orderOperations;
        private readonly IEmailSender _emailSender;
        public OrderController(IOrderOperations orderOperations, IEmailSender emailSender) 
        {
            _orderOperations = orderOperations;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderOperations.GetAll());
        }

        [HttpGet("{orderHeaderId}")]
        public async Task<IActionResult> Get(int? orderHeaderId)
        {
            if (orderHeaderId == null || orderHeaderId == 0)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var orderHeader = await _orderOperations.Get(orderHeaderId.Value);
            if (orderHeader == null)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(orderHeader);
        }

        [HttpPost]
		[ActionName("Create")]
        public async Task<IActionResult> Create([FromBody] StripePayment payment)
        {
            payment._Order.OrderHeader.OrderDate = DateTime.Now;
            var result = await _orderOperations.Create(payment._Order);
            return Ok(result);
        }
         
        [HttpPost]
        [ActionName("paymentsuccessful")]
        public async Task<IActionResult> PaymentSuccessful([FromBody] OrderHeader orderHeader)
        {
            var service = new SessionService();
            var sessionDetails = service.Get(orderHeader.SessionId);
            if (sessionDetails.PaymentStatus == "paid")
            {
                orderHeader.PaymentIntentId = sessionDetails.PaymentIntentId;
                var result = await _orderOperations.MarkPaymentSuccessful(orderHeader.Id, orderHeader.PaymentIntentId);
                await _emailSender.SendEmailAsync(orderHeader.Email, "R2O Order Confirmation",
                    "New Order has been Created :" + orderHeader.Id);
                if (result == null)
                {
                    return BadRequest(new ErrorModel()
                    {
                        ErrorMessage = "Can not mark payment as successful"
                    });
                }
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
