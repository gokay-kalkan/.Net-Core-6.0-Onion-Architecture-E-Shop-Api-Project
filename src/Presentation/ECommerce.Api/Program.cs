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
    // �erez kabul�n� yap�land�r�n
    options.CheckConsentNeeded = context => false; // Kullan�c� onay� gerekiyorsa true yap�n
    options.MinimumSameSitePolicy = SameSiteMode.None; // Sadece ayn� site i�in minimum k�s�tlamalar� kald�r�n

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
    options.IdleTimeout = TimeSpan.FromDays(30); // Oturumun s�resini istedi�iniz gibi ayarlayabilirsiniz
    options.Cookie.IsEssential = true; // �erezlerin etkinle�tirilmi� olmas� gerekir
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
        builder.WithOrigins("http://localhost:5281", "https://localhost:5281","http://localhost:5000") // SwaggerUI'nin �al��t��� URL
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddSwaggerGen();



var app = builder.Build();

app.UseSession();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin(); // T�m kaynaklara izin ver
    builder.AllowAnyMethod(); // T�m HTTP metodlar�na izin ver
    builder.AllowAnyHeader(); // T�m HTTP ba�l�klar�na izin ver
});
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1"));
}
app.UseHttpsRedirection(); // HTTPS'e y�nlendirme etkinle�tirme
app.UseHsts();

app.UseCookiePolicy();

app.UseAuthorization();

app.MapControllers();

app.Run();
