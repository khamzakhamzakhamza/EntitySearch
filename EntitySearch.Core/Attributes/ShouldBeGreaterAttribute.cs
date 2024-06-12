using System;

namespace EntitySearch.Core.Attributes {
    [AttributeUsage(AttributeTargets.Property)]
    public class ShouldBeGreaterAttribute : SpecificationAttribute
    {
        public ShouldBeGreaterAttribute(string entityPropName)
            : base(entityPropName)
        { }
    }
}
