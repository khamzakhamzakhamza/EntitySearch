using EntitySearch.Core.QueryBuilders;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySearch.Core {
    public static class AspDiExtensions
    {
        public static IServiceCollection AddEntitySearch(this IServiceCollection services) 
        {
            services.AddScoped<IAttributeQueryBuilder, AttributeQueryBuilder>();
            services.AddScoped<IFilteringQueryBuilder, FilteringQueryBuilder>();
            services.AddScoped<Search>();

            return services;
        }
    }
}
