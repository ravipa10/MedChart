using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedChart.DataTransferObjects.Pagination
{
    public class PagedResponse<T>
    {
        public PagedResponse(IEnumerable<T> resource, int pageSize, int totalRecords)
        {
            Success = true;
            ExceptionMessage = string.Empty;
            UserFriendlyMessage = string.Empty;
            Resource = resource;
            TotalNumberOfPages = (int)Math.Ceiling(((decimal)totalRecords / pageSize));
            TotalNumberOfRecords = totalRecords;
        }

        public PagedResponse(string message, string exceptionMessage = null)
        {
            Success = false;
            UserFriendlyMessage = message;
            ExceptionMessage = exceptionMessage;
            Resource = default;
        }

        public string Message(bool isDevelopEnvironment)
        {
            return !string.IsNullOrEmpty(ExceptionMessage) && isDevelopEnvironment ? $"{UserFriendlyMessage}: {ExceptionMessage}" : UserFriendlyMessage;
        }

        /// <summary>
        /// Indicates whether the response is a success or not
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Error message in case the response is a failure
        /// </summary>
        private string ExceptionMessage { get; }

        /// <summary>
        /// Error message in case the response is a failure
        /// </summary>
        private string UserFriendlyMessage { get; }

        /// <summary>
        /// Items
        /// </summary>
        public IEnumerable<T> Resource { get; private set; }

        /// <summary> 
        /// The total number of pages available. 
        /// </summary> 
        public int TotalNumberOfPages { get; set; }

        /// <summary> 
        /// The total number of records available. 
        /// </summary> 
        public int TotalNumberOfRecords { get; set; }
    }
}
