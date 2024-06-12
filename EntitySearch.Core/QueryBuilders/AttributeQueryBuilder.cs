using System;
using System.Linq.Expressions;
using System.Reflection;
using EntitySearch.Core.Attributes;
using EntitySearch.Core.Exceptions;

namespace EntitySearch.Core.QueryBuilders {

    internal class AttributeQueryBuilder: IAttributeQueryBuilder
    {
        public Expression<Func<TEntity, bool>> BuildQuery<TEntity>(Type attributeType,
                                                                   string propName,
                                                                   ParameterExpression param,
                                                                   object filteringValue)
            where TEntity : class, new()
        {
            var property = typeof(TEntity).GetProperty(propName, 
                                                       BindingFlags.Public
                                                       | BindingFlags.Instance);

            if (property is null) {
                var msg = ExceptionMessages.PropertyNameInvalid(propName,
                                                                typeof(TEntity).Name);
                throw new PropertyNameInvalidException(msg);
            }

            return attributeType switch
            {
                Type t when t == typeof(ShouldContainStrAttribute) =>
                    BuildContainsStrQuery<TEntity>(propName, param, filteringValue),
                Type t when t == typeof(ShouldBeLessAttribute) =>
                    BuildBinaryExpressionQuery<TEntity>(ExpressionType.LessThan,
                                                        propName,
                                                        param,
                                                        filteringValue),
                Type t when t == typeof(ShouldBeGreaterAttribute) =>
                    BuildBinaryExpressionQuery<TEntity>(ExpressionType.GreaterThan,
                                                        propName,
                                                        param,
                                                        filteringValue),
                Type t when t == typeof(ShouldEqualAttribute) =>
                    BuildBinaryExpressionQuery<TEntity>(ExpressionType.Equal,
                                                        propName,
                                                        param,
                                                        filteringValue),
                _ => throw new ArgumentException("Unsupported attribute type")
            };
        }

        private Expression<Func<TEntity, bool>> BuildContainsStrQuery<TEntity>(string propName,
                                                                               ParameterExpression param,
                                                                               object filteringValue)
            where TEntity : class, new()
        {
            var containsMethod = typeof(string).GetMethod(nameof(string.Contains), new Type[] { typeof(string) })!;
    
            var constantExpression = Expression.Constant(filteringValue);
            var propertyExpression = Expression.Property(param, typeof(TEntity), propName);
            var containsExpression = Expression.Call(propertyExpression, containsMethod, constantExpression);

            return Expression.Lambda<Func<TEntity, bool>>(containsExpression, param);
        }

        private Expression<Func<TEntity, bool>> BuildBinaryExpressionQuery<TEntity>(ExpressionType expressionType,
                                                                                    string propName,
                                                                                    ParameterExpression param,
                                                                                    object filteringValue)
            where TEntity : class, new()
        {
            var constantExpression = Expression.Constant(filteringValue);
            var propertyExpression = Expression.Property(param, typeof(TEntity), propName);
            var binaryExpression = Expression.MakeBinary(expressionType, propertyExpression, constantExpression);

            return Expression.Lambda<Func<TEntity, bool>>(binaryExpression, param);
        }
    }
}
