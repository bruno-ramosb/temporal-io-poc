using Temporalio.Extensions.Hosting;
using TemporalIO.Api.Temporal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTemporalClient(clientOptions =>
{
    clientOptions.TargetHost = "temporal:7233";
    clientOptions.Namespace = "default";
});

builder.Services.AddHostedTemporalWorker(
    clientTargetHost: "temporal:7233",
    clientNamespace: "default",
    taskQueue: "payment-queue")
    .AddScopedActivities<PaymentActivities>()
    .AddWorkflow<PaymentWorkflow>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

