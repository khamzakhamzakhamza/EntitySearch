using System;
using System.Linq.Expressions;

namespace EntitySearch.Core.QueryBuilders {
    public interface IAttributeQueryBuilder
    {
        Expression<Func<Entity, bool>> BuildQuery<Entity>(Type attributeType,
                                                          string propName,
                                                          ParameterExpression param,
                                                          object filteringValue) 
            where Entity : class, new();
    }
}
