namespace EntitySearch.Core.Specs {
    public struct SortingSpec
    {
        public string SortProperty { get; set; }

        public bool SortDescending { get; set; }

        public SortingSpec(string sortProperty,
                           bool sortDescending = false)
        {
            SortProperty = sortProperty;
            SortDescending = sortDescending;
        }
    }
}
