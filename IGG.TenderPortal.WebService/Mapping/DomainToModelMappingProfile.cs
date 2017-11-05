﻿using AutoMapper;
using IGG.TenderPortal.DtoModel.Models;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.WebService.Mapping
{
    public class DomainToModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToModelMappings";

        public DomainToModelMappingProfile()
        {                    
            CreateMap<Employee, EmployeeModel>()
                .ForMember(g => g.EmployeeId, map => map.MapFrom(vm => vm.EmployeeId))
                .ForMember(g => g.EmployeeNumber, map => map.MapFrom(vm => vm.EmployeeNumber))
                .ForMember(g => g.FirstName, map => map.MapFrom(vm => vm.Person.FirstName))
                .ForMember(g => g.LastName, map => map.MapFrom(vm => vm.Person.LastName))
                .ForMember(g => g.BirhDate, map => map.MapFrom(vm => vm.Person.BirhDate))
                .ForMember(g => g.EmployeeDate, map => map.MapFrom(vm => vm.EmployeeDate))
                .ForMember(g => g.Terminated, map => map.MapFrom(vm => vm.Terminated));

            CreateMap<Message, MessageModel>()
                .ForMember(m => m.UserId, map => map.MapFrom(vm => vm.UserTender.User.UserId))
                .ForMember(m => m.TenderId, map => map.MapFrom(vm => vm.UserTender.Tender.TenderId));
        }
    }
}