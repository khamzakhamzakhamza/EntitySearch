using System;

namespace EntitySearch.Core.Attributes {
    [AttributeUsage(AttributeTargets.Property)]
    public class ShouldBeLessAttribute : SpecificationAttribute
    {
        public ShouldBeLessAttribute(string entityPropName)
            : base(entityPropName)
        { }
    }
}
