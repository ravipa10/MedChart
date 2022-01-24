using FluentValidation.Attributes;
using MedChart.DataContracts.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MedChart.DataContracts.Pagination
{
    [DataContract]
    [Validator(typeof(PaginationValidator))]
    public class AddPaginationRequest
    {
        public AddPaginationRequest()
        {
            PageSize = 10;
            PageNumber = 1;
            Order = "desc";
        }
        /// <summary>
        /// Indicates the number of items for a page
        /// </summary>
        [DataMember(Name = "pageSize")]
        public int PageSize { get; set; }

        /// <summary>
        /// Indicates the page number
        /// </summary>
        [DataMember(Name = "pageNumber")]
        public int PageNumber { get; set; }

        /// <summary>
        /// Indicates the order to display the items in the page
        /// </summary>
        [DataMember(Name = "order")]
        public string Order { get; set; }
    }
}
