using AutoMapper;
using IGG.TenderPortal.DtoModel;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.WebService.Mapping
{
    public class DomainToModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToModelMappings";

        public DomainToModelMappingProfile()
        {                    

            CreateMap<Message, MessageModel>()
                .ForMember(m => m.UserId, map => map.MapFrom(vm => vm.UserTender.User.UserId))
                .ForMember(m => m.TenderId, map => map.MapFrom(vm => vm.UserTender.Tender.TenderId));

            CreateMap<Tender, TenderModel>()
                .ForMember(m => m.Status, map => map.MapFrom(vm => vm.Status.ToString()));

            CreateMap<User, UserModel>()
                .ForMember(m => m.Type, map => map.MapFrom(vm => vm.Type.ToString()));

            CreateMap<UserTender, UserTenderModel>()
                .ForMember(m => m.UserId, map => map.MapFrom(vm => vm.User.UserId))
                .ForMember(m => m.TenderId, map => map.MapFrom(vm => vm.Tender.TenderId));

            CreateMap<Milestone, MilestoneModel>()
               .ForMember(m => m.TenderId, map => map.MapFrom(vm => vm.Tender.TenderId));
        }
    }
}