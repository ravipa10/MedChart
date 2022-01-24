using MedChart.DataContracts.BloodPressures;
using MedChart.DataTransferObjects;
using MedChart.DataTransferObjects.Pagination;
using MedChart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedChart.Services
{
    public interface IBloodPressureService
    {
        Task<PagedResponse<BloodPressure>> List(PaginationParams paginationParams);
        Task<BloodPressureResponse> Create(BloodPressure bloodPressure);
        Task<BloodPressureResponse> UpdateAsync(Guid id, BloodPressure bloodPressure);
        Task<BloodPressureResponse> ListByPeriod(ListBloodPressureRequest listBPRequest);
        Task<BloodPressureResponse> ListOutliers(ListOutliersRequest listOutliersRequest);
    }
}
