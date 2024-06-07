using EntitySearch.Core;
using EntitySearch.Core.Attributes;
using EntitySearch.Example.Enums;

namespace EntitySearch.Example.FilteringSpecs;

public class TodoFilteringSpec: IFilteringSpec 
{
    [ShouldEqual("WeekDay", typeof(WeekDays))]
    public WeekDays? WeekDay { get; set; }

    [ShouldContainStr("Name", typeof(string))]
    public string? Name { get; set; }

    [ShouldBeLess("SomeValue", typeof(int))]
    public string? SomeValue { get; set; }
}
