using EntitySearch.Core.Adapters;
using Microsoft.EntityFrameworkCore;

namespace EntitySearch.EfCore;

public class EfCoreDataAdapter<AppContext>: IDataAdapter
    where AppContext: DbContext 
{
    private readonly AppContext _context;

    public EfCoreDataAdapter(AppContext context)
    {
        _context = context;
    }

    public async Task<uint> GetCountAsync<Entity>()
        where Entity: class, new()
    {
        var count = await _context.Set<Entity>().CountAsync();

        return (uint)count;
    }

    public IQueryable<Entity> GetBaseQuery<Entity>()
        where Entity: class, new() => 
        _context.Set<Entity>();

    public async Task<ICollection<Entity>> ExecuteQuery<Entity>(IQueryable<Entity> query)
        where Entity: class, new()
    {
        var entities = await query.ToListAsync();
        
        return entities;
    }
}
