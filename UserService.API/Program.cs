using Autofac;
using Autofac.Extensions.DependencyInjection;
using UserService.API.Infrastructure.AutofacModules;
using UserService.API.Utility;
using System.Text.Json.Serialization;
using UserService.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => 
{
    options.CustomSchemaIds(type => type.ToString());
});

//configure autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register services directly with Autofac here. 
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new MediatorModule()));

//DB context configuration
builder.Services.AddDataServices(builder.Configuration);

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddCors(option => 
{
    option.AddPolicy("CorsPolicy",
        builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
        );

});

//register resources languages
builder.Services.AddLocalizationServices();

var app = builder.Build();

//exception middleware
app.UseMiddleware<JsonExceptionMiddleware>();

//client language culture( set default or selected language by client )
app.UseMiddleware<CultureProviderMiddleware>();

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//create database if doesn't exist when application start
#pragma warning disable CS8602 // Dereference of a possibly null reference.
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<UserContext>();
    context.Database.EnsureCreated();
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.


app.Run();
