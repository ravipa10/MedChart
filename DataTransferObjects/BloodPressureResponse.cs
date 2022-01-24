using MedChart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedChart.DataTransferObjects
{
    public class BloodPressureResponse
    {
        public bool Success { get; }
        private string ExceptionMessage { get; }
        private string UserFriendlyMessage { get; }
        public BloodPressure Resource { get; }
        public IEnumerable<BloodPressure> ResourceList { get; }

        public BloodPressureResponse(BloodPressure resource)
        {
            Success = true;
            ExceptionMessage = string.Empty;
            UserFriendlyMessage = string.Empty;
            Resource = resource;
        }

        public BloodPressureResponse(IEnumerable<BloodPressure> resourceList)
        {
            Success = true;
            ExceptionMessage = string.Empty;
            UserFriendlyMessage = string.Empty;
            ResourceList = resourceList;
        }

        public BloodPressureResponse(string message, string exceptionMessage = null)
        {
            Success = false;
            ExceptionMessage = exceptionMessage;
            UserFriendlyMessage = message;
            Resource = default;
        }
    }
}
