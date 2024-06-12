using System.Collections.Generic;

namespace EntitySearch.Core.DataTransfer {
    public class PaginatedData<Entity>
    {
        public uint PageNumber { get; }

        public uint PageCount { get; }

        public ICollection<Entity> Data { get; set; }

        public PaginatedData(uint pageNumber, uint pageCount, ICollection<Entity> data)
        {
            PageNumber = pageNumber;
            PageCount = pageCount;
            Data = data;
        }
    }
}
 