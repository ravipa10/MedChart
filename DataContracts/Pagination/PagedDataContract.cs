using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MedChart.DataContracts.Pagination
{
    [DataContract]
    public class PagedDataContract<T> where T : class
    {
        /// <summary>
        /// Items
        /// </summary>
        [DataMember(Name = "items", Order = 3)]
        public IEnumerable<T> Items { get; private set; }

        /// <summary> 
        /// The total number of pages available. 
        /// </summary> 
        [DataMember(Name = "totalNumberOfPages", Order = 1)]
        public int TotalNumberOfPages { get; set; }

        /// <summary> 
        /// The total number of records available. 
        /// </summary> 
        [DataMember(Name = "totalNumberOfRecords", Order = 2)]
        public int TotalNumberOfRecords { get; set; }
    }
}
