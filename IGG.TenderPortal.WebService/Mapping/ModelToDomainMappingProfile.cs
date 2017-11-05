using AutoMapper;
using IGG.TenderPortal.DtoModel.Models;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.WebService.Mapping
{
    public class ModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => "ModelToDomainMappings";

        public ModelToDomainMappingProfile()
        {
            CreateMap<EmployeeModel, Person>()
                .ForMember(g => g.PersonId, opt => opt.Ignore())
                .ForMember(g => g.FirstName, map => map.MapFrom(vm => vm.FirstName))
                .ForMember(g => g.LastName, map => map.MapFrom(vm => vm.LastName))
                .ForMember(g => g.BirhDate, map => map.MapFrom(vm => vm.BirhDate));

            CreateMap<EmployeeModel, Employee>()
                .ForMember(g => g.EmployeeId, opt => opt.Ignore())
                .ForMember(g => g.EmployeeNumber, map => map.MapFrom(vm => vm.EmployeeNumber))
                .ForMember(g => g.EmployeeDate, map => map.MapFrom(vm => vm.EmployeeDate))
                .ForMember(g => g.Terminated, map => map.MapFrom(vm => vm.Terminated));

            CreateMap<MessageModel, Message>()
                .ForMember(m => m.UserTender, opt => opt.Ignore());
        }
    }
}