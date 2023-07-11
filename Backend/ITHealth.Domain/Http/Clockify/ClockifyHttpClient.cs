using ITHealth.Domain.Exceptions;
using ITHealth.Domain.Services.Clockify.Models;
using System.Net.Http.Headers;

namespace ITHealth.Domain.Http.Clockify
{
    public class ClockifyHttpClient : BaseHttpClient, IClockifyHttpClient
    {
        public ClockifyHttpClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory.CreateClient("Clockify"))
        {
        }

        public async Task<List<ClockifyWorkspace>> ListWorkspacesAsync(string token)
        {
            var url = "workspaces";
            var header = new RequestHeader("X-Api-Key", token);
            return await ExecuteGetRequestAsync<List<ClockifyWorkspace>, ClockifyApiException>(url, header);
        }

        public async Task<List<ClockifyUser>> ListUsersOnWorkspaceAsync(string token, string workspaceId)
        {
            var url = $"workspaces/{workspaceId}/users";
            var header = new RequestHeader("X-Api-Key", token);
            return await ExecuteGetRequestAsync<List<ClockifyUser>, ClockifyApiException>(url, header);
        }

        public async Task<List<ClockifyProject>> ListProjectsOnWorkplaceAsync(string token, string workspaceId)
        {
            var url = $"workspaces/{workspaceId}/projects";
            var header = new RequestHeader("X-Api-Key", token);
            return await ExecuteGetRequestAsync<List<ClockifyProject>, ClockifyApiException>(url, header);
        }

        public async Task<List<ClockifyTask>> ListWorkspaceTasksAsync(string token, string projectId, string workspaceId)
        {
            var url = $"workspaces/{workspaceId}/projects/{projectId}/tasks";
            var header = new RequestHeader("X-Api-Key", token);
            return await ExecuteGetRequestAsync<List<ClockifyTask>, ClockifyApiException>(url, header);
        }

        public async Task<List<ClockifyTimeEntry>> ListUserTimeEntriesAsync(string token, string workspaceId, string userId)
        {
            var url = $"workspaces/{workspaceId}/user/{userId}/time-entries";
            var header = new RequestHeader("X-Api-Key", token);
            return await ExecuteGetRequestAsync<List<ClockifyTimeEntry>, ClockifyApiException>(url, header);
        }

        public async Task<List<ClockifyTimeEntry>> ListUserTimeEntriesByTaskAsync(string token, string workspaceId, string userId, string taskId)
        {
            var url = $"workspaces/{workspaceId}/user/{userId}/time-entries/?task={taskId}";
            var header = new RequestHeader("X-Api-Key", token);
            return await ExecuteGetRequestAsync<List<ClockifyTimeEntry>, ClockifyApiException>(url, header);
        }
    }
}
