namespace TemporalIO.Api.Controllers
{
    public class CreatePaymentRequest
    {
        public string PaymentDetails { get; set; }
        public string OrderId { get; set; }
        public string Email { get; set; }
        public bool ThrowException { get; set; }
    }

}
