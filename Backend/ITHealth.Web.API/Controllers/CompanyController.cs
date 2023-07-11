using AutoMapper;
using ITHealth.Domain.Contracts.Commands.Company;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Web.API.Infrastructure.Filters;
using ITHealth.Web.API.Models.Account;
using ITHealth.Web.API.Models.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITHealth.Web.API.Controllers
{
    [Localization]
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class CompanyController : BaseApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IAccountService _accountService;

        public CompanyController(ICompanyService companyService, IAccountService accountService, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(httpContextAccessor, mapper)
        {
            _companyService = companyService;
            _accountService = accountService;
        }

        [Authorize(Roles = "GlobalAdministrator")]
        [HttpGet]
        public async Task<CompanyResultModel> Get()
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!;

            var commandResult = await _companyService.GetCompanyAsync(currentUserEmail);

            var readModel = commandResult.IsValid
                ? _mapper.Map<CompanyResponseModel>(commandResult.Data)
                : new CompanyResponseModel();

            return new CompanyResultModel(readModel, commandResult.ValidationResult);
        }

        [Authorize(Roles = "GlobalAdministrator")]
        [HttpGet]
        public async Task<ListUserCompanyResultModel> GetAdministrators()
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!;

            var commandResult = await _companyService.GetCompanyAdministrators(currentUserEmail);

            var readModels = commandResult.IsValid
                ? _mapper.Map<List<UserCompanyResponseModel>>(commandResult.Data)
                : new List<UserCompanyResponseModel>();

            return new ListUserCompanyResultModel(readModels, commandResult.ValidationResult);
        }

        [Authorize(Roles = "Administrator,GlobalAdministrator")]
        [HttpGet]
        public async Task<ListUserCompanyResultModel> GetCompanyUsersExcludingCurrentTeamUsers([FromQuery] int teamId)
        {
            var command = new GetCompanyUsersCommandModel
            {
                ExcludingTeamId = teamId,
                CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!
            };

            var commandResult = await _companyService.GetCompanyUsersExcludingCurrentTeamUsersAsync(command);

            var readModels = commandResult.IsValid
                ? _mapper.Map<List<UserCompanyResponseModel>>(commandResult.Data)
                : new List<UserCompanyResponseModel>();

            return new ListUserCompanyResultModel(readModels, commandResult.ValidationResult);
        }

        [Authorize(Roles = "GlobalAdministrator")]
        [HttpPost]
        public async Task<CompanyResultModel> Create([FromBody] CompanyRequestModel formModel)
        {
            var command = CreateCommand<CompanyCommandModel, CompanyRequestModel>(formModel);
            command.CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            var commandResult = await _companyService.CreateCompanyAsync(command);

            var readModel = commandResult.IsValid
                ? _mapper.Map<CompanyResponseModel>(commandResult.Data)
                : new CompanyResponseModel();

            return new CompanyResultModel(readModel, commandResult.ValidationResult);
        }

        [Authorize(Roles = "GlobalAdministrator")]
        [HttpPut]
        public async Task<CompanyResultModel> Update([FromBody] CompanyRequestModel formModel)
        {
            var command = CreateCommand<UpdateCompanyCommandModel, CompanyRequestModel>(formModel);
            command.CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            var commandResult = await _companyService.UpdateCompanyAsync(command);

            var readModel = commandResult.IsValid
                ? _mapper.Map<CompanyResponseModel>(commandResult.Data)
                : new CompanyResponseModel();

            return new CompanyResultModel(readModel, commandResult.ValidationResult);
        }

        [HttpPost]
        public async Task<AcceptUserCompanyResultModel> AcceptUserToCompany([FromQuery] string inviteCode)
        {
            var command = new AcceptUserCompanyCommandModel
            {
                InviteCode = inviteCode,
                CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!
            };

            var commandResult = await _companyService.AcceptUserToCompany(command);
            var profileResult = await _accountService.GetProfileAsync(command.CurrentUserEmail);

            commandResult.ValidationResult.Errors.AddRange(profileResult.ValidationResult.Errors);

            var readModel = commandResult.IsValid && profileResult.IsValid
                ? _mapper.Map<UserResponseModel>(profileResult.Data)
                : new UserResponseModel();

            return new AcceptUserCompanyResultModel(readModel, commandResult.ValidationResult);
        }
    }
}
