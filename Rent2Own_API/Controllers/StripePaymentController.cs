using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rent2Own_Models;
using Stripe.Checkout;

namespace Rent2Own_API.Controllers
{
    [Route("api/[controller]/[action]")]
	[ApiController]
	public class StripePaymentController : Controller
    {

        private readonly IConfiguration _configuration;

        public StripePaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Authorize]
        [ActionName("Create")]
        public async Task<IActionResult> Create([FromBody] StripePayment payment)
        {
            try
            {

                var domain = _configuration.GetValue<string>("Rent2Own_Client_URL");

                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain+payment.SuccessUrl,
                    CancelUrl = domain+payment.CancelUrl,
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                 
 
                foreach (var item in payment._Order.OrderDetails)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price*100), //20.00 -> 2000
                            Currency="usd",
                            ProductData= new SessionLineItemPriceDataProductDataOptions
                            {
                                Name= item.Product.Name
                            }
                        },
                        Quantity= item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                var service = new SessionService();
                Session session = service.Create(options);
                return Ok(new SuccessModel()
                {
                    Data = session.Id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
