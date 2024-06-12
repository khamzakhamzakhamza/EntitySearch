using System;
using System.Linq.Expressions;

namespace EntitySearch.Core.QueryBuilders {
    public interface ISortingQueryBuilder
    {
        /// <exception cref="PropertyNameInvalidException">Thrown when property could not be found.</exception>
        Expression<Func<TEntity, object>> BuildQuery<TEntity>(string sortProp)
            where TEntity : class, new();
    }
}
