using EntitySearch.Core.Exceptions;
using EntitySearch.Core.QueryBuilders;
using EntitySearch.Example.Entities;

namespace EntitySearch.Tests.QueryBuilders;

public class SortingQueryBuilderTests
{
    private readonly ISortingQueryBuilder _sortingQueryBuilder;

    public SortingQueryBuilderTests()
    {
        _sortingQueryBuilder = new SortingQueryBuilder();
    }

    [Fact]
    public void BuildQuery_PropvidedValidSortObjectProp_ReturnsValidSortQuery()
    {
        // Assert
        var propName = "Name";

        // Act
        var resultQuery = _sortingQueryBuilder.BuildQuery<Todo>(propName);

        // Assert
        Assert.Contains("Name", resultQuery.Body.ToString());
    }

    [Fact]
    public void BuildQuery_PropvidedValidSortStructProp_ReturnsValidSortQuery()
    {
        // Assert
        var propName = "CreatedAt";

        // Act
        var resultQuery = _sortingQueryBuilder.BuildQuery<Todo>(propName);

        // Assert
        Assert.Contains("CreatedAt", resultQuery.Body.ToString());
    }

    [Fact]
    public void BuildQuery_PropvidedInvalidSortProp_ThrowsPropertyNameInvalidException()
    {
        // Act & Assert
        Assert.Throws<PropertyNameInvalidException>(() => _sortingQueryBuilder.BuildQuery<Todo>(""));
    }
}