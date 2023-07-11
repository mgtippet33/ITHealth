using ITHealth.Domain.Contracts.Commands.Subscribe;

namespace ITHealth.Domain.Contracts.Interfaces
{
    public interface ISubscribeService
    {
        Task<SubscribeCommandModelResult> GetLastSubscribeAsync(string currentUserEmail);

        Task<SaveSubscribeCommandModelResult> SaveSubscribeAsync(SaveSubscribeCommandModel command);
    }
}
