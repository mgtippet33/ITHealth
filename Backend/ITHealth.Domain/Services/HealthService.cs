using AutoMapper;
using ITHealth.Data.Entities;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Health;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Data.Enums;

namespace ITHealth.Domain.Services
{
    public class HealthService : BaseApplicationService, IHealthService
    {
        private readonly ITestService _testService;
        private readonly IJiraService _jiraService;
        private readonly IHoursService _hoursService;

        public HealthService(
            AppDbContext appDbContext,
            UserManager<User> userManager,
            ITestService testService,
            IJiraService jiraService,
            IServiceProvider serviceProvider,
            IMapper mapper, IHoursService hoursService) : base(userManager, appDbContext, serviceProvider, mapper)
        {
            _testService = testService;
            _jiraService = jiraService;
            _hoursService = hoursService;
        }

        public async Task<ListStressLevelCommandModelResult> GetUserStressLevelsAsync(GetUserStressLevelsCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);
            var responseCommand = _mapper.Map<ListStressLevelCommandModel>(command);

            if (validationResult != null && validationResult.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(command.CurrentUserEmail);

                for (var date = command.StartDate; date <= command.EndDate; date = date.AddDays(1))
                {
                    var stressLevel = await CalculateStressLevelAsync(user.Id, date);

                    responseCommand.StressLevels.Add(new StressLevelCommandModel
                    {
                        Date = date,
                        StressLevel = stressLevel == 0.0 && responseCommand.StressLevels.Any()
                                      ? responseCommand.StressLevels.Last().StressLevel
                                      : stressLevel,
                    });
                }
            }

            return new ListStressLevelCommandModelResult(responseCommand, validationResult);
        }

        public async Task<HealthCommandModelResult> CreateHealthRecordAsync(CreateHealthCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);

            if (validationResult != null && validationResult.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(command.CurrentUserEmail);
                var health = _mapper.Map<Health>(command);
                health.User = user;

                await _appDbContext.Healths.AddAsync(health);
                await _appDbContext.SaveChangesAsync();

                command.Id = health.Id;
                command.UserId = user.Id;
            }

            return new HealthCommandModelResult(command, validationResult);
        }

        public async Task<BurnoutCommandModelResult> GetBurnoutInformationAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var validationResult = ValidateUserToken(user);
            var responseCommand = new BurnoutResponseCommandModel();

            if (validationResult != null &&  validationResult.IsValid)
            {
                responseCommand.HasOvertime = await HasOverTimeOnWeekWeekAsync(email);
                responseCommand.HasStress = await HasUserStressInWorkWeekAsync(user.Id);
                responseCommand.HasBadSleep = await HasUserBadSleepInWorkWeekAsync(user.Id);
                responseCommand.HasLowEfficiency = await _jiraService.HasUserLowEfficiencyAsync(user.Email);
                responseCommand.HasBadTestResults = await _testService.HasUserBadTestResultsAsync(user.Id);
            }

            return new BurnoutCommandModelResult(responseCommand, validationResult);
        }

        public async Task<double> GetAvarageSleepTimeByDateTimeRange(int userId, DateTime start, DateTime end)
        {
            var healthRecords = await _appDbContext.Healths
                .Where(x => x.UserID == userId && x.CreatedAt.Date >= start && x.CreatedAt.Date <= end)
                .ToListAsync();

            return healthRecords.Any() ? healthRecords.Average(x => x.SleepTime) : 0;
        }

        private async Task<bool> HasUserStressInWorkWeekAsync(int userId)
        {
            const double CriticalStressLevel = 1.12;

            var datesOfWeek = GetCurrentWorkWeekDates();
            var stressLevels = new List<double>();
            
            foreach (var date in datesOfWeek)
            {
                var stressLevel = await CalculateStressLevelAsync(userId, date);
                stressLevels.Add(stressLevel);
            }

            return stressLevels.Any(x => x > CriticalStressLevel);
        }

        private async Task<bool> HasUserBadSleepInWorkWeekAsync(int userId)
        {
            const double MinimumAvarageSleepTime = 5.0;

            var startOfWeek = GetStartDateOfWeek();
            var userAvarageSleepTime = await GetAvarageSleepTimeByDateTimeRange(userId, startOfWeek, DateTime.Today);

            return userAvarageSleepTime > 0 && userAvarageSleepTime < MinimumAvarageSleepTime;
        }

        private async Task<double> CalculateStressLevelAsync(int userId, DateTime healthRecordDate)
        {
            const double menNormalizationFactor = 0.8244e-4;
            const double womenNormalizationFactor = 0.9357e-4;

            double stressLevel = 0.0;
            var healthRecords = await _appDbContext.Healths.Where(x => x.UserID == userId && x.CreatedAt.Date == healthRecordDate).ToListAsync();

            if (healthRecords.Any())
            {
                var gender = (await _userManager.FindByIdAsync(userId.ToString())).Gender;
                var weight = healthRecords.OrderByDescending(x => x.CreatedAt).First().Weight;
                var pulseAVG = healthRecords.Average(x => x.Pulse);
                var pressureAVG = healthRecords.Average(x => x.Pressure);

                stressLevel = pulseAVG * pressureAVG * Math.Pow(weight, 1 / 3);
                stressLevel *= gender == Gender.Male ? menNormalizationFactor : womenNormalizationFactor;
            }

            return stressLevel;
        }
        
        private async Task<bool> HasOverTimeOnWeekWeekAsync(string email)
        {
            const double MaximumAvarageWorkingTime = 42;

            var hours = await _hoursService.GetLoggedHoursOnAWeekAsync(email);

            if (!hours.HasValue)
                return false;

            return hours > MaximumAvarageWorkingTime;
        }
    }
}