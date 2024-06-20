using EntitySearch.Core.Adapters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySearch.EfCore {
    public static class AspDiExtensions
    {
        public static IServiceCollection WithEfCore<AppDbContext>(this IServiceCollection services) 
            where AppDbContext: DbContext
        {
            services.AddScoped<IDataAdapter, EfCoreDataAdapter<AppDbContext>>();

            return services;
        }
    }
}
