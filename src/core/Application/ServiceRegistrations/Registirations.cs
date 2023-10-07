
using Application.FileStorages;
using Application.PersistentDataStorages;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.ServiceRegistrations
{
    public static class Registirations
    {
        public static void RegistrationServiceApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ISessionService, SessionService>();

            services.AddSingleton<FileStorage>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        }
    }
}
