using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MedChart.DataContracts.BloodPressures
{
    [DataContract]
    public class AddBloodPressureRequest
    {
        /// <summary>
        /// Exam Date
        /// </summary>
        [Required]
        [DataMember(Name = "examDate")]
        public DateTime ExamDate { get; set; }

        /// <summary>
        /// Systolic Measurement
        /// </summary>
        [Required]
        [DataMember(Name = "systolicReading")]
        public int SystolicReading { get; set; }

        /// <summary>
        /// Diastolic Measurement
        /// </summary>
        [Required]
        [DataMember(Name = "diastolicReading")]
        public int DiastolicReading { get; set; }

        /// <summary>
        /// Heart Rate
        /// </summary>
        [Required]
        [DataMember(Name = "heartRate")]
        public int HeartRate { get; set; }
    }
}
