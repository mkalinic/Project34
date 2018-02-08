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
               .ForMember(m => m.Milestones, opt => opt.Ignore())
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
        }
    }
}