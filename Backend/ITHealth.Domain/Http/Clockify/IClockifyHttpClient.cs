using ITHealth.Domain.Services.Clockify.Models;

namespace ITHealth.Domain.Http.Clockify
{
    public interface IClockifyHttpClient
    {
        Task<List<ClockifyWorkspace>> ListWorkspacesAsync(string token);

        Task<List<ClockifyUser>> ListUsersOnWorkspaceAsync(string token, string workspaceId);

        Task<List<ClockifyProject>> ListProjectsOnWorkplaceAsync(string token, string workspaceId);

        Task<List<ClockifyTask>> ListWorkspaceTasksAsync(string token, string projectId, string workspaceId);

        Task<List<ClockifyTimeEntry>> ListUserTimeEntriesAsync(string token, string workspaceId, string userId);

        Task<List<ClockifyTimeEntry>> ListUserTimeEntriesByTaskAsync(string token, string workspaceId, string userId, string taskId);
    }
}
