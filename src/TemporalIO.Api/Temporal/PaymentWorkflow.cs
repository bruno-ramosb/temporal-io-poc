using Temporalio.Workflows;

namespace TemporalIO.Api.Temporal
{
    [Workflow]
    public class PaymentWorkflow
    {
        [WorkflowRun]
        public async Task RunAsync(string paymentDetails, string orderId, string email,bool trowEx = false)
        {
            if (!await VerifyPayment(paymentDetails))
            {
                await UpdateOrderStatus(orderId, PaymentStatus.Failed);
                return;
            }

            if (!await ProcessPayment(paymentDetails))
            {
                await UpdateOrderStatus(orderId, PaymentStatus.Failed);
                return;
            }

            await SendReceipt(trowEx);

            await UpdateOrderStatus(orderId, PaymentStatus.Completed);
        }

        private async Task<bool> VerifyPayment(string paymentDetails)
        {
            return await Workflow.ExecuteActivityAsync(
                  (PaymentActivities act) => act.VerifyPaymentAsync(paymentDetails),
                  new() { ScheduleToCloseTimeout = TimeSpan.FromSeconds(10) });
        }

        private async Task UpdateOrderStatus(string orderId, PaymentStatus paymentStatus)
        {
            await Workflow.ExecuteActivityAsync(
                (PaymentActivities act) => act.UpdateOrderStatusAsync(orderId, paymentStatus),
                new() { ScheduleToCloseTimeout = TimeSpan.FromSeconds(10), });
        }

        private async Task SendReceipt(bool throwException)
        {
            await Workflow.ExecuteActivityAsync(
                (PaymentActivities act) => act.SendReceiptAsync(throwException),
                new() { ScheduleToCloseTimeout = TimeSpan.FromSeconds(60) });
        }

        private async Task<bool> ProcessPayment(string paymentDetails)
        {
            return await Workflow.ExecuteActivityAsync(
                (PaymentActivities act) => act.ProcessPaymentAsync(paymentDetails),
                new() { ScheduleToCloseTimeout = TimeSpan.FromSeconds(10) });
        }
    }
}
