using Microsoft.AspNetCore.Mvc;
using Temporalio.Client;
using TemporalIO.Api.Temporal;

namespace TemporalIO.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class PaymentController : ControllerBase
    {
        private readonly ITemporalClient _client;
        public PaymentController(ITemporalClient client)
        {
            _client = client;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreatePaymentRequest paymentRequest)
        {
            var id = $"payment-queue-{Guid.NewGuid()}";
            await _client.ExecuteWorkflowAsync(
                (PaymentWorkflow wf) => wf.RunAsync(paymentRequest.PaymentDetails, paymentRequest.OrderId, paymentRequest.Email,paymentRequest.ThrowException),
                new(id: id, taskQueue: "payment-queue"));

            return Ok();
        }
    }
}
