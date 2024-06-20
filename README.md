
# EntitySearch

EntitySearch is an out-of-the-box solution for filtering, sorting, and paginating data. It works with Entity Framework but can be extended to support other ORMs.

## Instalation

Download the nuget package: 
```
dotnet add package khamzakhamzakhamza.EntitySearch.EfCore
```

## Quickstart

To use EntitySearch in an ASP.NET Core application with Entity Framework, all you need to do is add EntitySearch services to the ASP.NET Core DI container:

``` diff
using  EntitySearch.Core;
using  EntitySearch.EfCore;
using  EntitySearch.Example.Data;
using  Microsoft.EntityFrameworkCore;
	
var  builder  =  WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContextFactory<ExampleDbContext>(
	options  =>  options.UseInMemoryDatabase("Example"));
	
+ builder.Services.AddEntitySearch().WithEfCore<ExampleDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();
 ```
Pick an entity you want to search through (here's an example):

``` csharp
using EntitySearch.Example.Enums;
using Microsoft.EntityFrameworkCore;

namespace EntitySearch.Example.Entities;

[PrimaryKey("Id")]
public class Todo {
    public string Id {get;set;} = null!;
    public WeekDays WeekDay { get; set; }
    public string? Name {get;set;}
    public bool Done {get;set;}
    public int SomeValue {get;set;}
}
 ```

Create a filtering spec for that entity with the filter properties you want to use:

``` csharp
using EntitySearch.Core;
using EntitySearch.Core.Attributes;
using EntitySearch.Example.Enums;

namespace EntitySearch.Example.FilteringSpecs;

public class TodoFilteringSpec: IFilteringSpec 
{
    [ShouldEqual("WeekDay")]
    public WeekDays? WeekDayEqual { get; set; }

    [ShouldContainStr("Name")]
    public string? NameContains { get; set; }

    [ShouldEqual("Done")]
    public bool? DoneEqual { get; set; }

    [ShouldBeLess("SomeValue")]
    public int? LessThanSomeValue { get; set; }

    [ShouldBeGreater("SomeValue")]
    public int? GreaterThanSomeValue { get; set; }
}
 ```

Now, to perform the search, you need to use a service called `Search`, which will perform the search and return the results. Search requires filtering, sorting, and paginating specs:

``` diff
using EntitySearch.Core;
using EntitySearch.Core.Specs;
using EntitySearch.Example.Data;
using EntitySearch.Example.Entities;
using EntitySearch.Example.Enums;
using EntitySearch.Example.FilteringSpecs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EntitySearch.Example.Pages;

public class IndexModel : PageModel
{
    public Todo[] Todos {get; set;} = {};

    private readonly ExampleDbContext _context;
+   private readonly Search _search;

    public IndexModel(ExampleDbContext context, Search search)
    {
        _context = context;
        _search = search;
        
        RefreshTodos();
    }

    public async Task OnPostFilterAsync()
    {
        // Collect data from the form 
        var weekDayEqualsOn = Request.Form["weekdayequalson"].FirstOrDefault() == "on";
        var weekDayEquals = (WeekDays)int.Parse(Request.Form["weekdayequals"].First() ?? "0");

        var nameContainsOn = Request.Form["namecontainson"].FirstOrDefault() == "on";
        var nameContains = Request.Form["namecontains"];

        var doneEqualOn = Request.Form["doneequalson"].FirstOrDefault() == "on";
        var doneEqual = Request.Form["doneequals"].FirstOrDefault() == "on";

        var someValueLessThanOn = Request.Form["somevaluelessthanon"].FirstOrDefault() == "on";
        int.TryParse(Request.Form["somevaluelessthan"].FirstOrDefault(), out var someValueLessThan);

        var someValueGreaterThanOn = Request.Form["somevaluegreaterthanon"].FirstOrDefault() == "on";
        int.TryParse(Request.Form["somevaluegreaterthan"].FirstOrDefault(), out var someValueGreaterThan);

        var sortDescending = Request.Form["sortdescending"].FirstOrDefault() == "on";
        var sortBy = Request.Form["sortby"].First();

        // Create specs
+       var filteringSpec = new TodoFilteringSpec {
+           WeekDayEqual = weekDayEqualsOn ? weekDayEquals : null,
+           DoneEqual = doneEqualOn ? doneEqual : null,
+           LessThanSomeValue = someValueLessThanOn ? someValueLessThan : null,
+           GreaterThanSomeValue = someValueGreaterThanOn ? someValueGreaterThan : null,
+       };
        if (nameContainsOn)
            filteringSpec.NameContains = nameContains;
        
+       var sortitngSpec = new SortingSpec(sortBy, sortDescending);

+       var paginatingSpec = new PaginatingSpec(0, 10);

        // Run search
+       var entities = await _search.SearchAsync<TodoFilteringSpec, Todo>(filteringSpec, sortitngSpec, paginatingSpec);

        // Display results
        Todos = entities.Data.ToArray();
    }
}
 ```

That's it! Enjoy standardized, hassle-free searching :)

## Extensibility

By default, EntitySearch builds the simplest search query based on the requirements. To apply modifications to the query, you can create a custom Search service that inherits from the default `Search` service and overrides the `CustomizeQuery` method:

``` csharp
using EntitySearch.Core;
using EntitySearch.Core.Adapters;
using EntitySearch.Core.QueryBuilders;
using Microsoft.EntityFrameworkCore;

namespace EntitySearch.Example;

public class TodoSearch: Search
{
    public TodoSearch(IDataAdapter dataAdapter,
                      IFilteringQueryBuilder filteringQueryBuilder,
                      ISortingQueryBuilder sortingQueryBuilder)
        : base(dataAdapter, filteringQueryBuilder, sortingQueryBuilder) {}

    public override IQueryable<Todo> CustomizeQuery<Todo>(IQueryable<Todo> query)
    {
        // Perform any modification to the query here.
        return query.AsNoTracking(); 
    }
}
 ```

## How it works

EntitySearch dynamically builds expressions and executes them with a data adapter.

If needed, new implementations of `IDataAdapter` can be created to make the library work with different ORMs. 

## License
[MIT](https://choosealicense.com/licenses/mit/)
