using EntitySearch.Example.Data;
using EntitySearch.Example.Entities;
using EntitySearch.Example.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EntitySearch.Example.Pages;

public class IndexModel : PageModel
{
    public Todo[] Todos {get; set;} = {};
    private readonly ExampleDbContext _context;

    public IndexModel(ExampleDbContext context)
    {
        _context = context;
        RefreshTodos();
    }

    public void OnGet()
    {
        
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
