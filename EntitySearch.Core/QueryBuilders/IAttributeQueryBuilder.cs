using System;
using System.Linq.Expressions;

namespace EntitySearch.Core.QueryBuilders {
    public interface IAttributeQueryBuilder
    {
        /// <exception cref="ArgumentException">Thrown when the attribute type is unsupported.</exception>
        /// <exception cref="PropertyNameInvalidException">Thrown when property could not be found.</exception>
        Expression<Func<TEntity, bool>> BuildQuery<TEntity>(Type attributeType,
                                                          string propName,
                                                          ParameterExpression param,
                                                          object filteringValue) 
            where TEntity : class, new();
    }
}
