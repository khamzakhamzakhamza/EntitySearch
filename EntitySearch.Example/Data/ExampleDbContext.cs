using EntitySearch.Example.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntitySearch.Example.Data;

public class ExampleDbContext : DbContext
{
    public DbSet<Todo> Todos {get; set;}

    public ExampleDbContext(DbContextOptions<ExampleDbContext> options)
        : base(options)
    {}
}