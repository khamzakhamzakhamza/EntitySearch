using System;
using System.Linq;
using System.Linq.Expressions;
using EntitySearch.Core.Attributes;

namespace EntitySearch.Core.QueryBuilders {
    public class FilteringQueryBuilder: IFilteringQueryBuilder
    {
        private readonly IAttributeQueryBuilder _attributeQueryBuilder;

        public FilteringQueryBuilder(IAttributeQueryBuilder attributeQueryBuilder)
        {
            _attributeQueryBuilder = attributeQueryBuilder
                ?? throw new ArgumentNullException($"Attribute query builder {nameof(IAttributeQueryBuilder)} is not provided.");
        }

        public IQueryable<Entity> BuildQuery<FilteringSpec, Entity>(FilteringSpec filteringSpec,
                                                                    IQueryable<Entity> entities)
            where FilteringSpec : IFilteringSpec, new()
            where Entity : class, new()
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

                var param = Expression.Parameter(typeof(Entity));
                var attributeType = attributeData.AttributeType;
                var propName = (string)attributeData.ConstructorArguments[0].Value!;


                var query = _attributeQueryBuilder.BuildQuery<Entity>(attributeType,
                                                                      propName,
                                                                      param,
                                                                      specValue);

                entities = entities.Where(query);
            }

            return entities;
        }
    }
}
