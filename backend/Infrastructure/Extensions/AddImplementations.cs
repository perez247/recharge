using Application.Interfaces.Iservices;
using Infrastructure.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class AddImplementations
    {
        public static void AddInfractureServices(this IServiceCollection services) 
        {
            services.AddScoped<ISMSService, SMSService>();
            // Add the implementations to be used
        }
    }
}