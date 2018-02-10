using AutoMapper;
using IGG.TenderPortal.Model;
using OldModels = IGG.TenderPortal.WebService.Models;

namespace IGG.TenderPortal.WebService.Mapping
{
    public class DomainToOldModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToOldModelMappings";

        public DomainToOldModelMappingProfile()
        {
            CreateMap<Tender, OldModels.Project>()
               .ForMember(m => m.ID, map => map.MapFrom(vm => vm.TenderId))
               .ForMember(m => m.name, map => map.MapFrom(vm => vm.ProjectName))
               .ForMember(m => m.description, map => map.MapFrom(vm => vm.Description))
               .ForMember(m => m.place, map => map.MapFrom(vm => vm.Place))
               .ForMember(m => m.clientCreated, map => map.MapFrom(vm => vm.ClientCreated))
               .ForMember(m => m.Client, opt => opt.Ignore())
               .ForMember(m => m.IGGperson, map => map.MapFrom(vm => vm.IGGperson))
               .ForMember(m => m.canUpload, map => map.MapFrom(vm => vm.CanUpload))
               .ForMember(m => m.status, map => map.MapFrom(vm => vm.Status))
               .ForMember(m => m.submisionDate, map => map.MapFrom(vm => vm.SubmissionDate))
               .ForMember(m => m.photo, opt => opt.Ignore())
               .ForMember(m => m.onFrontPage, opt => opt.Ignore())
               .ForMember(m => m.timeCompleted, map => map.MapFrom(vm => vm.TimeCompleted))
               .ForMember(m => m.timeCreated, map => map.MapFrom(vm => vm.TimeCreated))
               .ForMember(m => m.timeCompleted, map => map.MapFrom(vm => vm.TimeCompleted))
               .ForMember(m => m.photoThumbnail, map => map.MapFrom(vm => vm.PhotoThumbnail))
               .ForMember(m => m.timeOpenVault, map => map.MapFrom(vm => vm.TimeOpenVault));

            CreateMap<Milestone, OldModels.Milestone>()
               .ForMember(m => m.ID, map => map.MapFrom(vm => vm.MilestoneId))
               .ForMember(m => m.IDproject, map => map.MapFrom(vm => vm.Tender.TenderId))
               .ForMember(m => m.name, map => map.MapFrom(vm => vm.Name))
               .ForMember(m => m.time, map => map.MapFrom(vm => vm.WillBeAt))
               .ForMember(m => m.notificationTo, opt => opt.Ignore())
               .ForMember(m => m.notificationDate, opt => opt.Ignore())
               .ForMember(m => m.visibleFor, opt => opt.Ignore());

            CreateMap<User, OldModels.User>()
                .ForMember(m => m.ID, map => map.MapFrom(vm => vm.UserId));

            CreateMap<UserTender, OldModels.UsersProject>()
                .ForMember(m => m.ID, map => map.MapFrom(vm => vm.UserTenderId))
                .ForMember(m => m.beganWithProject, map => map.MapFrom(vm => vm.BeganWithProject))
                .ForMember(m => m.endedWithProject, map => map.MapFrom(vm => vm.EndedWithProject))
                .ForMember(m => m.userType, map => map.MapFrom(vm => vm.UserType))
                .ForMember(m => m.statusOnProject, map => map.MapFrom(vm => vm.StatusOnProject))
                .ForMember(m => m.Project, opt => opt.Ignore())
                .ForMember(m => m.User, opt => opt.Ignore())
                .ForMember(m => m.IDproject, map => map.MapFrom(vm => vm.Tender.TenderId))
                .ForMember(m => m.IDuser, map => map.MapFrom(vm => vm.User.UserId));

            CreateMap<CheckListItem, OldModels.Checklist>()
                .ForMember(m => m.ID, map => map.MapFrom(vm => vm.CheckListItemId))
                .ForMember(m => m.projectID, map => map.MapFrom(vm => vm.Tender.TenderId))
                .ForMember(m => m.item, map => map.MapFrom(vm => vm.Value));
        }
    }
}