


using Unity;
using Acv2.SharedKernel.Application;
using Infraescructure;
using Application;
using Distribution;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.ConfigureContainer<UnityContainer>(container => {

//    container.AddContainerInfraestructureCustomer();

//});

//ConfigureUnityContainer.Configure();

builder.Services.AddServiceInfraestructureCustomer();
builder.Services.AddServiceApplicationCustomer();

// Add services to the container.

builder.Services.AddControllers();
//builder.UseUnityServiceProvider();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//IHostBuilder hostBuilder = builder.Host.UseServiceProviderFactory(new UnityServiceProvider());

// Call ConfigureContainer on the Host sub property 
//builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
//{
//    builder.RegisterModule(new MyApplicationModule());
//});



var app = builder.Build();

//app.UseUnityServiceProvider(container => { 
//    container.ConfigureServices();
//});

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
