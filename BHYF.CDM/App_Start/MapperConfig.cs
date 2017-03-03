using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BHYF.CDM.Models;
using AutoMapper;

namespace BHYF.CDM
{
    public class MapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<DesignApply, DesignApplyDto>();
            Mapper.CreateMap<DesignApplyDto, DesignApply>();
            Mapper.CreateMap<MoreDesignApplyDto, DesignApply>();
            Mapper.CreateMap<ReviewDesignApplyDto, DesignApply>();

            Mapper.CreateMap<ActivitySuperScholarCreateDto, ActivitySuperScholar>();
            Mapper.CreateMap<ActivitySuperScholarEditDto, ActivitySuperScholar>();

            Mapper.CreateMap<DeliveryApply, DeliveryApplyDto>();
            Mapper.CreateMap<DeliveryApplyProduct, DeliveryApplyProductDto>();
            Mapper.CreateMap<DeliveryApplyProductDto, DeliveryApplyProduct>();
            Mapper.CreateMap<DeliveryApplyPutDto, DeliveryApply>();

        }
    }
}