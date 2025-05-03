using DotNetEnv;
using notification_service.infrastructure.kafka;
using notification_service.infrastructure.senders.email_sender;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    Env.Load("dev.env");
}
// Add services to the container.

//builder.Services.AddGrpc();

builder.Services.AddEmailSender();

builder.Services.AddKafkaConsumer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapGrpcService<NotifyService>();
app.MapControllers();

app.Run();
