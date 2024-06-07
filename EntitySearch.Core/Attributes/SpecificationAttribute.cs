using System;

namespace EntitySearch.Core.Attributes {
    public abstract class SpecificationAttribute : Attribute
    {
        public string EntityPropName { get; set; }
        public Type EntityPropType { get; set; }

        public SpecificationAttribute(string entityPropName, Type entityPropType)
        {
            EntityPropName = entityPropName;
            EntityPropType = entityPropType;
        }
    }
}
