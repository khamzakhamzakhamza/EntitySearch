using EntitySearch.Core;
using EntitySearch.Core.Adapters;
using EntitySearch.Core.QueryBuilders;
using EntitySearch.Core.Specs;
using EntitySearch.Example.Entities;
using EntitySearch.Example.FilteringSpecs;
using Moq;

namespace EntitySearch.Tests;

public class SearchTests
{
    private readonly Mock<IDataAdapter> _dataAdapterMock;
    private readonly Mock<IFilteringQueryBuilder> _filteringQueryBuilderMock;
    private readonly Mock<ISortingQueryBuilder> _sortingQueryBuilderMock;
    private readonly Search _search;

    public SearchTests()
    {
        _dataAdapterMock = new Mock<IDataAdapter>();
        _filteringQueryBuilderMock = new Mock<IFilteringQueryBuilder>();
        _sortingQueryBuilderMock = new Mock<ISortingQueryBuilder>();
        _search = new Search(_dataAdapterMock.Object,
                             _filteringQueryBuilderMock.Object,
                             _sortingQueryBuilderMock.Object);
    }

    [Theory]
    [InlineData(3, 11, 5, "Done")]
    [InlineData(2, 10, 5, "Name")]
    [InlineData(0, 0, 5, "CreatedAt")]
    [InlineData(1, 4, 5, "Id")]
    public async Task SearchAsync_ValidQuery_CorrectPageCount(uint expectedPageCount,
                                                              uint entitiesCount,
                                                              uint pageSize,
                                                              string sortingPropName) 
    {
        // Assert
        var query = new Todo[] {}.AsQueryable();
        var filteringSpec = new TodoFilteringSpec();
        var paginatingSpec = new PaginatingSpec(0, pageSize);
        var sortingSpec = new SortingSpec(sortingPropName);

        _dataAdapterMock.Setup(x => x.GetCountAsync<Todo>())
            .ReturnsAsync(entitiesCount);
        _dataAdapterMock.Setup(x => x.GetBaseQuery<Todo>())
            .Returns(query);
        _filteringQueryBuilderMock
            .Setup(x => x.BuildQuery(It.IsAny<TodoFilteringSpec>(),
                                     It.IsAny<IQueryable<Todo>>()))
            .Returns(query);
        _sortingQueryBuilderMock
            .Setup(x => x.BuildQuery<Todo>(It.IsAny<string>()))
            .Returns(t => t.Done);
        _dataAdapterMock
            .Setup(x => x.ExecuteQuery(It.IsAny<IQueryable<Todo>>()))
            .ReturnsAsync((ICollection<Todo>) new List<Todo>());
        
        // Act
        var result = await _search.SearchAsync<TodoFilteringSpec, Todo>(filteringSpec,
                                                                        sortingSpec,
                                                                        paginatingSpec);

        // Assert
        Assert.Equal(expectedPageCount, result.PageCount);
    }
}