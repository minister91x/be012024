using DataAccess.Eshop.EntitiesFrameWork;
using DataAccess.Eshop.IServices;
using DataAccess.Eshop.Services;
using DataAccess.Eshop.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnStr")));

builder.Services.AddDbContext<EshopDBContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("ConnStr"), b => b.MigrationsAssembly("Eshop.API")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<EshopDBContext>()
    .AddDefaultTokenProviders();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
    };
});


builder.Services.AddTransient<IProductRepository, ProductRepository>(); // nó sẽ quản lý việc khởi tạo class ProductServices
builder.Services.AddTransient<IUseRepository, UseRepository>();
builder.Services.AddTransient<IEShopUnitOfWork, EShopUnitOfWork>();
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
