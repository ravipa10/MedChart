using System;
using System.ComponentModel.DataAnnotations;

namespace MedChart.Models
{
    public class BloodPressure
    {
        /// <summary>
        /// Blood Pressure ID
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Exam Date
        /// </summary>
        [Required]
        public DateTime ExamDate { get; set; }

        /// <summary>
        /// Creation Date
        /// </summary>
        [Required]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Updation Date
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Systolic Measurement
        /// </summary>
        [Required]
        public int SystolicReading { get; set; }

        /// <summary>
        /// Diastolic Measurement
        /// </summary>
        [Required]
        public int DiastolicReading { get; set; }

        /// <summary>
        /// Heart Rate
        /// </summary>
        [Required]
        public int HeartRate { get; set; }
    }
}
