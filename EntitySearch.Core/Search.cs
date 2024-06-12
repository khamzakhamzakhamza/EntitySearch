using System.Linq;
using System.Threading.Tasks;
using EntitySearch.Core.Adapters;
using EntitySearch.Core.DataTransfer;
using EntitySearch.Core.QueryBuilders;
using EntitySearch.Core.Specs;

namespace EntitySearch.Core {
    public class Search
    {
        private readonly IDataAdapter _dataAdapter;
        private readonly IFilteringQueryBuilder _filteringQueryBuilder;
        private readonly ISortingQueryBuilder _sortingQueryBuilder;

        public Search(IDataAdapter dataAdapter,
                      IFilteringQueryBuilder filteringQueryBuilder,
                      ISortingQueryBuilder sortingQueryBuilder)
        {
            _dataAdapter = dataAdapter;
            _filteringQueryBuilder = filteringQueryBuilder;
            _sortingQueryBuilder = sortingQueryBuilder;
        }

        public async Task<PaginatedData<Entity>> SearchAsync<FilteringSpec, Entity>(FilteringSpec filteringSpec,
                                                                                    SortingSpec sortingSpec,
                                                                                    PaginatingSpec paginatingSpec)
            where FilteringSpec: IFilteringSpec, new()
            where Entity: class, new()
        {
            var pageCount = await _dataAdapter.GetCountAsync<Entity>() / paginatingSpec.PageSize;

            // apply filtering
            var query = _filteringQueryBuilder
                .BuildQuery(filteringSpec, _dataAdapter.GetBaseQuery<Entity>());

            // apply sorting
            query = sortingSpec.SortDescending
                ? query.OrderByDescending(_sortingQueryBuilder.BuildQuery<Entity>(sortingSpec.SortProperty))
                : query.OrderBy(_sortingQueryBuilder.BuildQuery<Entity>(sortingSpec.SortProperty));

            // apply pagination
            query = query
                .Skip((int)(paginatingSpec.Page * paginatingSpec.PageSize))
                .Take((int)paginatingSpec.PageSize);
            
            var entities = await _dataAdapter.ExecuteQuery(CustomizeQuery(query));

            return new PaginatedData<Entity>(paginatingSpec.Page, pageCount, entities);
        }

        public virtual IQueryable<Entity> CustomizeQuery<Entity>(IQueryable<Entity> query) 
            where Entity: class, new() => query;
    }
}
