using ChallengeN5.Api.Data;
using ChallengeN5.Api.IServices;
using ChallengeN5.Api.Services;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Nest;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Datacontext>(options =>
{    
    options.UseSqlServer(builder.Configuration.GetConnectionString("ChallangeN5")); 
}, ServiceLifetime.Scoped);

// Obtener la configuraci�n de Elasticsearch desde el archivo de configuraci�n
var elasticsearchUri = builder.Configuration["ElasticsearchSettings:Url"];
var defaultIndex = builder.Configuration["ElasticsearchSettings:DefaultIndex"];
// Configurar el cliente de Elasticsearch
builder.Services.AddSingleton<IElasticClient>(serviceProvider =>
{
    var settings = new ConnectionSettings(new Uri(elasticsearchUri))
        .DefaultIndex(defaultIndex); // Nombre del �ndice en Elasticsearch
    return new ElasticClient(settings);
});

object value = builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
     .MinimumLevel.Information()
     .WriteTo.Console()
     .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
     .Enrich.FromLogContext()
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Configurar el productor Kafka
builder.Services.AddSingleton<KafkaProducer>(serviceProvider =>
{
    var bootstrapServers = "localhost:9092"; //direcci�n del servidor Kafka
    var topic = "operaciones"; // nombre del tema
    return new KafkaProducer(bootstrapServers, topic);
});
// Registrar la implementaci�n de IChallengerServices
builder.Services.AddScoped<IChallengerServices, ChallengerServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
