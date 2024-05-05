using AutoMapper;
using messaging_service.models.domain;
using messaging_service.models.dto.Detailed;
using messaging_service.models.dto.Minimal;
using messaging_service.models.dto.Requests;

namespace messaging_service.MappingProfiles
{
    public class WorkspaceProfile:Profile
    {
        public WorkspaceProfile() {
            CreateMap<Workspace, WorkspaceDetailDto>();
            CreateMap<WorkspaceDetailDto, Workspace>();
            CreateMap<WorkspaceMinimalDto, Workspace>();
            CreateMap<Workspace, WorkspaceMinimalDto>();
            CreateMap<Workspace,WorkspaceRequestDto>();
            CreateMap<WorkspaceRequestDto,Workspace>();
        }
    }
}
