using AutoMapper;
using Filed_Coding.Data.Models;
using Filed_Coding.ShearedModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filed_Coding.WebApi.AutomapperProfile
{
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// not a best practice Always provide different mapper profile for dfferent entities for ease of editing
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<Payment, PaymentDto>().ReverseMap(); 
        }
    }
}
