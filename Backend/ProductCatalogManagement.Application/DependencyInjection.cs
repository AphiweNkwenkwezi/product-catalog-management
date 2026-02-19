using Microsoft.Extensions.DependencyInjection;
using ProductCatalogManagement.Application.Categories;
using ProductCatalogManagement.Application.Products;
using ProductCatalogManagement.Application.Search;
using ProductCatalogManagement.Domain.Entities;
using ProductCatalogManagement.Domain.Interfaces;

namespace ProductCatalogManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<CategoryService>();

            // Register generic search engine
            services.AddSingleton<ProductSearchEngine<ProductEntity>>();

            return services;
        }
    }
}
