using System;
using System.Linq.Expressions;
using EntitySearch.Core.Attributes;

namespace EntitySearch.Core.QueryBuilders {
    public interface IAttributeQueryBuilder
    {
        Expression<Func<Entity, bool>> BuildQuery<Entity>(ShouldContainEnumAttribute attribute,
                                                          ParameterExpression param,
                                                          object filteringValue) where Entity : class, new();
        Expression<Func<Entity, bool>> BuildQuery<Entity>(ShouldContainStrAttribute attribute,
                                                          ParameterExpression param,
                                                          object filteringValue) where Entity : class, new();
        Expression<Func<Entity, bool>> BuildQuery<Entity>(ShouldBeLessAttribute attribute,
                                                          ParameterExpression param,
                                                          object filteringValue) where Entity : class, new();
        Expression<Func<Entity, bool>> BuildQuery<Entity>(ShouldBeGreaterAttribute attribute,
                                                          ParameterExpression param,
                                                          object filteringValue) where Entity : class, new();
        Expression<Func<Entity, bool>> BuildQuery<Entity>(ShouldEqualAttribute attribute,
                                                          ParameterExpression param,
                                                          object filteringValue) where Entity : class, new();
    }
}
