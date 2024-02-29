﻿using messaging_service.models.domain;

namespace messaging_service.Repository.Interfaces
{
    public interface IWorkspaceRepository
    {
        public Task<bool> CreateWorkspaceAsync(Workspace workspace);
        public Task<bool> DeleteWorkspaceAsync(int workspaceId);
        public Task<bool> UpdateWorkspaceAsync(Workspace workspace);
        public Task<Workspace> GetWorkspaceAsync(int workspaceId);
    }
}
