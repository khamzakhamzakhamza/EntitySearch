using System.Linq;
using System.Linq.Expressions;
using EntitySearch.Core.Attributes;
using EntitySearch.Core.Exceptions;

namespace EntitySearch.Core.QueryBuilders {
    internal class FilteringQueryBuilder: IFilteringQueryBuilder
    {
        private readonly IAttributeQueryBuilder _attributeQueryBuilder;

        public FilteringQueryBuilder(IAttributeQueryBuilder attributeQueryBuilder)
        {
            _attributeQueryBuilder = attributeQueryBuilder;
        }

        public IQueryable<TEntity> BuildQuery<TFilteringSpec, TEntity>(TFilteringSpec filteringSpec,
                                                                       IQueryable<TEntity> entities)
            where TFilteringSpec : IFilteringSpec, new()
            where TEntity : class, new()
        {
            var filteringSpecType = filteringSpec.GetType();
            var filteringSpecProps = filteringSpecType.GetProperties();

            foreach (var prop in filteringSpecProps)
            {
                var attributeData = prop.CustomAttributes
                    .FirstOrDefault(a => a.AttributeType.BaseType == typeof(SpecificationAttribute));

                if (attributeData is null)
                    continue;
            
                var specValue = prop.GetValue(filteringSpec);

                if (specValue is null)
                    continue;

                var attributeType = attributeData.AttributeType;
                var propName = attributeData.ConstructorArguments[0].Value as string;

                if (propName is null) {
                    var msg = ExceptionMessages.PropertyNameNotProvided(filteringSpecType.Name,
                                                                        attributeType.Name);
                    throw new PropertyNameNotProvidedException(msg);
                }

                var parameterExpression = Expression.Parameter(typeof(TEntity));

                var query = _attributeQueryBuilder.BuildQuery<TEntity>(attributeType,
                                                                       propName,
                                                                       parameterExpression,
                                                                       specValue);

                entities = entities.Where(query);
            }

            return entities;
        }
    }
}
