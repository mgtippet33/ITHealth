using AutoMapper;
using FluentValidation.Results;
using ITHealth.Data;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Subscribe;
using ITHealth.Domain.Contracts.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ITHealth.Domain.Services
{
    public class SubscribeService : BaseApplicationService, ISubscribeService
    {
        public SubscribeService(
            UserManager<User> userManager,
            AppDbContext appDbContext,
            IServiceProvider serviceProvider,
            IMapper mapper) : base(userManager, appDbContext, serviceProvider, mapper)
        {
        }

        public async Task<SubscribeCommandModelResult> GetLastSubscribeAsync(string currentUserEmail)
        {
            var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);
            var validationResult = ValidateUserToken(currentUser);
            var responseModel = new SubscribeResponseCommandModel();

            if (validationResult != null && validationResult.IsValid)
            {
                var subscribe = await _appDbContext.SubscribeHistories
                    .Where(x => x.CompanyId == currentUser.CompanyId.Value)
                    .OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefaultAsync();

                responseModel = new SubscribeResponseCommandModel
                {
                    Price = subscribe?.Price ?? 0,
                    StartDate = subscribe?.CreatedAt ?? null,
                    EndDate = subscribe?.EndDate,
                    IsPaid = subscribe?.EndDate >= DateTime.Today.Date
                };
            }

            return new SubscribeCommandModelResult(responseModel, validationResult);
        }

        public async Task<SaveSubscribeCommandModelResult> SaveSubscribeAsync(SaveSubscribeCommandModel command)
        {
            const string SuccessStatus = "success";
            const string SubscribedStatus = "subscribed";

            var liqpayModel = GetLiqPayInformation(command.Data);
            var responseModel = new SaveSubscribeResponseCommandModel { IsSuccessful = false };

            if (liqpayModel.Status == SuccessStatus || liqpayModel.Status == SubscribedStatus)
            {
                var companyId = Convert.ToInt32(liqpayModel.CompanyId);
                var company = await _appDbContext.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
                
                if (company != null)
                {
                    var subscribe = new SubscribeHistory
                    {
                        Price = liqpayModel.Amount,
                        CompanyId = companyId,
                        Company = company,
                        EndDate = liqpayModel.Amount == 30 ? DateTime.Today.AddYears(1) : DateTime.Today.AddMonths(1)
                    };

                    await _appDbContext.SubscribeHistories.AddAsync(subscribe);
                    await _appDbContext.SaveChangesAsync();

                    responseModel.IsSuccessful = true;
                }

            }

            return new SaveSubscribeCommandModelResult(responseModel, new ValidationResult());
        }

        private LiqPayModel GetLiqPayInformation(string data)
        {
            var bytes = Convert.FromBase64String(data);
            var liqPayJson = System.Text.Encoding.UTF8.GetString(bytes);

            return JsonSerializer.Deserialize<LiqPayModel>(liqPayJson) ?? new LiqPayModel();
        }
    }
}
