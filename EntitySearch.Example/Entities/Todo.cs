using EntitySearch.Example.Enums;

namespace EntitySearch.Example.Entities;

public class Todo {
    public WeekDays WeekDay { get; set; }
    public string Name {get;set;}
    public bool Done {get;set;}
    public int SomeValue {get;set;}
}