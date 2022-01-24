using MedChart.DataTransferObjects.Pagination;
using MedChart.Models;
using MedChart.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedChart.Common.QueryExtensions;

namespace MedChart.Repositories
{
    public class BloodPressureRepository : IBloodPressureRepository
    {
        protected readonly DatabaseContext _context;
        public BloodPressureRepository(DatabaseContext context)
        {
            _context = context;
        }        

        public async Task<(IEnumerable<BloodPressure>, int)> ListAsync(PaginationParams paginationParams)
        {
            var data = _context.BloodPressures
                .AsNoTracking();

            data = paginationParams.Order == ESortOrder.Ascending ? data.OrderBy(x => x.CreatedDate) : data.OrderByDescending(x => x.CreatedDate);
            var (page, countTotal) = data.QueryPage(paginationParams, null, null, project: x => x);

            var result = await page.ToListAsync();
            return (result, countTotal());
        }

        public async Task AddAsync(BloodPressure bloodPressure)
        {
            await _context.BloodPressures.AddAsync(bloodPressure);
        }

        public void Update(BloodPressure bloodPressure)
        {
            _context.BloodPressures.Update(bloodPressure);
        }
        public async Task<BloodPressure> FindByIdAsync(Guid id)
        {
            return await _context.BloodPressures.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<BloodPressure>> ListByPeriodAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            var data = _context.BloodPressures
                .Where(x => startDate == null || x.ExamDate.Date >= startDate)
                .Where(x => endDate == null || x.ExamDate.Date <= endDate)
                .OrderBy(x => x.ExamDate)
                .AsNoTracking();

            return await data.ToListAsync();
        }

        public async Task<IEnumerable<BloodPressure>> ListOutliersAsync(decimal upperLimit, decimal lowerLimit)
        {
            var data = _context.BloodPressures
                .Where(x => x.SystolicReading > upperLimit || x.DiastolicReading < lowerLimit)
                .OrderBy(x => x.CreatedDate)
                .AsNoTracking();

            return await data.ToListAsync();
        }
    }
}
