using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketService.Dtos;
using Store.Service.Services.OrderService.Dtos;
using Store.Service.Services.PaymentSevice;
using Stripe;

namespace Store.API.Controllers
{
  
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private const string endpointSecret = "whsec_0792b5ac623dc01888edad07da8ce8283c39a80337620e2a7dcc5770aef2faf0";

        public PaymentController(
            IPaymentService paymentService,
            ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntentForExistingOrder(CustomerBasketDto input)
        =>Ok(await _paymentService.CreateOrUpdatePaymentIntentForExistingOrder(input));

        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntentForNewOrder(string basketId)
        => Ok(await _paymentService.CreateOrUpdatePaymentIntentForNewOrder(basketId));

            [HttpPost("webhook")]
            public async Task<IActionResult> Index()
            {
                var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                try
                {
                    var stripeEvent = EventUtility.ConstructEvent(json,
                        Request.Headers["Stripe-Signature"], endpointSecret);
                PaymentIntent paymentIntent;
                OrderResultDto order;
                    // Handle the event
                    if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                    {
                        paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                        _logger.LogInformation("Payment Failed: ", paymentIntent.Id);
                        order = await _paymentService.UpdateOrderPaymentFailed(paymentIntent.Id);
                         _logger.LogInformation("Order Updated To Payment Failed: ", order.Id);
                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                    {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeed: ", paymentIntent.Id);
                    order = await _paymentService.UpdateOrderPaymentSucceeded(paymentIntent.Id);
                    _logger.LogInformation("Order Updated To Payment Succeeded: ", order.Id);
                }
                // ... handle other event types
                else
                    {
                        Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    }

                    return Ok();
                }
                catch (StripeException e)
                {
                    return BadRequest();
                }
            
        }
    }
}
