using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedChart.DataTransferObjects.Pagination
{
    public class PaginationParams
    {
        /// <summary>
        /// Indicates the number of items for a page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Indicates the page number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Indicates the order to display the items in the page
        /// </summary>
        public ESortOrder Order { get; set; }
    }
}
