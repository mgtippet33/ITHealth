using AutoMapper;
using ITHealth.Data;
using ITHealth.Data.Entities;
using ITHealth.Data.Enums;
using ITHealth.Domain.Contracts.Commands.Company;
using ITHealth.Domain.Contracts.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Services
{
    public class CompanyService : BaseApplicationService, ICompanyService
    {
        public CompanyService(
            AppDbContext appDbContext, 
            UserManager<User> userManager, 
            IServiceProvider serviceProvider, 
            IMapper mapper) : base(userManager, appDbContext, serviceProvider, mapper)
        {
        }

        public async Task<CompanyCommandModelResult> GetCompanyAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var role = await GetUserRoleAsync(user);

            var validationResult = ValidateUserToken(user);
            var companyCommand = new CompanyCommandModel();

            if (validationResult != null && validationResult.IsValid && role == Role.GlobalAdministrator.ToString())
            {
                var company = await _appDbContext.Companies.SingleOrDefaultAsync(x => x.Id == user.CompanyId);
                companyCommand = _mapper.Map<CompanyCommandModel>(company);
            }

            return new CompanyCommandModelResult(companyCommand, validationResult);
        }

        public async Task<ListUserCompanyCommandModelResult> GetCompanyAdministrators(string currentUserEmail)
        {
            var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);
            var role = await GetUserRoleAsync(currentUser);

            var validationResult = ValidateUserToken(currentUser);
            var userCompanyList = new List<UserCompanyResponseCommandModel>();

            if (validationResult != null && validationResult.IsValid && role == Role.GlobalAdministrator.ToString())
            {
                var company = await _appDbContext.Companies.Include(e => e.Users).SingleOrDefaultAsync(x => x.Id == currentUser.CompanyId);
                var users = company?.Users.ToList() ?? new List<User>();

                foreach (var companyUser in users)
                {
                    if (await GetUserRoleAsync(companyUser) == Role.Administrator.ToString())
                    {
                        userCompanyList.Add(_mapper.Map<UserCompanyResponseCommandModel>(companyUser));
                    }
                }
            }

            return new ListUserCompanyCommandModelResult(
                userCompanyList.OrderByDescending(x => x.IsActive).ToList(),
                validationResult);
        }

        public async Task<ListUserCompanyCommandModelResult> GetCompanyUsersExcludingCurrentTeamUsersAsync(GetCompanyUsersCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);
            var responseCommand = new List<UserCompanyResponseCommandModel>();

            if (validationResult != null && validationResult.IsValid)
            {
                var currentUser = await _userManager.FindByEmailAsync(command.CurrentUserEmail);
                var currentUserRole = await GetUserRoleAsync(currentUser);

                var company = await _appDbContext.Companies
                    .Include(e => e.Users)
                    .ThenInclude(e => e.Teams)
                    .SingleAsync(x => x.Id == currentUser.CompanyId);
                var users = company.Users
                    .Where(x => x.IsActive && !x.Teams.Any(t => t.Id == command.ExcludingTeamId))
                    .ToList();

                foreach (var companyUser in users)
                {
                    var companyUserRole = await GetUserRoleAsync(companyUser);

                    if (currentUserRole == Role.GlobalAdministrator.ToString() && companyUserRole == Role.Administrator.ToString()
                        || companyUserRole == Role.User.ToString())
                    {
                        var userCompanyResponseModel = _mapper.Map<UserCompanyResponseCommandModel>(companyUser);
                        userCompanyResponseModel.Role = companyUserRole;

                        responseCommand.Add(userCompanyResponseModel);
                    }
                }
            }

            return new ListUserCompanyCommandModelResult(responseCommand, validationResult);
        }

        public async Task<CompanyCommandModelResult> CreateCompanyAsync(CompanyCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);

            if (validationResult != null && validationResult.IsValid)
            {
                var entity = _mapper.Map<Company>(command);
                entity.InviteCode = GenerateInviteCode();

                await _appDbContext.Companies.AddAsync(entity);
                await _appDbContext.SaveChangesAsync();

                var user = await _userManager.FindByEmailAsync(command.CurrentUserEmail);
                user.CompanyId = entity.Id;

                await _userManager.UpdateAsync(user);

               command = _mapper.Map<CompanyCommandModel>(entity);
            }

            return new CompanyCommandModelResult(command, validationResult);
        }

        public async Task<CompanyCommandModelResult> UpdateCompanyAsync(UpdateCompanyCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);
            var responseCommand = new CompanyCommandModel();

            if (validationResult != null && validationResult.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(command.CurrentUserEmail);
                var company = await _appDbContext.Companies.SingleAsync(x => x.Id == user.CompanyId);
                company.Name = command.Name;

                _appDbContext.Companies.Update(company);
                await _appDbContext.SaveChangesAsync();

                responseCommand = _mapper.Map<CompanyCommandModel>(company);
            }

            return new CompanyCommandModelResult(responseCommand, validationResult);
        }

        public async Task<AcceptUserCompanyCommandModelResult> AcceptUserToCompany(AcceptUserCompanyCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);
            var responseCommand = new AcceptUserCompanyResponseCommandModel { IsSuccessful = validationResult?.IsValid ?? false };

            if (validationResult != null && validationResult.IsValid)
            {
                var company = await _appDbContext.Companies.SingleAsync(x => x.InviteCode == command.InviteCode);

                var user = await _userManager.FindByEmailAsync(command.CurrentUserEmail);
                user.CompanyId = company.Id;
                user.Company = company;

                responseCommand.IsSuccessful = (await _userManager.UpdateAsync(user)).Succeeded;
            }

            return new AcceptUserCompanyCommandModelResult(responseCommand, validationResult);
        }

        private string GenerateInviteCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }
    }
}
