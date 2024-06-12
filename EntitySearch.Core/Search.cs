using System.Linq;
using System.Threading.Tasks;
using EntitySearch.Core.Adapters;
using EntitySearch.Core.DataTransfer;
using EntitySearch.Core.Exceptions;
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
            _filteringQueryBuilder = filteringQueryBuilder;
            _sortingQueryBuilder = sortingQueryBuilder;
            _dataAdapter = dataAdapter 
                ?? throw new DataAdapterMissingException(ExceptionMessages.DataAdapterMissing());
        }

        public async Task<PaginatedData<TEntity>> SearchAsync<TFilteringSpec, TEntity>(TFilteringSpec filteringSpec,
                                                                                       SortingSpec sortingSpec,
                                                                                       PaginatingSpec paginatingSpec)
            where TFilteringSpec: IFilteringSpec, new()
            where TEntity: class, new()
        {
            var pageCount = await _dataAdapter.GetCountAsync<TEntity>() / paginatingSpec.PageSize;

            // apply filtering
            var query = _filteringQueryBuilder
                .BuildQuery(filteringSpec, _dataAdapter.GetBaseQuery<TEntity>());

            // apply sorting
            query = sortingSpec.SortDescending
                ? query.OrderByDescending(_sortingQueryBuilder.BuildQuery<TEntity>(sortingSpec.SortProperty))
                : query.OrderBy(_sortingQueryBuilder.BuildQuery<TEntity>(sortingSpec.SortProperty));

            // apply pagination
            query = query
                .Skip((int)(paginatingSpec.PageNumber * paginatingSpec.PageSize))
                .Take((int)paginatingSpec.PageSize);
            
            var entities = await _dataAdapter.ExecuteQuery(CustomizeQuery(query));

            return new PaginatedData<TEntity>(paginatingSpec.PageNumber, pageCount, entities);
        }

        public virtual IQueryable<TEntity> CustomizeQuery<TEntity>(IQueryable<TEntity> query) 
            where TEntity: class, new() => query;
    }
}
