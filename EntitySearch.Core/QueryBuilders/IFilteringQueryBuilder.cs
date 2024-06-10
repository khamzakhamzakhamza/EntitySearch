using System.Linq;

namespace EntitySearch.Core.QueryBuilders {
    public interface IFilteringQueryBuilder
    {
        IQueryable<Entity> BuildQuery<FilteringSpec, Entity>(FilteringSpec filteringSpec,
                                                             IQueryable<Entity> entities)
            where FilteringSpec: IFilteringSpec, new()
            where Entity : class, new();
    }
}
