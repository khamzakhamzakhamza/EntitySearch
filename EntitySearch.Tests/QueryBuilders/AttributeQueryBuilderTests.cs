using System.Linq.Expressions;
using EntitySearch.Core.Attributes;
using EntitySearch.Core.QueryBuilders;
using EntitySearch.Example.Entities;
using EntitySearch.Example.Enums;

namespace EntitySearch.Tests;

public class AttributeQueryBuilderTests
{
    private readonly IAttributeQueryBuilder _attributeQueryBuilder;

    public AttributeQueryBuilderTests()
    {
        _attributeQueryBuilder = new AttributeQueryBuilder();
    }

    [Fact]
    public void BuildQuery_ShouldEqualAttributeWithEnumValue_ReturnsEqualValueQuery()
    {
        // Assert
        var attribute = new ShouldEqualAttribute("WeekDay", typeof(WeekDays));
        var param = Expression.Parameter(typeof(Todo));
        var value = WeekDays.Monday;

        // Act
        var resultQuery = _attributeQueryBuilder.BuildQuery<Todo>(attribute, param, value);

        // Assert
        Assert.Equal("Should Be Equal Expression", resultQuery.Name);
        Assert.Equal("(Param_0.WeekDay == Monday)", resultQuery.Body.ToString());
    }

    [Fact]
    public void BuildQuery_ShouldEqualAttributeWithStringValue_ReturnsEqualValueQuery()
    {
        // Assert
        var attribute = new ShouldEqualAttribute("Name", typeof(string));
        var param = Expression.Parameter(typeof(Todo));
        var value = "hello";

        // Act
        var resultQuery = _attributeQueryBuilder.BuildQuery<Todo>(attribute, param, value);

        // Assert
        Assert.Equal("Should Be Equal Expression", resultQuery.Name);
        Assert.Equal("(Param_0.Name == \"hello\")", resultQuery.Body.ToString());
    }

    [Fact]
    public void BuildQuery_ShouldEqualAttributeWithBoolValue_ReturnsEqualValueQuery()
    {
        // Assert
        var attribute = new ShouldEqualAttribute("Done", typeof(bool));
        var param = Expression.Parameter(typeof(Todo));
        var value = true;

        // Act
        var resultQuery = _attributeQueryBuilder.BuildQuery<Todo>(attribute, param, value);

        // Assert
        Assert.Equal("Should Be Equal Expression", resultQuery.Name);
        Assert.Equal("(Param_0.Done == True)", resultQuery.Body.ToString());
    }

    [Fact]
    public void BuildQuery_ShouldEqualAttributeWithIntValue_ReturnsEqualValueQuery()
    {
        // Assert
        var attribute = new ShouldEqualAttribute("SomeValue", typeof(int));
        var param = Expression.Parameter(typeof(Todo));
        var value = 1;

        // Act
        var resultQuery = _attributeQueryBuilder.BuildQuery<Todo>(attribute, param, value);

        // Assert
        Assert.Equal("Should Be Equal Expression", resultQuery.Name);
        Assert.Equal("(Param_0.SomeValue == 1)", resultQuery.Body.ToString());
    }
}