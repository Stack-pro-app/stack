using AutoMapper;
using messaging_service.models.domain;
using messaging_service.models.dto;

namespace messaging_service.MappingProfiles
{
    public class WorkspaceProfile:Profile
    {
        public WorkspaceProfile() {
            CreateMap<Workspace, WorkspaceDto>();
            CreateMap<WorkspaceDto, Workspace>();

        }
    }
}
