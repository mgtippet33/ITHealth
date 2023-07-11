using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using ITHealth.Data;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ITHealth.Domain.Services
{
    public class BaseApplicationService
    {
        protected readonly UserManager<User> _userManager;

        protected AppDbContext _appDbContext;

        protected readonly IServiceProvider _serviceProvider;

        protected readonly IMapper _mapper;

        public BaseApplicationService(
            UserManager<User> userManager,
            AppDbContext appDbContext,
            IServiceProvider serviceProvider,
            IMapper mapper)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        protected async Task<ValidationResult?> ValidateCommandAsync<TCommand>(TCommand command)
            where TCommand : BaseCommandModel
        {
            IValidator<TCommand> validator = _serviceProvider.GetService<IValidator<TCommand>>();

            return command != null && validator != null ? await validator.ValidateAsync(command) : null;
        }

        protected ValidationResult ValidateNullData<TData>(TData data, string propertyName, string errorMessage)
        {
            var failure = new ValidationFailure(propertyName, errorMessage);

            return data == null ? new ValidationResult(new List<ValidationFailure> { failure }) : new ValidationResult();
        }

        protected ValidationResult ValidateUserToken(User user)
        {
            return ValidateNullData(user, "Token", Resources.Validator.CommonResource.Token_Expired);
        }

        protected async Task<string> GetUserRoleAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return roles.FirstOrDefault().ToString();
        }

        protected DateTime GetStartDateOfWeek()
        {
            var today = DateTime.Today;
            var delta = DayOfWeek.Monday - today.DayOfWeek;
            return today.AddDays(delta);
        }

        protected ICollection<DateTime> GetCurrentWorkWeekDates()
        {
            var startOfWeek = GetStartDateOfWeek();
            var datesOfWeek = Enumerable.Range(0, 5)
                .Select(i => startOfWeek.AddDays(i))
                .ToList();

            return datesOfWeek;
        }
    }
}
