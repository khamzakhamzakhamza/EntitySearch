namespace EntitySearch.Core.Exceptions {
    internal static class ExceptionMessages
    { 
        public static string PropertyNameNotProvided(string filteringSpecType, string attributeType) => 
            $"Property name was not provided for the attribute {attributeType} at the filtering spec {filteringSpecType}";

        public static string PropertyNameInvalid(string propName, string entityType) =>
            $"Property name {propName} could not be found on the entity {entityType}";
    }
}
