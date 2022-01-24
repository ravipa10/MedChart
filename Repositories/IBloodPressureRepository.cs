using MedChart.DataTransferObjects.Pagination;
using MedChart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedChart.Repositories
{
    public interface IBloodPressureRepository
    {
        Task<(IEnumerable<BloodPressure>, int)> ListAsync(PaginationParams paginationParams);
        Task AddAsync(BloodPressure bloodPressure);
        void Update(BloodPressure bloodPressure);
        Task<BloodPressure> FindByIdAsync(Guid id);
        Task<IEnumerable<BloodPressure>> ListByPeriodAsync(DateTime? startDate = null, DateTime? endDate = null);
        Task<IEnumerable<BloodPressure>> ListOutliersAsync(decimal upperLimit, decimal lowerLimit);
    }
}
