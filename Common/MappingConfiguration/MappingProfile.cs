using AutoMapper;
using MedChart.DataContracts.BloodPressures;
using MedChart.DataContracts.Pagination;
using MedChart.DataTransferObjects.Pagination;
using MedChart.Models;
using MedChart.Common.QueryExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedChart.MappingConfiguration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BloodPressure, BloodPressureDataContract>();

            CreateMap<PagedResponse<BloodPressure>, PagedDataContract<BloodPressureDataContract>>()
                .ForMember(x => x.Items, req => req.MapFrom(src => src.Resource));

            CreateMap<AddPaginationRequest, PaginationParams>()
                .ForMember(x => x.Order, request => request.MapFrom(req => EnumHelper.GetValueFromDescription<EPaginationOrder>(req.Order.ToLower())));
        }
    }
}
