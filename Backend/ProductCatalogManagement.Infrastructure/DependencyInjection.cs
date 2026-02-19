using Microsoft.Extensions.DependencyInjection;
using ProductCatalogManagement.Domain.Interfaces;
using ProductCatalogManagement.Infrastructure.Repositories;

namespace ProductCatalogManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            //services.AddDbContext<AppDbContext>(options =>
            //    options.UseInMemoryDatabase("ProductCatalogDb"));

            //services.AddSingleton<IProductRepository, InMemProductRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
