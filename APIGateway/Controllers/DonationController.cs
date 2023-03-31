using APIGateway.CQRS.Commands;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Paypal;
using TDonation;


namespace APIGateway.Controllers;

[ApiController]
// [Authorize]
[Route("[controller]")]
public class DonationController : ControllerBase
{
    private readonly Payment.PaymentClient _paymentClient;
    private readonly ILogger<WeatherController> _logger;
    private readonly IMediator _mediator;
    private readonly IBus _bus;



    public DonationController(Payment.PaymentClient paymentClient, ILogger<WeatherController> logger, IMediator mediator, IBus bus)
    {
        _paymentClient = paymentClient;
        _logger = logger;
        _mediator = mediator;
        _bus = bus;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction(CreateDonationTransactionCommand request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    [Route("zalo-callBack")]
    public async Task<IActionResult> ZaloPayCallBack(ZaloCallbackCommand request)
    {
        var processResult =await _mediator.Send(request);
        return Ok(processResult);
    }
    
    [HttpPost]
    [Route("paypal-capture")]
    public async Task<IActionResult> PaypalCapture([FromBody] PaypalPaymentCaptureMessage request)
    {
        var endpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/paypal-capture"));
        var headers = new Dictionary<string, object>();
        headers["Authorization"] = "Bearer myAccessToken";
        await endpoint.Send(request, context =>
        {
            foreach (var keyValuePair in headers)
            {
                context.Headers.Set(keyValuePair.Key, keyValuePair.Value);
            }
        });
        return Ok();
    }
    
}