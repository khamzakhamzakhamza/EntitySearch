using EntitySearch.Example.Enums;
using Microsoft.EntityFrameworkCore;

namespace EntitySearch.Example.Entities;

[PrimaryKey("Id")]
public class Todo {
    public string Id {get; set;} = null!;
    public WeekDays WeekDay { get; set; }
    public string? Name {get; set;}
    public bool Done {get; set;}
    public int SomeValue {get; set;}
    public DateTime CreatedAt {get; set;} 
}