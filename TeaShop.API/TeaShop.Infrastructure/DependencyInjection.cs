using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeaShop.Application.Data;
using TeaShop.Domain.Repository;
using TeaShop.Infrastructure.Database;
using TeaShop.Infrastructure.Repository;

namespace TeaShop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TeaShopDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory")));

            services.AddScoped<ITeaShopDbContext, TeaShopDbContext>();
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TeaShopDbContext>());

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ITeaRepository, TeaRepository>();
            services.AddScoped<ITeaTypeRepository, TeaTypeRepository>();

            return services;
        }
    }
}
