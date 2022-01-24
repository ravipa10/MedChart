using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MedChart.DataContracts.BloodPressures
{
    [DataContract]
    public class BloodPressureDataContract : AddBloodPressureRequest
    {
        /// <summary>
        /// Blood Pressure Id
        /// </summary>
        [Required]
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Exam Date
        /// </summary>
        [Required]
        [DataMember(Name = "createdDate")]
        public DateTime CreatedDate { get; set; }
    }
}
