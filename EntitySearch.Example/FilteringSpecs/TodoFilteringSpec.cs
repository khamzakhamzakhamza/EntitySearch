using EntitySearch.Core;
using EntitySearch.Core.Attributes;
using EntitySearch.Example.Enums;

namespace EntitySearch.Example.FilteringSpecs;

public class TodoFilteringSpec: IFilteringSpec 
{
    [ShouldEqual("WeekDay", typeof(WeekDays))]
    public WeekDays? WeekDayEqual { get; set; }

    [ShouldContainStr("Name", typeof(string))]
    public string? NameContains { get; set; }

    [ShouldEqual("Done", typeof(bool))]
    public bool? DoneEqual { get; set; }

    [ShouldBeLess("SomeValue", typeof(int))]
    public int? LessThanSomeValue { get; set; }

    [ShouldBeGreater("SomeValue", typeof(int))]
    public int? GreaterThanSomeValue { get; set; }
}
