using Microsoft.EntityFrameworkCore;
using OnboardingTest.Domain.Interfaces;
using OnboardingTest.Infrastructure.Services;
using OnboardingTest.Repository.Context;
using Newtonsoft.Json.Serialization;
using FluentValidation.AspNetCore;
using OnboardingTest.Entity.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CreditoAutomotrizContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));

// Add services to the container.
builder.Services.AddTransient<IClientePatioService, ClientePatioServices>();
builder.Services.AddTransient<IClienteService, ClienteServices>();
builder.Services.AddTransient<IMarcaService, MarcaServices>();
builder.Services.AddTransient<IPatioService, PatioServices>();
builder.Services.AddTransient<ISolicitudCreditoService, SolicitudCreditoServices>();
builder.Services.AddTransient<ITrackingSolicitudService, TrackingSolicitudServices>();
builder.Services.AddTransient<IVehiculoService, VehiculoServices>();

// Add Validation Fluent
builder.Services.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<ClientePatio>());
builder.Services.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Cliente>());
builder.Services.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Patio>());
builder.Services.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<SolicitudCredito>());
builder.Services.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Vehiculo>());

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
}).AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddMvc().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Inicializar clientes
new ClienteServices(new CreditoAutomotrizContext()).InsertMultipleCliente().GetAwaiter().GetResult();
new MarcaServices(new CreditoAutomotrizContext()).InsertMultipleMarca().GetAwaiter().GetResult();
new EjecutivoServices(new CreditoAutomotrizContext()).InsertMultipleEjecutivo().GetAwaiter().GetResult();

app.Run();
