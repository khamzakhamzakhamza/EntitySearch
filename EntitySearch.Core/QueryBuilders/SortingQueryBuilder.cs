using System;
using System.Linq.Expressions;
using System.Reflection;
using EntitySearch.Core.Exceptions;

namespace EntitySearch.Core.QueryBuilders {
    internal class SortingQueryBuilder: ISortingQueryBuilder
    {
        public Expression<Func<TEntity, object>> BuildQuery<TEntity>(string sortProp)
            where TEntity : class, new()
        {
            var property = typeof(TEntity).GetProperty(sortProp, 
                                                       BindingFlags.Public
                                                       | BindingFlags.Instance);

            if (property is null) {
                var msg = ExceptionMessages.PropertyNameInvalid(sortProp,
                                                                typeof(TEntity).Name);
                throw new PropertyNameInvalidException(msg);
            }

            var parameterExpression = Expression.Parameter(typeof(TEntity));
            var propertyAccessExpression = Expression.MakeMemberAccess(parameterExpression, property);

            var convertedPropertyAccessExpression = Expression.Convert(propertyAccessExpression, typeof(object));

            return Expression.Lambda<Func<TEntity, object>>(convertedPropertyAccessExpression, parameterExpression);
        }
    }
}
