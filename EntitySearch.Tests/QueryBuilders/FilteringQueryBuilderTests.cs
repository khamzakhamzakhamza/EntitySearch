using System.Linq.Expressions;
using EntitySearch.Core;
using EntitySearch.Core.Attributes;
using EntitySearch.Core.Exceptions;
using EntitySearch.Core.QueryBuilders;
using EntitySearch.Example.Entities;
using EntitySearch.Example.Enums;
using EntitySearch.Example.FilteringSpecs;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;

namespace EntitySearch.Tests;

public class FilteringQueryBuilderTests
{
    private readonly Mock<IAttributeQueryBuilder> _attributeQueryBuilderMock;
    private readonly IFilteringQueryBuilder _filteringQueryBuilder;

    public FilteringQueryBuilderTests()
    {
        _attributeQueryBuilderMock = new Mock<IAttributeQueryBuilder>();
        _filteringQueryBuilder = new FilteringQueryBuilder(_attributeQueryBuilderMock.Object);
    }

    [Fact]
    public void BuildQuery_ProvidedValidFilteringSpec_ReturnsValidQuery()
    {
        // Assert
        var expectedEqualPropName = "WeekDay";
        var expectedEqualSpecValue = WeekDays.Monday;
        var expectedContainPropName = "Name";
        var expectedContainSpecValue = "tst";
        var expectedLessPropName = "SomeValue";
        var expectedLessSpecValue = 1;

        var filteringSpec = new TodoFilteringSpec { 
            WeekDayEqual = expectedEqualSpecValue,
            NameContains = expectedContainSpecValue,
            LessThanSomeValue = expectedLessSpecValue
        };

        Expression<Func<Todo, bool>> expectedEqualQuery = entity => entity.WeekDay == WeekDays.Monday;
        Expression<Func<Todo, bool>> expectedContainStrQuery = entity => entity.Name.Contains("tst");
        Expression<Func<Todo, bool>> expectedLessQuery = entity => entity.SomeValue < 1;

        _attributeQueryBuilderMock
            .Setup(x => x.BuildQuery<Todo>(It.IsAny<Type>(),
                                           It.Is<string>(v => v == expectedEqualPropName),
                                           It.IsAny<ParameterExpression>(),
                                           It.Is<object>(v => (WeekDays)v == expectedEqualSpecValue)))
            .Returns(expectedEqualQuery);
        
        _attributeQueryBuilderMock
            .Setup(x => x.BuildQuery<Todo>(It.IsAny<Type>(),
                                           It.Is<string>(v => v == expectedContainPropName),
                                           It.IsAny<ParameterExpression>(),
                                           It.Is<object>(v => (string)v == expectedContainSpecValue)))
            .Returns(expectedContainStrQuery);

        _attributeQueryBuilderMock
            .Setup(x => x.BuildQuery<Todo>(It.IsAny<Type>(),
                                           It.Is<string>(v => v == expectedLessPropName),
                                           It.IsAny<ParameterExpression>(),
                                           It.Is<object>(v => (int)v == expectedLessSpecValue)))
            .Returns(expectedLessQuery);

        // Act
        var resultQuery = _filteringQueryBuilder.BuildQuery(filteringSpec,
                                                            new Todo[] {}.AsQueryable());

        // Assert
        var resultExpressions = resultQuery.Expression.Print();
        Assert.Contains("Where(entity => (int)entity.WeekDay == 0)", resultExpressions);
        Assert.Contains("Where(entity => entity.Name.Contains(\"tst\"))", resultExpressions);
        Assert.Contains("Where(entity => entity.SomeValue < 1)", resultExpressions);
    }

    [Fact]
    public void BuildQuery_ProvidedInvalidPropName_ThrowsPropertyNameNotProvidedException()
    {
        // Assert
        var expectedEqualSpecValue = "tst";

        var filteringSpec = new InvalidFilteringSpec { 
            Test = expectedEqualSpecValue,
        };

        // Act & Assert
        Assert.Throws<PropertyNameNotProvidedException>(() => 
            _filteringQueryBuilder.BuildQuery(filteringSpec, new Todo[] {}.AsQueryable()));
    }
}

public class InvalidFilteringSpec: IFilteringSpec {
    [ShouldEqual(null)]
    public string? Test {get; set;}
}