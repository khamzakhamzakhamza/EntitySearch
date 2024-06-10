using System.Collections.Generic;

namespace EntitySearch.Core.DataTransfer {
    public struct PaginatedData<Entity>
    {
        public uint Page { get; }

        public uint PageCount { get; }

        public ICollection<Entity> Data { get; set; }

        public PaginatedData(uint page, uint pageCount, ICollection<Entity> data)
        {
            Page = page;
            PageCount = pageCount;
            Data = data;
        }
    }
}
 