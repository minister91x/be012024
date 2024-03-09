using DataAccess.Eshop.EntitiesFrameWork;
using DataAccess.Eshop.IServices;
using DataAccess.Eshop.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EshopDBContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("ConnStr")));

builder.Services.AddTransient<IProductServices, ProductServices>(); // nó sẽ quản lý việc khởi tạo class ProductServices

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.Run(async context =>
//{

//    await context.Response.WriteAsync("Hello world!");
//});

//app.UseMiddleware<Eshop.API.CustomMiddleWare>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
