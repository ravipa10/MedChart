using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedChart.DataContracts.Pagination;
using MedChart.DataTransferObjects.Pagination;
using MedChart.Services;
using MedChart.Models;
using MedChart.DataContracts.BloodPressures;
using MedChart.DataTransferObjects;
using Microsoft.AspNetCore.Cors;

namespace MedChart.Controllers
{
    [ApiController]
    [Route("blood_pressure")]
    [Produces("application/json")]
    [EnableCors("AllowUIUrl")]
    public class BloodPressureController : Controller
    {
        private readonly IBloodPressureService _bloodPressureService;
        public BloodPressureController(IMapper mapper,
            IBloodPressureService bloodPressureService) 
        {
            Mapper = mapper;
            _bloodPressureService = bloodPressureService;
        }

        public IMapper Mapper { get; set; }

        #region LIST

        /// <summary>
        /// List all blood pressure entries
        /// </summary>
        /// <param name="addPagination">Pagination Parameters</param>
        /// <returns>Returns the list of blood pressure entries</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedDataContract<BloodPressureDataContract>))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(PagedDataContract<BloodPressureDataContract>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetBloodPressures([FromQuery] AddPaginationRequest addPagination)
        {
            var paginationParams = Mapper.Map<AddPaginationRequest, PaginationParams>(addPagination);

            var response = await _bloodPressureService.List(paginationParams);
            if (response.Success)
            {
                var resources = Mapper.Map<PagedResponse<BloodPressure>, PagedDataContract<BloodPressureDataContract>>(response);
                return Ok(resources);
            }
            return BadRequest();
        }

        #endregion

        #region POST

        /// <summary>
        /// Add a blood pressure entry
        /// </summary>
        /// <param name="bloodPressureRequest">Blood pressure entry to create</param>
        /// <returns>Returns the newly created blood pressure entry</returns>
        /// <response code="201">New blood pressure entry added</response>
        /// <response code="400">Bad Request.</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BloodPressureDataContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> PostBloodPressure(AddBloodPressureRequest bloodPressureRequest)
        {
            var bloodPressure = Mapper.Map<AddBloodPressureRequest, BloodPressure>(bloodPressureRequest);

            var response = await _bloodPressureService.Create(bloodPressure);
            if (response.Success)
            {
                var bloodPressureResource = Mapper.Map<BloodPressure, BloodPressureDataContract>(response.Resource);
                return Ok(bloodPressureResource);
            }
            return NotFound();
        }

        #endregion

        #region PUT

        /// <summary>
        /// Update Blood Pressure entry
        /// </summary>
        /// <param name="id">Blood Pressure ID</param>
        /// <param name="bloodPressureRequest">Blood Pressure to update</param>
        /// <returns>Returns a boolean notifying if the Blood Pressure has been updated properly or not</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Not Found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BloodPressureDataContract))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBloodPressure(Guid id, AddBloodPressureRequest bloodPressureRequest)
        {
            var bloodPressure = Mapper.Map<AddBloodPressureRequest, BloodPressure>(bloodPressureRequest);
            var result = await _bloodPressureService.UpdateAsync(id, bloodPressure);

            if (!result.Success)
                return BadRequest();

            var bloodPressureResource = Mapper.Map<BloodPressure, BloodPressureDataContract>(result.Resource);
            return Ok(bloodPressureResource);
        }

        #endregion

        #region Report

        /// <summary>
        /// List all blood pressure entries for the report
        /// </summary>
        /// <returns>Returns the list of blood pressure entries for the report</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedDataContract<BloodPressureDataContract>))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(PagedDataContract<BloodPressureDataContract>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("report")]
        public async Task<IActionResult> GetBloodPressuresByPeriod([FromQuery] ListBloodPressureRequest listBloodPressureRequest)
        {
            var response = await _bloodPressureService.ListByPeriod(listBloodPressureRequest);
            if (response.Success)
            {
                var resources = Mapper.Map<IEnumerable<BloodPressure>, IEnumerable<BloodPressureDataContract>>(response.ResourceList);
                return Ok(resources);
            }
            return BadRequest();
        }

        #endregion

        #region Outliers

        /// <summary>
        /// List all outliers beyond a thershold value
        /// </summary>
        /// <returns>Returns the list of outliers beyond a thershold value</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedDataContract<BloodPressureDataContract>))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(PagedDataContract<BloodPressureDataContract>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("outliers")]
        public async Task<IActionResult> GetOutliers([FromQuery] ListOutliersRequest listOutliersRequest)
        {
            var response = await _bloodPressureService.ListOutliers(listOutliersRequest);
            if (response.Success)
            {
                var resources = Mapper.Map<IEnumerable<BloodPressure>, IEnumerable<BloodPressureDataContract>>(response.ResourceList);
                return Ok(resources);
            }
            return BadRequest();
        }

        #endregion
    }
}
