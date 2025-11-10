using Microsoft.EntityFrameworkCore;
using SkyNetAPI.Application;
using SkyNetAPI.Infrastructure.EntityFramework;
using SkyNetAPI.Infrastructure.Repository;
using KafkaConsumerService = SkyNetAPI.Infrastructure.KafkaService.KafkaConsumerService;


var bootstrapServer = "localhost:9092";
var topic = "batch-waybills";

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=SkyNet_db;Username=postgres;Password=Mdumiseni@96;"));
builder.Services.AddTransient<IWaybillService, WaybillService>();
builder.Services.AddScoped<IWaybillRepository, WaybillRepository>();

builder.Services.AddTransient<KafkaConsumerService>(_ =>
    new KafkaConsumerService(bootstrapServer, topic, "waybill-group", 
        _.GetRequiredService<IWaybillRepository>()));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.MapGet("/waybills/{id:Guid}", async (Guid id, IWaybillService waybillService) =>
{
    var waybill = await waybillService.GetWaybillByIdAsync(id);
    return waybill is not null ? Results.Ok(waybill) : Results.NotFound();
});

app.MapGet("/waybills/{waybillNumber}", async (string waybillNumber, IWaybillService waybillService) =>
{
    var waybill = await waybillService.GetWaybillByWaybillNumber(waybillNumber);
    return waybill is not null ? Results.Ok(waybill) : Results.NotFound();
});



// Resolve and run Kafka consumer
var kafka = app.Services.GetRequiredService<KafkaConsumerService>();
_ = Task.Run(() => kafka.StartConsumingAsync(CancellationToken.None));
app.UseCors("AllowAngularDev");
// Run web API
await app.RunAsync();
