using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MedChart.DataContracts.Pagination
{
    public enum EPaginationOrder
    {
        [Description("asc")]
        Ascending,

        [Description("desc")]
        Descending
    }
}
