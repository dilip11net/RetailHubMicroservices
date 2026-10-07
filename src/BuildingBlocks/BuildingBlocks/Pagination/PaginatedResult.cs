using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Pagination
{
    public class PaginatedResult<TEntity>(int PageIndex, int PageSize, int TotalCount, IEnumerable<TEntity> Items) where TEntity : class
    {
        public int PageIndex { get; } = PageIndex;
        public int PageSize { get; } = PageSize;
        public int TotalCount { get; } = TotalCount;
        public IEnumerable<TEntity> Items { get; } = Items;
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
