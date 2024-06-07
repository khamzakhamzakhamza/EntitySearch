using EntitySearch.Core;
using EntitySearch.Core.Attributes;
using EntitySearch.Example.Entities;
using EntitySearch.Example.Enums;

namespace EntitySearch.Example.FilteringSpecs;

public class TodoFilteringSpec: IFilteringSpec 
{
    [ShouldEqual("WeekDay", typeof(WeekDays))]
    public WeekDays? WeekDay { get; set; }
    [ShouldEqual("Name", typeof(string))]
    public WeekDays? OrderStatus { get; set; }
}
