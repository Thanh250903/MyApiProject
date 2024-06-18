using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Controllers;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MyApiProject.Models;
using MyApiProject.Services;
using MyApiProject.Data;

var builder = WebApplication.CreateBuilder(args);
// Load configuration
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSession();
builder.Services.AddTransient<IMongoDBSettings, MongoDBSettings>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Register your controllers
builder.Services.AddScoped<UsersController>();
// Register address controllers
builder.Services.AddScoped<AddressesController>();
// Configure MongoDB settings
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection(nameof(MongoDBSettings)));
// Optional: Configure MongoDB services
builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetConnectionString("MongoDbConnection")));
// Register IMongoDatabase (if needed for specific services)
builder.Services.AddScoped<IMongoDatabase>(s =>
{
    var client = s.GetRequiredService<IMongoClient>();
    var settings = s.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});
// Optional: Register MongoDB context and services

// Register MongoDbContext
builder.Services.AddSingleton<MongoDbContext>();

// Register services
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddDistributedMemoryCache(); // Hoặc AddDistributedRedisCache() nếu bạn sử dụng Redis
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "YourApp.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { Title = "Your API Name", Version = "v1" 
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name V1");
    c.RoutePrefix = "swagger"; // URL root for accessing Swagger UI
});
builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCookiePolicy(); // Enable reading and writing cookies

app.MapControllers();

app.Run();
