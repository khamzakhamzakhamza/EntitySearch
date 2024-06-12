using System.Linq;

namespace EntitySearch.Core.QueryBuilders {
    public interface IFilteringQueryBuilder
    {
        /// <exception cref="PropertyNameNotProvidedException">Thrown when the prop name is not provided to the attribute.</exception>
        IQueryable<TEntity> BuildQuery<TFilteringSpec, TEntity>(TFilteringSpec filteringSpec,
                                                                IQueryable<TEntity> entities)
            where TFilteringSpec: IFilteringSpec, new()
            where TEntity : class, new();
    }
}
