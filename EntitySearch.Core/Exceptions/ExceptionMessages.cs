namespace EntitySearch.Core.Exceptions {
    internal static class ExceptionMessages
    { 
        public static string PropertyNameNotProvided(string filteringSpecType, string attributeType) => 
            $"Entity property name was not provided for the filtering spec '{filteringSpecType}' with the attribute '{attributeType}";
    }
}
