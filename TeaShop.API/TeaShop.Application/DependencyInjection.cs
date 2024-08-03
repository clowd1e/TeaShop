using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TeaShop.Application.Comparison;
using TeaShop.Application.Mapper;
using TeaShop.Domain.Entities;

namespace TeaShop.Application
{
    /// <summary>
    /// Dependency injection for application layer
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("en");

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            #region Set comparison delegates
            Tea.ComparisonDelegate = ComparisonExtensions.TeaDefaultComparison;
            TeaType.ComparisonDelegate = ComparisonExtensions.TeaTypeDefaultComparison;
            Customer.ComparisonDelegate = ComparisonExtensions.CustomerDefaultComparison;
            Order.ComparisonDelegate = ComparisonExtensions.OrderDefaultComparison;
            #endregion

            return services;
        }
    }
}
