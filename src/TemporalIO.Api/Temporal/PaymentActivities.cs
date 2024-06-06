using Temporalio.Activities;
namespace TemporalIO.Api.Temporal
{
    public class PaymentActivities
    {
        [Activity]
        public async Task<bool> VerifyPaymentAsync(string paymentId)
        {
            await Console.Out.WriteLineAsync("Verificando pagamento.");
            return true;
        }

        [Activity]
        public async Task<bool> ProcessPaymentAsync(string paymentId)
        {
            await Console.Out.WriteLineAsync("Processando pagamento.");
            return true;
        }

        [Activity]
        public async Task<bool> SendReceiptAsync(bool throwException)
        {
            if (throwException)
                throw new Exception("Forçando erro para retornar execução na atividade especifica");

            await Console.Out.WriteLineAsync("Enviando comprovante por email.");
            return true;
        }

        [Activity]
        public async Task<bool> UpdateOrderStatusAsync(string orderId, PaymentStatus status)
        {
            await Console.Out.WriteLineAsync("Atualizado status do pedido.");
            return true;
        }
    }
}
