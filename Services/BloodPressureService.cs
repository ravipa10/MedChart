using MedChart.DataContracts.BloodPressures;
using MedChart.DataTransferObjects;
using MedChart.DataTransferObjects.Pagination;
using MedChart.Models;
using MedChart.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedChart.Services
{
    public class BloodPressureService : IBloodPressureService
    {
        private readonly IBloodPressureRepository _bloodPressureRepository;
        private readonly IUnitOfWork _unitOfWork;
        public BloodPressureService(IBloodPressureRepository bloodPressureRepository,
            IUnitOfWork unitOfWork)
        {
            _bloodPressureRepository = bloodPressureRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<PagedResponse<BloodPressure>> List(PaginationParams paginationParams)
        {
            try
            {
                var data = await _bloodPressureRepository.ListAsync(paginationParams);
                return new PagedResponse<BloodPressure>(data.Item1, paginationParams.PageSize, data.Item2);
            }
            catch (Exception ex)
            {
                return new PagedResponse<BloodPressure>("An error occurred getting the blood pressure entries", ex.Message);
            }
        }

        public async Task<BloodPressureResponse> Create(BloodPressure bloodPressure)
        {
            try
            {
                await _bloodPressureRepository.AddAsync(bloodPressure);
                await _unitOfWork.CompleteAsync();

                return new BloodPressureResponse(bloodPressure);

            }
            catch (Exception ex)
            {
                return new BloodPressureResponse("An error occurred when saving the blood pressure entry", ex.Message);
            }
        }

        public async Task<BloodPressureResponse> UpdateAsync(Guid id, BloodPressure bloodPressure)
        {
            try
            {
                var existingBP = await _bloodPressureRepository.FindByIdAsync(id);
                if (existingBP == null)
                    return new BloodPressureResponse("Blood Pressure not found");

                existingBP.ExamDate = bloodPressure.ExamDate;
                existingBP.SystolicReading = bloodPressure.SystolicReading;
                existingBP.DiastolicReading = bloodPressure.DiastolicReading;
                existingBP.HeartRate = bloodPressure.HeartRate;
                existingBP.UpdatedDate = DateTime.UtcNow;

                _bloodPressureRepository.Update(existingBP);
                await _unitOfWork.CompleteAsync();

                return new BloodPressureResponse(existingBP);
            }
            catch (Exception ex)
            {
                return new BloodPressureResponse("An error occurred when updating the blood pressure entry", ex.Message);
            }
        }

        public async Task<BloodPressureResponse> ListByPeriod(ListBloodPressureRequest listBPRequest)
        {
            try
            {
                var data = await _bloodPressureRepository.ListByPeriodAsync(listBPRequest.StartDate, listBPRequest.EndDate);
                return new BloodPressureResponse(data);
            }
            catch (Exception ex)
            {
                return new BloodPressureResponse("An error occurred while getting blood pressure entries by period", ex.Message);
            }
        }

        public async Task<BloodPressureResponse> ListOutliers(ListOutliersRequest listOutliersRequest)
        {
            try
            {
                var upperLimit = listOutliersRequest.SystolicReading + (listOutliersRequest.SystolicReading * listOutliersRequest.ThersholdPercent / 100);
                var lowerLimit = listOutliersRequest.DiastolicReading - (listOutliersRequest.DiastolicReading * listOutliersRequest.ThersholdPercent / 100);
                var data = await _bloodPressureRepository.ListOutliersAsync(upperLimit, lowerLimit);
                return new BloodPressureResponse(data);
            }
            catch (Exception ex)
            {
                return new BloodPressureResponse("An error occurred while getting outliers", ex.Message);
            }
        }
    }
}
