using ITHealth.Domain.Contracts.Commands.Team;
using ITHealth.Domain.Contracts.Commands.UserTeam;

namespace ITHealth.Domain.Contracts.Interfaces
{
    public interface ITeamService
    {
        Task<TeamListCommandModelResult> GetUserTeamsAsync(string email);

        Task<TeamCommandModelResult> GetTeamAsync(BaseTeamCommandModel command);

        Task<TeamListCommandModelResult> GetCompanyTeamsAsync(GetCompanyTeamsCommandModel command);

        Task<TeamCommandModelResult> CreateTeamAsync(CreateTeamCommandModel command);

        Task<TeamCommandModelResult> UpdateTeamAsync(UpdateTeamCommandModel command);

        Task<TeamCommandModelResult> DeleteTeamAsync(BaseTeamCommandModel command);

        Task<UserTeamListCommandModelResult> GetUsersInTeamAsync(BaseUserTeamCommandModel command);

        Task<UserTeamCommandModelResult> InsertUserToTeamAsync(InsertUserTeamCommandModel command);

        Task<UserTeamCommandModelResult> RemoveUserFromTeamAsync(UserTeamCommandModel command);
    }
}
