using System;
using System.Linq.Expressions;
using EntitySearch.Core.Attributes;

namespace EntitySearch.Core.QueryBuilders {
    public class AttributeQueryBuilder: IAttributeQueryBuilder
    {
        public Expression<Func<Entity, bool>> BuildQuery<Entity>(Type attributeType,
                                                                 string propName,
                                                                 ParameterExpression param,
                                                                 object filteringValue)
            where Entity : class, new()
        {
            if (attributeType == typeof(ShouldContainStrAttribute))
                return BuildShouldContainStrAttributeQuery<Entity>(propName, param, filteringValue);
            else if (attributeType == typeof(ShouldBeLessAttribute))
                return BuildShouldBeLessAttributeQuery<Entity>(propName, param, filteringValue);
            else if (attributeType == typeof(ShouldBeGreaterAttribute))
                return BuildShouldBeGreaterAttributeQuery<Entity>(propName, param, filteringValue);
            else if (attributeType == typeof(ShouldEqualAttribute))
                return BuildShouldEqualAttributeQuery<Entity>(propName, param, filteringValue);

            throw new ArgumentException("Unsoported attribute type");
        }

        private Expression<Func<Entity, bool>> BuildShouldContainStrAttributeQuery<Entity>(string propName,
                                                                                           ParameterExpression param,
                                                                                           object filteringValue)
            where Entity : class, new()
        {
            var containsStrExpression = Expression.Call(Expression.Property(param, typeof(Entity), propName),
                                                        typeof(string).GetMethod(nameof(string.Contains), new Type[] { typeof(string) })!,
                                                        Expression.Constant(filteringValue));

            return Expression.Lambda<Func<Entity, bool>>(containsStrExpression,
                                                         "Contains Str Expression",
                                                         true,
                                                         new ParameterExpression[] { param });
        }

        private Expression<Func<Entity, bool>> BuildShouldBeLessAttributeQuery<Entity>(string propName,
                                                                                       ParameterExpression param,
                                                                                       object filteringValue)
            where Entity : class, new()
        {
            var lessExpression = Expression.MakeBinary(ExpressionType.LessThan,
                                                       Expression.Property(param, typeof(Entity), propName),
                                                       Expression.Constant(filteringValue));

            return Expression.Lambda<Func<Entity, bool>>(lessExpression,
                                                         "Should Be Less Expression",
                                                         true,
                                                         new ParameterExpression[] { param });
        }

        private Expression<Func<Entity, bool>> BuildShouldBeGreaterAttributeQuery<Entity>(string propName,
                                                                                          ParameterExpression param,
                                                                                          object filteringValue)
            where Entity : class, new()
        {
            var moreExpression = Expression.MakeBinary(ExpressionType.GreaterThan,
                                                       Expression.Property(param, typeof(Entity), propName),
                                                       Expression.Constant(filteringValue));

            return Expression.Lambda<Func<Entity, bool>>(moreExpression,
                                                         "Should Be Greater Expression",
                                                         true,
                                                         new ParameterExpression[] { param });
        }

        private Expression<Func<Entity, bool>> BuildShouldEqualAttributeQuery<Entity>(string propName,
                                                                                      ParameterExpression param,
                                                                                      object filteringValue)
            where Entity : class, new()
        {
            var equalExpression = Expression.MakeBinary(ExpressionType.Equal,
                                                        Expression.Property(param, typeof(Entity), propName),
                                                        Expression.Constant(filteringValue));

            return Expression.Lambda<Func<Entity, bool>>(equalExpression,
                                                         "Should Be Equal Expression",
                                                         true,
                                                         new ParameterExpression[] { param });
        }
    }
}
