using EntitySearch.Core.QueryBuilders;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySearch.Core {
    public static class AspDiExtensions
    {
        public static IServiceCollection AddEntitySearch(this IServiceCollection services) {
            services.AddSingleton<IAttributeQueryBuilder, AttributeQueryBuilder>();

            return services;
        }
    }
}
