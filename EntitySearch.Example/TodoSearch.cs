using EntitySearch.Core;
using EntitySearch.Core.Adapters;
using EntitySearch.Core.QueryBuilders;
using Microsoft.EntityFrameworkCore;

namespace EntitySearch.Example;

public class TodoSearch: Search
{
    public TodoSearch(IDataAdapter dataAdapter,
                      IFilteringQueryBuilder filteringQueryBuilder,
                      ISortingQueryBuilder sortingQueryBuilder)
        : base(dataAdapter, filteringQueryBuilder, sortingQueryBuilder) {}

    public override IQueryable<Todo> CustomizeQuery<Todo>(IQueryable<Todo> query)
    {
        // Perform any modification to the query here.
        return query.AsNoTracking(); 
    }
}
