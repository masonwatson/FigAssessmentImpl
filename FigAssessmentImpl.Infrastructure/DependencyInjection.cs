using FigAssessmentImpl.Application.Products;
using FigAssessmentImpl.Application.Users;
using FigAssessmentImpl.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigAssessmentImpl.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var cs = configuration.GetConnectionString("FigAssessmentDb")
                ?? throw new InvalidOperationException("Missing connection string in appsettings: ConnectionStrings:FigAssessmentDb");

            // Declare lifetimes for Product support classes
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository>(_ => new SqlProductRepository(cs));

            // Declare lifetimes for User support classes
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository>(_ => new SqlUserRepository(cs));

            return services;
        }
    }
}
