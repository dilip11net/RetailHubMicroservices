using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Pagination
{
    public record PaginationRequest(int PageIndex = 1, int PageSize = 10);
    
}
