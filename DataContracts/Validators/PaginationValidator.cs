using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MedChart.DataContracts.Pagination;
using MedChart.Common.QueryExtensions;

namespace MedChart.DataContracts.Validators
{
    public class PaginationValidator : AbstractValidator<AddPaginationRequest>
    {
        public PaginationValidator()
        {
            // Order values
            var orderValues = typeof(EPaginationOrder).GetDescriptions<EPaginationOrder>();
            RuleFor(p => p.Order.ToLower())
                .Must(p => orderValues.Contains(p, StringComparer.OrdinalIgnoreCase))
                .WithMessage("Please use only the following values for Order field: " + string.Join(", ", orderValues));

            RuleFor(p => p.PageSize).GreaterThanOrEqualTo(1).WithMessage("Page size should be greater than or equal to 1")
                .LessThanOrEqualTo(20).WithMessage("Page size should be less than or equal to 20");

            RuleFor(p => p.PageNumber).GreaterThanOrEqualTo(1).WithMessage("Page Number should be greater than or equal to 1");
        }
    }
}
