using System;

namespace EntitySearch.Core.Attributes {
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ShouldContainStrAttribute : SpecificationAttribute
    {
        public ShouldContainStrAttribute(string entityPropName) 
            : base(entityPropName)
        { }
    }
}
