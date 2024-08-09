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
    private readonly TodoSearch _search;

    public IndexModel(ExampleDbContext context, TodoSearch search)
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
        var filteringSpec = new TodoFilteringSpec {
            WeekDayEqual = weekDayEqualsOn ? weekDayEquals : null,
            DoneEqual = doneEqualOn ? doneEqual : null,
            LessThanSomeValue = someValueLessThanOn ? someValueLessThan : null,
            GreaterThanSomeValue = someValueGreaterThanOn ? someValueGreaterThan : null,
        };

        if (nameContainsOn)
            filteringSpec.NameContains = nameContains;
        
        var sortitngSpec = new SortingSpec(sortBy, sortDescending);

        var paginatingSpec = new PaginatingSpec(0, 10);

        // Run search
        var entities = await _search.SearchAsync<TodoFilteringSpec, Todo>(filteringSpec, sortitngSpec, paginatingSpec);

        // Display results
        Todos = entities.Data.ToArray();
    }

    public void OnPostCreate()
    {
        var todo = new Todo {
            Id = Guid.NewGuid().ToString(),
            WeekDay = (WeekDays)int.Parse(Request.Form["weekday"].First() ?? "0"),
            Name = Request.Form["name"],
            Done = Request.Form["done"].FirstOrDefault() == "on" ? true : false,
            SomeValue = int.Parse(Request.Form["somevalue"].First() ?? "0"),
            CreatedAt = DateTime.UtcNow
        };

        _context.Todos.Add(todo);
        _context.SaveChanges();

        RefreshTodos();
    }

    private void RefreshTodos() => Todos = _context.Todos.ToArray();
}
