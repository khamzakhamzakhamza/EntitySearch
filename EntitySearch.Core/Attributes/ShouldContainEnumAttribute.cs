using System;

namespace EntitySearch.Core.Attributes {
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ShouldContainEnumAttribute : SpecificationAttribute
    {
        public ShouldContainEnumAttribute(string entityPropName) 
            : base(entityPropName)
        { }
    }
}
