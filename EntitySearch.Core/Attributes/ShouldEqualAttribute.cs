using System;

namespace EntitySearch.Core.Attributes {
    [AttributeUsage(AttributeTargets.Property)]
    public class ShouldEqualAttribute : SpecificationAttribute
    {
        public ShouldEqualAttribute(string entityPropName)
            : base(entityPropName)
        { }
    }
}
