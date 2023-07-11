using AutoMapper;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Account;
using ITHealth.Domain.Contracts.Commands.Account.Login;
using ITHealth.Domain.Contracts.Commands.Account.SignUp;
using ITHealth.Domain.Contracts.Commands.Trello.Key;
using ITHealth.Domain.Contracts.Commands.Account.Update;
using ITHealth.Domain.Contracts.Commands.Answer;
using ITHealth.Domain.Contracts.Commands.Company;
using ITHealth.Domain.Contracts.Commands.Team;
using ITHealth.Domain.Contracts.Commands.Health;
using ITHealth.Domain.Contracts.Commands.Subquestion;
using ITHealth.Domain.Contracts.Commands.Question;
using ITHealth.Domain.Contracts.Commands.Test;
using ITHealth.Domain.Contracts.Commands.Clockify.Key;
using ITHealth.Domain.Contracts.Commands.Account.InviteUser;
using ITHealth.Domain.Contracts.Commands.Jira;
using ITHealth.Domain.Contracts.Commands.Subscribe;

namespace ITHealth.Domain.Mapping
{
    public class CommandsMappingProfile : Profile
    {
        public CommandsMappingProfile()
        {
            MapAccountCommandsToEntities();
            MapAccountEntitiesToCommands();
            MapCompanyCommandsToEntities();
            MapCompanyEntitiesToCommands();
            MapTeamCommandsToEntities();
            MapTeamEntitiesToCommands();
            MapTrelloCommandsToEntities();
            MapHealthCommandsToEntities();
            MapHealthEntitiesToCommands();
            MapAnswerCommandsToEntities();
            MapAnswerEntitiesToCommands();
            MapTestCommandsToEntities();
            MapTestEntitiesToCommands();
            MapClockifyCommandsToEntities();
            MapJiraCommandsToEntities();
            MapSubscribeCommandsToEntities();

            CreateMap<GetUserStressLevelsCommandModel, ListStressLevelCommandModel>();
        }

        private void MapAccountCommandsToEntities()
        {
            CreateMap<LoginUserCommandModel, User>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<SignUpUserCommandModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<UpdateUserCommandModel, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<InviteUserCommandModel, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.InvitedUserEmail))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => string.Empty))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => false));
        }

        private void MapAccountEntitiesToCommands()
        {
            CreateMap<User, UserCommandModel>()
                .ForMember(dest => dest.ConnectedToCompany, opt => opt.MapFrom(src => src.CompanyId.HasValue));

            CreateMap<User, UpdateUserCommandModel>()
                .ForMember(dest => dest.ConnectedToCompany, opt => opt.MapFrom(src => src.CompanyId.HasValue));

            CreateMap<User, UserCompanyResponseCommandModel>();
        }

        private void MapCompanyCommandsToEntities()
        {
            CreateMap<CompanyCommandModel, Company>();
        }

        private void MapCompanyEntitiesToCommands()
        {
            CreateMap<Company, CompanyCommandModel>();
        }

        private void MapTeamCommandsToEntities()
        {
            CreateMap<TeamCommandModel, Team>();
        }

        private void MapTeamEntitiesToCommands()
        {
            CreateMap<Team, TeamCommandModel>();
        }

        private void MapTrelloCommandsToEntities()
        {
            CreateMap<SetAppKeyCommandModel, TrelloWorkspaceSecrets>();
        }

        private void MapHealthCommandsToEntities()
        {
            CreateMap<HealthCommandModel, Health>();
        }

        private void MapHealthEntitiesToCommands()
        {
            CreateMap<Health, HealthCommandModel>();
        }
        
        private void MapAnswerCommandsToEntities()
        {
            CreateMap<AnswerCommandModel, Answer>();
        }

        private void MapAnswerEntitiesToCommands()
        {
            CreateMap<Answer, AnswerCommandModel>();
            CreateMap<Answer, TestAnswerResponseCommandModel>()
                .ForMember(dest => dest.AnswerId, opt => opt.MapFrom(src => src.Id));
        }
        
        private void MapTestCommandsToEntities()
        {
            CreateMap<TestCommandModel, Test>();
            CreateMap<QuestionCommandModel, Question>();
            CreateMap<SubquestionCommandModel, Subquestion>();
            CreateMap<CreateUsersTestDeadlineCommandModel, TestDeadline>();
        }

        private void MapTestEntitiesToCommands()
        {
            CreateMap<Test, TestCommandModel>();
            CreateMap<Question, QuestionCommandModel>();
            CreateMap<Subquestion, SubquestionCommandModel>();
            CreateMap<TestResult, TestResultCommandModel>()
                .ForMember(dest => dest.MaxPoints, opt => opt.MapFrom(src => src.MaxResult));
            CreateMap<TestDeadline, UserTestDeadlineCommandModel>();
            CreateMap<Test, CreateTestCommandModel>();
        }

        private void MapClockifyCommandsToEntities()
        {
            CreateMap<SetClockifySecretsCommandModel, ClockifyWorkspaceSecrets>();
        }
        
        private void MapJiraCommandsToEntities()
        {
            CreateMap<SetJiraSecretsCommandModel, JiraWorkspaceSecrets>()
                .ForMember(m => m.Team, dest => dest.Ignore());

            CreateMap<GetUserEfficiencyCommandModel, ListEfficiencyCommandModel>();
        }

        private void MapSubscribeCommandsToEntities()
        {
            CreateMap<SaveSubscribeCommandModel, SubscribeHistory>();
        }
    }
}