using AutoMapper;
using ITHealth.Data;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Account;
using ITHealth.Domain.Contracts.Commands.Team;
using ITHealth.Domain.Contracts.Commands.UserTeam;
using ITHealth.Domain.Contracts.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Services
{
    public class TeamService : BaseApplicationService, ITeamService
    {
        public TeamService(
            AppDbContext appDbContext, 
            UserManager<User> userManager, 
            IServiceProvider serviceProvider, 
            IMapper mapper) : base(userManager, appDbContext, serviceProvider, mapper)
        {
        }

        public async Task<TeamListCommandModelResult> GetUserTeamsAsync(string email)
        {
            var user = await _appDbContext.Users.Include(e => e.Teams).FirstOrDefaultAsync(x => x.Email == email);
            var validationResult = ValidateUserToken(user);

            if (validationResult != null && validationResult.IsValid)
            {
                var teams = user.Teams.ToList();
                var command = _mapper.Map<List<TeamCommandModel>>(teams);

                return new TeamListCommandModelResult(command, validationResult);
            }

            return new TeamListCommandModelResult(validationResult: validationResult);
        }

        public async Task<TeamCommandModelResult> GetTeamAsync(BaseTeamCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);

            if (validationResult == null || !validationResult.IsValid)
            {
                return new TeamCommandModelResult(validationResult: validationResult);
            }

            var team = await _appDbContext.Teams.FirstOrDefaultAsync(x => x.Id == command.Id);

            var teamCommand = _mapper.Map<TeamCommandModel>(team);

            return new TeamCommandModelResult(teamCommand, validationResult);
        }

        public async Task<TeamListCommandModelResult> GetCompanyTeamsAsync(GetCompanyTeamsCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);
            var responseCommand = new List<TeamCommandModel>();

            if (validationResult != null && validationResult.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(command.CurrentUserEmail);
                var teams = await _appDbContext.Teams.Where(x => x.CompanyId == user.CompanyId).ToListAsync();
                responseCommand = _mapper.Map<List<TeamCommandModel>>(teams);
            }

            return new TeamListCommandModelResult(responseCommand, validationResult);
        }

        public async Task<TeamCommandModelResult> CreateTeamAsync(CreateTeamCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);

            if (validationResult != null && validationResult.IsValid) 
            {
                var user = await _userManager.FindByEmailAsync(command.CurrentUserEmail);

                var company = await _appDbContext.Companies.SingleAsync(x => x.Id == user.CompanyId);
                var team = _mapper.Map<Team>(command);
                team.Company = company;

                await _appDbContext.Teams.AddAsync(team);
                await _appDbContext.SaveChangesAsync();

                command.Id = team.Id;
            }

            return new TeamCommandModelResult(command, validationResult);
        }

        public async Task<TeamCommandModelResult> UpdateTeamAsync(UpdateTeamCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);

            if (validationResult != null && validationResult.IsValid)
            {
                var team = await _appDbContext.Teams
                    .Include(e => e.Company)
                    .Include(e => e.Users)
                    .SingleAsync(x => x.Id == command.Id);
                var updatedTeam = _mapper.Map(command, team);

                _appDbContext.Teams.Update(updatedTeam);
                await _appDbContext.SaveChangesAsync();

                command.Id = updatedTeam.Id;
            }

            return new TeamCommandModelResult(command, validationResult);
        }

        public async Task<TeamCommandModelResult> DeleteTeamAsync(BaseTeamCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);

            if (validationResult != null && validationResult.IsValid)
            {
                var team = await _appDbContext.Teams.SingleAsync(x => x.Id == command.Id);

                _appDbContext.Teams.Remove(team);
                await _appDbContext.SaveChangesAsync();
            }

            return new TeamCommandModelResult(validationResult: validationResult);
        }

        public async Task<UserTeamListCommandModelResult> GetUsersInTeamAsync(BaseUserTeamCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);

            if (validationResult == null || !validationResult.IsValid)
            {
                return new UserTeamListCommandModelResult(validationResult: validationResult);
            }

            var team = await _appDbContext.Teams
                .Include(e => e.Users)
                .SingleAsync(x => x.Id == command.TeamId);
            var users = team.Users.Where(x => x.IsActive).ToList();

            var userTeamListCommand = await GetUserCommandList(users);

            return new UserTeamListCommandModelResult(userTeamListCommand, validationResult);
        }

        public async Task<UserTeamCommandModelResult> InsertUserToTeamAsync(InsertUserTeamCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);

            if (validationResult != null && validationResult.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(command.UserEmail);
                var team = await _appDbContext.Teams
                    .Include(e => e.Users)
                    .SingleAsync(x => x.Id == command.TeamId);
                team.Users.Add(user);

                _appDbContext.Teams.Update(team);
                await _appDbContext.SaveChangesAsync();
            }

            return new UserTeamCommandModelResult(command, validationResult);
        }

        public async Task<UserTeamCommandModelResult> RemoveUserFromTeamAsync(UserTeamCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);

            if (validationResult != null && validationResult.IsValid)
            {
                var team = await _appDbContext.Teams
                    .Include(e => e.Users)
                    .SingleAsync(x => x.Id == command.TeamId);
                team.Users.RemoveWhere(x => x.Email == command.UserEmail);

                _appDbContext.Teams.Update(team);
                await _appDbContext.SaveChangesAsync();
            }

            return new UserTeamCommandModelResult(command, validationResult);
        }

        private async Task<List<UserCommandModel>> GetUserCommandList(List<User> users)
        {
            var userCommandList = new List<UserCommandModel>();

            foreach (var user in users)
            {
                var userCommand = _mapper.Map<UserCommandModel>(user);
                userCommand.Role = await GetUserRoleAsync(user);

                userCommandList.Add(userCommand);
            }

            return userCommandList;
        }
    }
}
