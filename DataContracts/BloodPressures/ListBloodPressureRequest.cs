using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MedChart.DataContracts.BloodPressures
{
    [DataContract]
    public class ListBloodPressureRequest
    {
        /// <summary>
        /// Start Date
        /// </summary>
        [DataMember(Name = "startDate")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End Date
        /// </summary>
        [DataMember(Name = "endDate")]
        public DateTime EndDate { get; set; }
    }
}
