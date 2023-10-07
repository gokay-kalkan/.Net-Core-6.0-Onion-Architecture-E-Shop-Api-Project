using Application.CustomizeIdentityErrorDescriber;
using Application.Middlewares;
using Application.ServiceRegistrations;
using Domain.Entities;

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Database;
using Persistence.ServiceRegistirations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // Çerez kabulünü yapýlandýrýn
    options.CheckConsentNeeded = context => false; // Kullanýcý onayý gerekiyorsa true yapýn
    options.MinimumSameSitePolicy = SameSiteMode.None; // Sadece ayný site için minimum kýsýtlamalarý kaldýrýn

    options.ConsentCookie.Expiration = TimeSpan.FromHours(1);
});
builder.Services.AddDistributedMemoryCache();

builder.Services.AddMemoryCache();

builder.Services.Configure<MemoryCacheEntryOptions>(options =>
{
    options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
});
builder.Services.AddSession(options =>
{
    options.IOTimeout = TimeSpan.FromDays(30);
    options.IdleTimeout = TimeSpan.FromDays(30); // Oturumun süresini istediðiniz gibi ayarlayabilirsiniz
    options.Cookie.IsEssential = true; // Çerezlerin etkinleþtirilmiþ olmasý gerekir
});

builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddDbContext<DataContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies());
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


builder.Services.AddControllers();


builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = int.MaxValue; //2gb
});


builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 10;
   

}).AddEntityFrameworkStores<DataContext>()
  .AddDefaultTokenProviders().AddErrorDescriber<CustomIdentityErrorDescriber>();

builder.Services.RegistrationServiceApplication();

builder.Services.RegistrationServicePersistence();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5281", "https://localhost:5281","http://localhost:5000") // SwaggerUI'nin çalýþtýðý URL
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddSwaggerGen();



var app = builder.Build();

app.UseSession();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin(); // Tüm kaynaklara izin ver
    builder.AllowAnyMethod(); // Tüm HTTP metodlarýna izin ver
    builder.AllowAnyHeader(); // Tüm HTTP baþlýklarýna izin ver
});
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1"));
}
app.UseHttpsRedirection(); // HTTPS'e yönlendirme etkinleþtirme
app.UseHsts();

app.UseCookiePolicy();

app.UseAuthorization();

app.MapControllers();

app.Run();
