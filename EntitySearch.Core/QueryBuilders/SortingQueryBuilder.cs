using System;
using System.Linq.Expressions;
using System.Reflection;

namespace EntitySearch.Core.QueryBuilders {
    public class SortingQueryBuilder: ISortingQueryBuilder
    {
        public Expression<Func<Entity, object>> BuildQuery<Entity>(string sortField)
            where Entity : class, new()
        {
            var property = typeof(Entity).GetProperty(sortField,
                                                      BindingFlags.IgnoreCase
                                                      | BindingFlags.Public
                                                      | BindingFlags.Instance);

            var param = Expression.Parameter(typeof(Entity), "c");
            var propertyAccess = Expression.MakeMemberAccess(param, property);

            return Expression.Lambda<Func<Entity, object>>(propertyAccess, param);
        }
    }
}
