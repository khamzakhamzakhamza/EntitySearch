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
