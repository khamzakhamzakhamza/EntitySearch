using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntitySearch.Core.Adapters {
    public interface IDataAdapter
    {
        Task<uint> GetCountAsync<Entity>() where Entity: class, new();

        IQueryable<Entity> GetBaseQuery<Entity>() where Entity: class, new();

        Task<ICollection<Entity>> ExecuteQuery<Entity>(IQueryable<Entity> query) where Entity: class, new();
    }
}
