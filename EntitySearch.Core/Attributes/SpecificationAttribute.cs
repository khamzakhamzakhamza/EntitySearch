using System;

namespace EntitySearch.Core.Attributes {
    public abstract class SpecificationAttribute : Attribute
    {
        public string EntityPropName { get; set; }

        public SpecificationAttribute(string entityPropName)
        {
            EntityPropName = entityPropName;
        }
    }
}
