using AutoMapper;
using IGG.TenderPortal.Model;
using OldModels = IGG.TenderPortal.WebService.Models;

namespace IGG.TenderPortal.WebService.Mapping
{
    public class OldModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => "ModelToDomainMappings";

        public OldModelToDomainMappingProfile()
        {
            CreateMap<OldModels.Project, Tender>()
               .ForMember(m => m.TenderId, map => map.MapFrom(vm => vm.ID))
               .ForMember(m => m.ProjectName, map => map.MapFrom(vm => vm.name))
               .ForMember(m => m.Description, map => map.MapFrom(vm => vm.description))
               .ForMember(m => m.Place, map => map.MapFrom(vm => vm.place))
               .ForMember(m => m.ClientCreated, map => map.MapFrom(vm => vm.clientCreated))
               .ForMember(m => m.Client, opt => opt.Ignore())
               .ForMember(m => m.IGGperson, map => map.MapFrom(vm => vm.IGGperson))
               .ForMember(m => m.CanUpload, map => map.MapFrom(vm => vm.canUpload))
               .ForMember(m => m.Status, map => map.MapFrom(vm => vm.status))
               .ForMember(m => m.SubmissionDate, map => map.MapFrom(vm => vm.submisionDate))
               .ForMember(m => m.Photo, opt => opt.Ignore())
               .ForMember(m => m.TimeCompleted, map => map.MapFrom(vm => vm.timeCompleted))
               .ForMember(m => m.TimeCreated, map => map.MapFrom(vm => vm.timeCreated))
               .ForMember(m => m.TimeCompleted, map => map.MapFrom(vm => vm.timeCompleted))
               .ForMember(m => m.PhotoThumbnail, map => map.MapFrom(vm => vm.photoThumbnail))
               .ForMember(m => m.TimeOpenVault, map => map.MapFrom(vm => vm.timeOpenVault))
               .ForMember(m => m.UserTenders, map => map.MapFrom(vm => vm.UsersProjects))
               .ForMember(m => m.CheckLists, opt => opt.Ignore())
               .ForMember(m => m.TenderFileBlocks, opt => opt.Ignore());

            CreateMap<OldModels.Milestone, Milestone>()
               .ForMember(m => m.MilestoneId, map => map.MapFrom(vm => vm.ID))
               .ForMember(m => m.Tender, opt => opt.Ignore())
               .ForMember(m => m.Name, map => map.MapFrom(vm => vm.name))
               .ForMember(m => m.WillBeAt, map => map.MapFrom(vm => vm.time))
               .ForMember(m => m.VisibleFor, map => map.MapFrom(vm => vm.visibleFor))
               .ForMember(m => m.NotificationTo, map => map.MapFrom(vm => vm.notificationTo))
               .ForMember(m => m.NotificationDate, map => map.MapFrom(vm => vm.notificationDate));

            CreateMap<OldModels.User, User>()
                .ForMember(m => m.UserId, map => map.MapFrom(vm => vm.ID));

            CreateMap<OldModels.UsersProject, UserTender>()
                .ForMember(m => m.UserTenderId, map => map.MapFrom(vm => vm.ID))
                .ForMember(m => m.BeganWithProject, map => map.MapFrom(vm => vm.beganWithProject))
                .ForMember(m => m.EndedWithProject, map => map.MapFrom(vm => vm.endedWithProject))
                .ForMember(m => m.UserType, map => map.MapFrom(vm => vm.userType))
                .ForMember(m => m.StatusOnProject, map => map.MapFrom(vm => vm.statusOnProject))
                .ForMember(m => m.Tender, opt => opt.Ignore())
                .ForMember(m => m.User, opt => opt.Ignore())
                .ForMember(m => m.Messages, opt => opt.Ignore())
                .ForMember(m => m.UserFiles, opt => opt.Ignore())
                .ForMember(m => m.UserNotifications, opt => opt.Ignore());

            CreateMap<OldModels.Checklist, CheckListItem>()
                 .ForMember(m => m.CheckListItemId, map => map.MapFrom(vm => vm.ID))
                 .ForMember(m => m.Tender, opt => opt.Ignore())
                 .ForMember(m => m.Value, map => map.MapFrom(vm => vm.item));

            CreateMap<OldModels.TextBlock, TenderFileBlock>()
                 .ForMember(m => m.TenderFileBlockId, map => map.MapFrom(vm => vm.ID))
                 .ForMember(m => m.Tender, opt => opt.Ignore())
                 .ForMember(m => m.Text, map => map.MapFrom(vm => vm.text))
                 .ForMember(m => m.Time, map => map.MapFrom(vm => vm.time))
                 .ForMember(m => m.TenderFiles, map => map.MapFrom(vm => vm.Files));

            CreateMap<OldModels.TextBlockFile, TenderFile>()
                 .ForMember(m => m.TenderFileBlock, map => map.MapFrom(vm => vm.ID))
                 .ForMember(m => m.TenderFileBlock, opt => opt.Ignore())
                 .ForMember(m => m.Size, map => map.MapFrom(vm => vm.size))
                 .ForMember(m => m.LocationPath, map => map.MapFrom(vm => vm.file))
                 .ForMember(m => m.DisplayName, map => map.MapFrom(vm => vm.displayName))
                 .ForMember(m => m.DateModified, map => map.MapFrom(vm => vm.dateModified))
                 .ForMember(m => m.DateUploaded, map => map.MapFrom(vm => vm.dateUploaded));

        }
    }
}