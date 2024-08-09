using EntitySearch.Core;
using EntitySearch.Core.Adapters;
using EntitySearch.Core.QueryBuilders;
using EntitySearch.Example.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntitySearch.Example;

public class TodoSearch: Search
{
    public TodoSearch(IDataAdapter dataAdapter,
                      IFilteringQueryBuilder filteringQueryBuilder,
                      ISortingQueryBuilder sortingQueryBuilder)
        : base(dataAdapter, filteringQueryBuilder, sortingQueryBuilder) {}

    public override IQueryable<TEntity> CustomizeQuery<TEntity>(IQueryable<TEntity> query)
    {
        var todoQuery = query as IQueryable<Todo>;
        
        return CustomizeTodoQuery(todoQuery) as IQueryable<TEntity>;
    }

    // Perform any modification to the query here.
    private IQueryable<Todo> CustomizeTodoQuery(IQueryable<Todo> query) => query.AsNoTracking();
}
