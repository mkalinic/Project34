using AutoMapper;
using IGG.TenderPortal.DtoModel;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.WebService.Mapping
{
    public class ModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => "ModelToDomainMappings";

        public ModelToDomainMappingProfile()
        {
            CreateMap<MessageModel, Message>()
                .ForMember(m => m.UserTender, opt => opt.Ignore());

            CreateMap<TenderModel, Tender>()
                .ForMember(m => m.TenderId, opt => opt.Ignore())
                .ForMember(m => m.Milestones, opt => opt.Ignore())
                .ForMember(m => m.CheckLists, opt => opt.Ignore())
                .ForMember(m => m.TenderFileBlocks, opt => opt.Ignore())
                .ForMember(m => m.Status, map => map.MapFrom(vm => vm.Status));

            CreateMap<UserModel, Model.User>()
                .ForMember(m => m.UserId, opt => opt.Ignore())
                .ForMember(m => m.Loggings, opt => opt.Ignore())
                .ForMember(m => m.Type, map => map.MapFrom(vm => vm.Type));

            CreateMap<UserTenderModel, UserTender>()
                .ForMember(m => m.UserTenderId, opt => opt.Ignore())
                .ForMember(m => m.UserFiles, opt => opt.Ignore())
                .ForMember(m => m.Messages, opt => opt.Ignore())
                .ForMember(m => m.User, opt => opt.Ignore())
                .ForMember(m => m.Tender, opt => opt.Ignore())
                .ForMember(m => m.UserNotifications, opt => opt.Ignore());

            CreateMap<MilestoneModel, Model.Milestone>()
                .ForMember(m => m.Tender, opt => opt.Ignore())                
                .ForMember(m => m.TenderNotifications, opt => opt.Ignore());
        }
    }
}