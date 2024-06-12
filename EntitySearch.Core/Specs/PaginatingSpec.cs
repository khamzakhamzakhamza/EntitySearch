namespace EntitySearch.Core.Specs {
    public struct PaginatingSpec
    {
        public uint PageNumber { get; set; }

        public uint PageSize { get; set; }

        public PaginatingSpec(uint pageNumber,
                              uint pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
