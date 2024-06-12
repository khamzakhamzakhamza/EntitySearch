using EntitySearch.Core;
using EntitySearch.Core.QueryBuilders;
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
    private readonly Search _search;

    public IndexModel(ExampleDbContext context, Search search)
    {
        _context = context;
        _search = search;
        
        RefreshTodos();
    }

    public async Task OnPostFilterAsync()
    {
        var weekDayEquals = (WeekDays)int.Parse(Request.Form["weekdayequals"].First() ?? "0");
        var nameContains = Request.Form["namecontains"];
        var doneEqual = Request.Form["doneequals"].FirstOrDefault() == "on" ? true : false;
        int.TryParse(Request.Form["somevaluelessthan"].FirstOrDefault(), out var someValueLessThan);
        int.TryParse(Request.Form["somevaluegreaterthan"].FirstOrDefault(), out var someValueGreaterThan);

        var filteringSpec = new TodoFilteringSpec {
            WeekDayEqual = weekDayEquals,
            NameContains  = nameContains,
            DoneEqual = doneEqual,
            LessThanSomeValue = someValueLessThan,
            GreaterThanSomeValue = someValueGreaterThan,
        };

        var sortitngSpec = new SortingSpec("Name");

        var paginatingSpec = new PaginatingSpec(0, 10);

        var entities = await _search.SearchAsync<TodoFilteringSpec, Todo>(filteringSpec, sortitngSpec, paginatingSpec);

        Todos = entities.Data.ToArray();
    }

    public void OnPost()
    {
        var todo = new Todo {
            Id = Guid.NewGuid().ToString(),
            WeekDay = (WeekDays)int.Parse(Request.Form["weekday"].First() ?? "0"),
            Name = Request.Form["name"],
            Done = Request.Form["done"].FirstOrDefault() == "on" ? true : false,
            SomeValue = int.Parse(Request.Form["somevalue"].First() ?? "0"),
        };

        _context.Todos.Add(todo);
        _context.SaveChanges();

        RefreshTodos();
    }

    private void RefreshTodos() => Todos = _context.Todos.ToArray();
}
