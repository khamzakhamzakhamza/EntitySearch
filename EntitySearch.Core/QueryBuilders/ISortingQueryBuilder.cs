using System;
using System.Linq.Expressions;

namespace EntitySearch.Core.QueryBuilders {
    public interface ISortingQueryBuilder
    {
        Expression<Func<Entity, object>> BuildQuery<Entity>(string sortField)
            where Entity : class, new();
    }
}
