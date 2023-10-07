

using Application.IdentityServices;
using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Persistence.IdentityServices;
using Persistence.Repositories;
using System.Reflection;

namespace Persistence.ServiceRegistirations
{
    public static class Registirations
    {
        public static void RegistrationServicePersistence(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepositories>();

            services.AddScoped<IProductRepository, ProductRepositories>();

            services.AddScoped<ICartRepository, CartRepository>();


            services.AddScoped<IUserService, UserService>();


        }
    }
}
