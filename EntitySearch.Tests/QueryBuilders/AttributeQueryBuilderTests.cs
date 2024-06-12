using System.Linq.Expressions;
using EntitySearch.Core.Attributes;
using EntitySearch.Core.Exceptions;
using EntitySearch.Core.QueryBuilders;
using EntitySearch.Example.Entities;
using EntitySearch.Example.Enums;

namespace EntitySearch.Tests.QueryBuilders;

public class AttributeQueryBuilderTests
{
    private readonly IAttributeQueryBuilder _attributeQueryBuilder;

    public AttributeQueryBuilderTests()
    {
        _attributeQueryBuilder = new AttributeQueryBuilder();
    }

    [Theory]
    [InlineData("WeekDay", WeekDays.Monday, "(Param_0.WeekDay == Monday)")]
    [InlineData("Name", "hello", "(Param_0.Name == \"hello\")")]
    [InlineData("Done", true, "(Param_0.Done == True)")]
    [InlineData("SomeValue", 1, "(Param_0.SomeValue == 1)")]
    public void BuildQuery_ShouldEqualAttribute_ReturnsEqualValueQuery(string propName,
                                                                       object value,
                                                                       string expected)
    {
        // Arrange
        var param = Expression.Parameter(typeof(Todo));

        // Act
        var resultQuery = _attributeQueryBuilder.BuildQuery<Todo>(typeof(ShouldEqualAttribute),
                                                                  propName,
                                                                  param,
                                                                  value);

        // Assert
        Assert.Equal(expected, resultQuery.Body.ToString());
    }

    [Fact]
    public void BuildQuery_ShouldBeGreaterAttributeWithIntValue_ReturnsGreaterValueQuery()
    {
        // Assert
        var propName = "SomeValue";
        var param = Expression.Parameter(typeof(Todo));
        var value = 1;

        // Act
        var resultQuery = _attributeQueryBuilder.BuildQuery<Todo>(typeof(ShouldBeGreaterAttribute),
                                                                  propName,
                                                                  param,
                                                                  value);

        // Assert
        Assert.Equal("(Param_0.SomeValue > 1)", resultQuery.Body.ToString());
    }

    [Fact]
    public void BuildQuery_ShouldBeLessAttributeWithIntValue_ReturnsLessValueQuery()
    {
        // Assert
        var propName = "SomeValue"; 
        var param = Expression.Parameter(typeof(Todo));
        var value = 1;

        // Act
        var resultQuery = _attributeQueryBuilder.BuildQuery<Todo>(typeof(ShouldBeLessAttribute),
                                                                  propName,
                                                                  param,
                                                                  value);

        // Assert
        Assert.Equal("(Param_0.SomeValue < 1)", resultQuery.Body.ToString());
    }

    [Fact]
    public void BuildQuery_ShouldContainStrAttribute_ReturnsContainStrQuery()
    {
        // Assert
        var propName = "Name";
        var param = Expression.Parameter(typeof(Todo));
        var value = "name";

        // Act
        var resultQuery = _attributeQueryBuilder.BuildQuery<Todo>(typeof(ShouldContainStrAttribute),
                                                                  propName,
                                                                  param,
                                                                  value);

        // Assert
        Assert.Equal("Param_0.Name.Contains(\"name\")", resultQuery.Body.ToString());
    }

    [Fact]
    public void BuildQuery_InvalidAttributeType_ThrowsArgumentException()
    {
        // Arrange
        var propName = "Name";
        var param = Expression.Parameter(typeof(Todo));
        var value = "test";

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            _attributeQueryBuilder.BuildQuery<Todo>(typeof(AttributeQueryBuilderTests),
                                                    propName,
                                                    param,
                                                    value));
    }

    [Fact]
    public void BuildQuery_InvalidPropName_ThrowsPropertyNameInvalidException()
    {
        // Arrange
        var param = Expression.Parameter(typeof(Todo));
        var value = "test";

        // Act & Assert
        Assert.Throws<PropertyNameInvalidException>(() =>
            _attributeQueryBuilder.BuildQuery<Todo>(typeof(AttributeQueryBuilderTests),
                                                    "",
                                                    param,
                                                    value));
    }
}