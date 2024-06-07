using System;
using System.Linq;
using System.Linq.Expressions;
using EntitySearch.Core.Attributes;

namespace EntitySearch.Core.QueryBuilders {
    public class AttributeQueryBuilder: IAttributeQueryBuilder
    {
        public Expression<Func<Entity, bool>> BuildQuery<Entity>(ShouldContainEnumAttribute attribute,
                                                                 ParameterExpression param,
                                                                 object filteringValue) where Entity : class, new()
        {
            var containsMethod = typeof(Enumerable).GetMethods()
                .Single(m => m.Name.Equals(nameof(Enumerable.Contains))
                && m.GetParameters().Length == 2);

            containsMethod = containsMethod.MakeGenericMethod(attribute.EntityPropType.GenericTypeArguments.First());

            var containsEnumExpression = Expression.Call(containsMethod,
                                                         Expression.Property(param, typeof(Entity), attribute.EntityPropName),
                                                         Expression.Constant(filteringValue));

            return Expression.Lambda<Func<Entity, bool>>(containsEnumExpression,
                                                         "Contains Enum Expression",
                                                         true,
                                                         new ParameterExpression[] { param });
        }

        public Expression<Func<Entity, bool>> BuildQuery<Entity>(ShouldContainStrAttribute attribute,
                                                                 ParameterExpression param,
                                                                 object filteringValue) where Entity : class, new()
        {
            var containsStrExpression = Expression.Call(
                                        Expression.Property(param, typeof(Entity), attribute.EntityPropName),
                                        typeof(string).GetMethod(nameof(string.Contains), new Type[] { typeof(string) })!,
                                        Expression.Constant(filteringValue));

            return Expression.Lambda<Func<Entity, bool>>(containsStrExpression,
                                                         "Contains Str Expression",
                                                         true,
                                                         new ParameterExpression[] { param });
        }

        public Expression<Func<Entity, bool>> BuildQuery<Entity>(ShouldBeLessAttribute attribute,
                                                                 ParameterExpression param,
                                                                 object filteringValue) where Entity : class, new()
        {
            var lessExpression = Expression.MakeBinary(ExpressionType.LessThan,
                                                       Expression.Property(param, typeof(Entity), attribute.EntityPropName),
                                                       Expression.Constant(filteringValue));

            return Expression.Lambda<Func<Entity, bool>>(lessExpression,
                                                         "Should Be Less Expression",
                                                         true,
                                                         new ParameterExpression[] { param });
        }

        public Expression<Func<Entity, bool>> BuildQuery<Entity>(ShouldBeGreaterAttribute attribute,
                                                                 ParameterExpression param,
                                                                 object filteringValue) where Entity : class, new()
        {
            var moreExpression = Expression.MakeBinary(ExpressionType.GreaterThan,
                                                       Expression.Property(param, typeof(Entity), attribute.EntityPropName),
                                                       Expression.Constant(filteringValue));

            return Expression.Lambda<Func<Entity, bool>>(moreExpression,
                                                         "Should Be Greater Expression",
                                                         true,
                                                         new ParameterExpression[] { param });
        }

        public Expression<Func<Entity, bool>> BuildQuery<Entity>(ShouldEqualAttribute attribute,
                                                                 ParameterExpression param,
                                                                 object filteringValue) where Entity : class, new()
        {
            var equalExpression = Expression.MakeBinary(ExpressionType.Equal,
                                                        Expression.Property(param, typeof(Entity), attribute.EntityPropName),
                                                        Expression.Constant(filteringValue));

            return Expression.Lambda<Func<Entity, bool>>(equalExpression,
                                                         "Should Be Equal Expression",
                                                         true,
                                                         new ParameterExpression[] { param });
        }
    }
}
