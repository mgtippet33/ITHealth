using AutoMapper;
using ITHealth.Domain.Contracts.Commands.Account;
using ITHealth.Domain.Contracts.Commands.Account.GenerateResetPasswordToken;
using ITHealth.Domain.Contracts.Commands.Account.Login;
using ITHealth.Domain.Contracts.Commands.Account.ResetPassword;
using ITHealth.Domain.Contracts.Commands.Account.SignUp;
using ITHealth.Domain.Contracts.Commands.Trello.Key;
using ITHealth.Domain.Contracts.Commands.Trello.Tasks;
using ITHealth.Domain.Contracts.Commands.Account.Update;
using ITHealth.Domain.Contracts.Commands.Answer;
using ITHealth.Domain.Contracts.Commands.Company;
using ITHealth.Domain.Contracts.Commands.Team;
using ITHealth.Domain.Contracts.Commands.UserTeam;
using ITHealth.Web.API.Models.Account;
using ITHealth.Web.API.Models.Account.GenerateResetPasswordToken;
using ITHealth.Web.API.Models.Account.Login;
using ITHealth.Web.API.Models.Account.ResetPassword;
using ITHealth.Web.API.Models.Account.SignUp;
using ITHealth.Web.API.Models.Trello.Tasks;
using ITHealth.Web.API.Models.Account.Update;
using ITHealth.Web.API.Models.Company;
using ITHealth.Web.API.Models.Team;
using ITHealth.Web.API.Models.UserTeam;
using ITHealth.Domain.Contracts.Commands.Health;
using ITHealth.Domain.Contracts.Commands.Subquestion;
using ITHealth.Domain.Contracts.Commands.Question;
using ITHealth.Domain.Contracts.Commands.Test;
using ITHealth.Web.API.Models.Answer;
using ITHealth.Web.API.Models.Health;
using ITHealth.Web.API.Models.Subquestion;
using ITHealth.Web.API.Models.Question;
using ITHealth.Web.API.Models.Test;
using AnswerRequestModel = ITHealth.Web.API.Models.Answer.AnswerRequestModel;
using ITHealth.Domain.Contracts.Commands.Clockify.Key;
using ITHealth.Web.API.Models.Clockify.Secrets;
using ITHealth.Web.API.Models.Clockify.Tracker;
using ITHealth.Domain.Contracts.Commands.Clockify.Tracker;
using ITHealth.Domain.Contracts.Commands.Jira;
using ITHealth.Web.API.Models.Jira;
using JiraIssueResponse = ITHealth.Domain.Contracts.Commands.Jira.JiraIssueResponse;
using ITHealth.Web.API.Models.Account.InviteUser;
using ITHealth.Domain.Contracts.Commands.Account.InviteUser;
using ITHealth.Domain.Contracts.Commands.Account.ChangeUserRole;
using ITHealth.Web.API.Models.Account.ChangeUserRole;
using ITHealth.Web.API.Models.Account.DeleteUser;
using ITHealth.Domain.Contracts.Commands.Account.DeleteUser;
using ITHealth.Domain.Contracts.Commands.WorkingHours;
using ITHealth.Web.API.Models.Trello.Key;
using ITHealth.Web.API.Models.Subscribe;
using ITHealth.Domain.Contracts.Commands.Subscribe;
using ITHealth.Web.API.Models.WorkTime;

namespace ITHealth.Web.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapAccountModelsToCommands();
            MapAccountCommandsToModels();
            MapCompanyModelsToCommands();
            MapCompanyCommandsToModels();
            MapTeamModelsToCommands();
            MapTeamCommandsToModels();
            MapTrelloCommandsToModels();
            MapHealthModelsToCommands();
            MapHealthCommandsToModels();
            MapAnswerModelsToCommands();
            MapAnswerCommandsToModels();
            MapTestModelsToCommands();
            MapTestCommandsToModels();
            MapQuestionModelsToCommands();
            MapQuestionCommandsToModels();
            MapClockifyModelsToCommands();
            MapClockifyCommandsToModels();
            MapJiraModelsToCommands();
            MapJiraCommandsToModels();
            MapSubscribeModelsToCommands();
            MapSubscribeCommandsToModels();
        }

        private void MapAccountModelsToCommands()
        {
            CreateMap<LoginUserRequestModel, LoginUserCommandModel>();
            CreateMap<SignUpUserRequestModel, SignUpUserCommandModel>();
            CreateMap<UpdateUserRequestModel, UpdateUserCommandModel>();
            CreateMap<ResetPasswordRequestModel, ResetPasswordCommandModel>();
            CreateMap<GenerateResetPasswordTokenRequestModel, GenerateResetPasswordTokenCommandModel>();
            CreateMap<InviteUserRequestModel, InviteUserCommandModel>();
            CreateMap<ChangeUserRoleRequestModel, ChangeUserRoleCommandModel>();
        }

        private void MapAccountCommandsToModels()
        {
            CreateMap<LoginUserResponseCommandModel, LoginUserResponseModel>();
            CreateMap<SignUpUserCommandModel, UserResponseModel>();
            CreateMap<UserCommandModel, UserResponseModel>();
            CreateMap<UpdateUserCommandModel, UserResponseModel>();
            CreateMap<ResetPasswordCommandResponseModel, ResetPasswordResponseModel>();
            CreateMap<GenerateResetPasswordTokenCommandResponseModel, GenerateResetPasswordTokenResponseModel>();
            CreateMap<InviteUserResponseCommandModel, InviteUserResponseModel>();
            CreateMap<ChangeUserRoleResponseCommandModel, ChangeUserRoleResponseModel>();
            CreateMap<DeleteUserResponseCommandModel, DeleteUserResponseModel>();
        }

        private void MapCompanyModelsToCommands()
        {
            CreateMap<CompanyRequestModel, CompanyCommandModel>();
        }

        private void MapCompanyCommandsToModels()
        {
            CreateMap<CompanyCommandModel, CompanyResponseModel>();
            CreateMap<UserCompanyResponseCommandModel, UserCompanyResponseModel>();
        }

        private void MapTeamModelsToCommands()
        {
            CreateMap<TeamRequestModel, CreateTeamCommandModel>();
            CreateMap<UpdateTeamRequestModel, UpdateTeamCommandModel>();
            CreateMap<UserTeamRequestModel, UserTeamCommandModel>();
        }

        private void MapTeamCommandsToModels()
        {
            CreateMap<TeamCommandModel, TeamResponseModel>();
        }

        private void MapTrelloCommandsToModels()
        {
            CreateMap<GetCurrentUserTasksInProgressCommandModel,
                    GetCurrentUserTasksInProgressResponseModel>()
                .ForMember(m => m.TrelloCards, dest => dest.MapFrom(opt => opt.TrelloCards));
            
            CreateMap<SetAppKeyCommandModel, TrelloSetSecretsRequestModel>().ReverseMap();
        }

        private void MapHealthModelsToCommands()
        {
            CreateMap<HealthRequestModel, CreateHealthCommandModel>();
            CreateMap<GetUserStressLevelsRequestModel, GetUserStressLevelsCommandModel>();
        }

        private void MapHealthCommandsToModels()
        {
            CreateMap<HealthCommandModel, HealthResponseModel>();
            CreateMap<StressLevelCommandModel, StressLevelResponseModel>();
            CreateMap<ListStressLevelCommandModel, ListStressLevelResponseModel>();
            CreateMap<BurnoutResponseCommandModel, BurnoutResponseModel>();
            CreateMap<ListWorkingTimeCommandModel, GetWorkTimeStatisticsResponseModel>();
            CreateMap<WorkingTimeCommandModel, WorkingTimeResponse>();
            CreateMap<ListTeamWorkingTimeCommandModel, GetTeamWorkTimeStatisticsResponseModel>();
            CreateMap<TeamWorkingTimeCommandModel, TeamWorkingTimeResponse>();
        }
        
        private void MapAnswerModelsToCommands()
        {
            CreateMap<AnswerRequestModel, AnswerCommandModel>().ReverseMap();
            CreateMap<UpdateAnswerRequestModel, UpdateAnswerCommandModel>();
        }

        private void MapAnswerCommandsToModels()
        {
            CreateMap<AnswerCommandModel, AnswerResponseModel>();
            CreateMap<SubquestionCommandModel, SubquestionResponseModel>();
        }
        
        private void MapTestCommandsToModels()
        {
            CreateMap<TestCommandModel, TestResponseModel>();
            CreateMap<TestCommandModel, TestPassedInfoResponseModel>();
            CreateMap<TestResultCommandModel, TestResultResponseModel>();
            CreateMap<UserTestInfoCommandModel, UserTestInfoResponseModel>();
            CreateMap<TestAnswerResponseCommandModel, TestAnswerResponseModel>();
            CreateMap<QuestionUserAnswersResponseCommandModel, QuestionUserAnswersResponseModel>();
            CreateMap<UserAnswersResponseCommandModel, UserAnswersResponseModel>();
            CreateMap<TestPassedUserResponseCommandModel, TestPassedUserResponseModel>();
            CreateMap<UserTestingStatisticResponseCommandModel, UserTestingStatisticResponseModel>();
            CreateMap<UnverifiedUserTestInfoCommandModel, UnverifiedUserTestResponseModel>();
            CreateMap<TestPaginationResponseCommandModel<TestCommandModel>, TestPaginationResponseModel<TestResponseModel>>();
            CreateMap<TestPaginationResponseCommandModel<UnverifiedUserTestInfoCommandModel>, TestPaginationResponseModel<UnverifiedUserTestResponseModel>>();
            CreateMap<TestPaginationResponseCommandModel<UserTestInfoCommandModel>, TestPaginationResponseModel<UserTestInfoResponseModel>>();
        }

        private void MapTestModelsToCommands()
        {
            CreateMap<UserAnswerRequestModel, UserAnswerCommandModel>();
            CreateMap<UserSubAnswerRequestModel, UserSubAnswerCommandModel>();
            CreateMap<SendRequestModel, SendTestCommandModel>();
            CreateMap<TestRequestModel, CreateTestCommandModel>();
            CreateMap<UpdateTestRequestModel, UpdateTestCommandModel>();

            CreateMap<UsersTestDeadlineRequestModel, CreateUsersTestDeadlineCommandModel>();
            CreateMap<TeamTestDeadlineRequestModel, CreateTeamTestDeadlineCommandModel>();

            CreateMap<TestPaginationRequestModel, GetUserTestListCommandModel>();

            CreateMap<SendOpenUserAnswersAssesmentRequestModel, SendOpenUserAnswersAssesmentCommandModel>();

            CreateMap<TestPassedUsersRequestModel, TestPassedUsersCommandModel>();
            CreateMap<UsersTestingStatisticsRequestModel, UsersTestingStatisticsCommandModel>();
            CreateMap<UserTestingStatisticsRequestModel, UserTestingStatisticsCommandModel>();

            CreateMap<TestPaginationRequestModel, GetTestsCommandModel>();
            CreateMap<TestPaginationRequestModel, GetUserTestListCommandModel>();

            CreateMap<SubquestionRequestModel, SubquestionCommandModel>().ReverseMap();
        }

        private void MapQuestionModelsToCommands()
        {
            CreateMap<QuestionRequestModel, QuestionCommandModel>().ReverseMap();
        }
        
        private void MapQuestionCommandsToModels()
        {
            CreateMap<QuestionCommandModel, QuestionResponseModel>();
        }

        private void MapClockifyModelsToCommands()
        {
            CreateMap<ClockifySetSecretsRequestModel, SetClockifySecretsCommandModel>();
        }

        private void MapClockifyCommandsToModels()
        {
            CreateMap<ClockifyTimeEntriesCommandModel, ClockifyTimeEntriesResponseModel>();
        }
        
        private void MapJiraModelsToCommands()
        {
            CreateMap<JiraSetSecretsRequestModel,  SetJiraSecretsCommandModel>();
            CreateMap<GetEfficiencyStatisticsRequestModel, GetUserEfficiencyCommandModel>();
        }

        private void MapJiraCommandsToModels()
        {
            CreateMap<JiraCurrentUserTasksInProgressCommandModel, JiraCurrentUserTasksInProgressResponseModel>();
            CreateMap<JiraIssueResponse, Web.API.Models.Jira.JiraIssueResponse>();
            CreateMap<GetUserEfficiencyCommandModel, ListEfficiencyResponseModel>();
            CreateMap<ListEfficiencyCommandModel, ListEfficiencyResponseModel>();
            CreateMap<EfficiencyCommandModel, EfficiencyResponseModel>();
        }

        private void MapSubscribeModelsToCommands()
        {
            CreateMap<LiqPayRequestModel, SaveSubscribeCommandModel>();
        }

        private void MapSubscribeCommandsToModels()
        {
            CreateMap<SubscribeResponseCommandModel, SubscribeResponseModel>();
        }
    }
}