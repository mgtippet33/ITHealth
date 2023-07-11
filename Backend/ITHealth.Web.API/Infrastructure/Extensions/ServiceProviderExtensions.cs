using FluentValidation;
using ITHealth.Domain.Contracts.Commands.Account.ChangeUserRole;
using ITHealth.Domain.Contracts.Commands.Account.GenerateResetPasswordToken;
using ITHealth.Domain.Contracts.Commands.Account.InviteUser;
using ITHealth.Domain.Contracts.Commands.Account.Login;
using ITHealth.Domain.Contracts.Commands.Account.Profile;
using ITHealth.Domain.Contracts.Commands.Account.ResetPassword;
using ITHealth.Domain.Contracts.Commands.Account.SignUp;
using ITHealth.Domain.Contracts.Commands.Account.Update;
using ITHealth.Domain.Contracts.Commands.Answer;
using ITHealth.Domain.Contracts.Commands.Clockify.Key;
using ITHealth.Domain.Contracts.Commands.Clockify.Tracker;
using ITHealth.Domain.Contracts.Commands.Company;
using ITHealth.Domain.Contracts.Commands.Health;
using ITHealth.Domain.Contracts.Commands.Jira;
using ITHealth.Domain.Contracts.Commands.Subquestion;
using ITHealth.Domain.Contracts.Commands.Team;
using ITHealth.Domain.Contracts.Commands.Test;
using ITHealth.Domain.Contracts.Commands.Trello.Key;
using ITHealth.Domain.Contracts.Commands.UserTeam;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Domain.Http.Clockify;
using ITHealth.Domain.Http.Jira;
using ITHealth.Domain.Http.Trello;
using ITHealth.Domain.Services;
using ITHealth.Domain.Services.Clockify;
using ITHealth.Domain.Services.Jira;
using ITHealth.Domain.Services.Trello;
using ITHealth.Domain.Validators.Account;
using ITHealth.Domain.Validators.Answer;
using ITHealth.Domain.Validators.Clockify;
using ITHealth.Domain.Validators.Company;
using ITHealth.Domain.Validators.Health;
using ITHealth.Domain.Validators.Jira;
using ITHealth.Domain.Validators.Subquestion;
using ITHealth.Domain.Validators.Team;
using ITHealth.Domain.Validators.Test;
using ITHealth.Domain.Validators.Trello;
using ITHealth.Domain.Validators.UserTeam;

namespace ITHealth.Web.API.Infrastructure.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            AddCustomServices(services);
            AddAccountValidatorServices(services);
            AddTeamValidatorServices(services);
            AddCompanyValidatorServices(services);
            AddTrelloValidatorServices(services);
            AddHealthValidatorServices(services);
            AddTestValidatorServices(services);
            AddAnswerValidatorServices(services);
            AddClockifyValidatorServices(services);
            AddJiraValidatorServices(services);

            return services;
        }

        private static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ITrelloService, TrelloService>();
            services.AddScoped<IHealthService, HealthService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<ISubquestionService, SubquestionService>();
            services.AddScoped<IClockifyService, ClockifyService>();
            services.AddScoped<IJiraService, JiraService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ISubscribeService, SubscribeService>();
            services.AddScoped<IHoursService, HoursService>();

            services.AddTransient<ITrelloHttpClient, TrelloHttpClient>();
            services.AddTransient<IClockifyHttpClient, ClockifyHttpClient>();
            services.AddTransient<IJiraHttpClient, JiraHttpClient>();

            return services;
        }

        private static IServiceCollection AddAccountValidatorServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<LoginUserCommandModel>, LoginUserCommandValidator>();
            services.AddScoped<IValidator<SignUpUserCommandModel>, SignUpUserCommandValidator>();
            services.AddScoped<IValidator<UpdateUserCommandModel>, UpdateUserCommandValidator>();
            services.AddScoped<IValidator<ResetPasswordCommandModel>, ResetPasswordCommandValidator>();
            services.AddScoped<IValidator<GenerateResetPasswordTokenCommandModel>, GenerateResetPasswordTokenCommandValidator>();
            services.AddScoped<IValidator<InviteUserCommandModel>, InviteUserCommandValidator>();
            services.AddScoped<IValidator<ChangeUserRoleCommandModel>, ChangeUserRoleCommandValidator>();
            services.AddScoped<IValidator<GetUserProfileCommandModel>, GetUserProfileCommandValidator>();

            return services;
        }

        private static IServiceCollection AddCompanyValidatorServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<BaseCompanyCommandModel>, BaseCompanyCommandValidator>();
            services.AddScoped<IValidator<CompanyCommandModel>, CompanyCommandValidator>();
            services.AddScoped<IValidator<UpdateCompanyCommandModel>, UpdateCompanyCommandValidator>();
            services.AddScoped<IValidator<AcceptUserCompanyCommandModel>, AcceptUserCompanyCommandValidator>();
            services.AddScoped<IValidator<GetCompanyUsersCommandModel>, GetCompanyUsersCommandValidator>();

            return services;
        }

        private static IServiceCollection AddTeamValidatorServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateTeamCommandModel>, CreateTeamCommandValidator>();
            services.AddScoped<IValidator<UpdateTeamCommandModel>, UpdateTeamCommandValidator>();
            services.AddScoped<IValidator<BaseTeamCommandModel>, BaseTeamCommandValidator>();
            services.AddScoped<IValidator<BaseUserTeamCommandModel>, BaseUserTeamCommandValidator>();
            services.AddScoped<IValidator<InsertUserTeamCommandModel>, InsertUserTeamCommandValidator>();
            services.AddScoped<IValidator<UserTeamCommandModel>, UserTeamCommandValidator>();
            services.AddScoped<IValidator<GetCompanyTeamsCommandModel>, GetCompanyTeamsCommandValidator>();

            return services;
        }

        private static IServiceCollection AddTrelloValidatorServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<SetAppKeyCommandModel>, SetAppKeyCommandValidator>();

            return services;
        }

        private static IServiceCollection AddHealthValidatorServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<BaseHealthCommandModel>, BaseHealthCommandValidator>();
            services.AddScoped<IValidator<CreateHealthCommandModel>, HealthCommandValidator>();
            services.AddScoped<IValidator<GetUserStressLevelsCommandModel>, GetUserStressLevelsCommandValidator>();

            return services;
        }
        
        private static IServiceCollection AddTestValidatorServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<SendTestCommandModel>, SendTestCommandValidator>();
            services.AddScoped<IValidator<CreateTestCommandModel>, CreateTestCommandValidator>();
            services.AddScoped<IValidator<BaseTestCommandModel>, TestCommandValidator>();
            services.AddScoped<IValidator<UpdateTestCommandModel>, UpdateTestCommandValidator>();
            services.AddScoped<IValidator<CreateUsersTestDeadlineCommandModel>, CreateUsersTestDeadlineCommandValidator>();
            services.AddScoped<IValidator<CreateTeamTestDeadlineCommandModel>, CreateTeamTestDeadlineCommandValidator>();
            services.AddScoped<IValidator<GetUserTestListCommandModel>, GetUserTestListCommandValidator>();
            services.AddScoped<IValidator<GetUserAnswersCommandModel>, GetUserAnswersCommandValidator>();
            services.AddScoped<IValidator<SendOpenUserAnswersAssesmentCommandModel>, SendOpenUserAnswersAssesmentCommandValidator>();
            services.AddScoped<IValidator<TestPassedUsersCommandModel>, TestPassedUsersCommandValidator>();
            services.AddScoped<IValidator<UserTestingStatisticsCommandModel>, UserTestingStatisticsCommandValidator>();
            services.AddScoped<IValidator<UsersTestingStatisticsCommandModel>, UsersTestingStatisticsCommandValidator>();

            return services;
        }
        
        private static IServiceCollection AddAnswerValidatorServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<BaseAnswerCommandModel>, BaseAnswerCommandValidator>();
            services.AddScoped<IValidator<UpdateAnswerCommandModel>, UpdateAnswerCommandValidator>();
            services.AddScoped<IValidator<UpdateSubquestionCommandModel>, UpdateSubquestionCommandValidator>();

            return services;
        }

        private static IServiceCollection AddClockifyValidatorServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<SetClockifySecretsCommandModel>, SetClockifySecretsCommandValidator>();
            services.AddScoped<IValidator<ClockifyTrackerCommandModel>, ClockifyTrackerCommandValidator>();

            return services;
        }
        
        private static IServiceCollection AddJiraValidatorServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<SetJiraSecretsCommandModel>, SetJiraSecretsCommandValidator>();
            services.AddScoped<IValidator<GetUserEfficiencyCommandModel>, GetEfficiencyCommandValidator>();

            return services;
        }
    }
}
