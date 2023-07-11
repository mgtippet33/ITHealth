using AutoMapper;
using FluentValidation.Results;
using ITHealth.Data;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Trello.Key;
using ITHealth.Domain.Contracts.Commands.Trello.Tasks;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Domain.Exceptions;
using ITHealth.Domain.Http.Trello;
using ITHealth.Domain.Services.Trello.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Services.Trello;

public class TrelloService : BaseApplicationService, ITrelloService
{
    private readonly ITrelloHttpClient _trelloHttpClient;

    public TrelloService(ITrelloHttpClient trelloHttpClient, AppDbContext appDbContext, IMapper mapper,
        UserManager<User> userManager, IServiceProvider serviceProvider) : base(userManager, appDbContext, serviceProvider, mapper)
    {
        _trelloHttpClient = trelloHttpClient;
    }

    public async Task<SetAppKeyCommandModelResult> SetAppKeyAsync(SetAppKeyCommandModel command) 
    {
        var validationResult = await ValidateCommandAsync(command);
        if (validationResult != null && validationResult.IsValid)
        {
            var secrets = _mapper.Map<TrelloWorkspaceSecrets>(command);
            secrets.Team = null;
            
            var doesEntityExist = await _appDbContext.TrelloWorkspaceSecrets.AnyAsync(x => x.TeamId == secrets.TeamId);
            if (doesEntityExist)
            {
                _appDbContext.Update(secrets);
            }
            else
            {
                await _appDbContext.TrelloWorkspaceSecrets.AddAsync(secrets);
            }
            await _appDbContext.SaveChangesAsync();
        }

        return new SetAppKeyCommandModelResult(command, validationResult);
    }

    public async Task<GetCurrentUserTasksInProgressCommandModelResult> OpenedUserTaskAsync(string token, string email)
    {
        var currentCards = (await UserCardsList(token, email)).Where(c => !c.Closed);

        return new GetCurrentUserTasksInProgressCommandModelResult(
            new GetCurrentUserTasksInProgressCommandModel()
            {
                TrelloCards = currentCards.ToList()
            }, new ValidationResult());
    }

    private async Task<IEnumerable<TrelloCard>> UserCardsList(string token, string email)
    {
        var appKey = (await GetSecretsAsync(email)).FirstOrDefault();
        if (appKey == null)
            throw new TrelloSecretsException();
        
        var user = await _trelloHttpClient.GetCurrentUserAsync(appKey.AppKey, token);
        return await _trelloHttpClient.ListUserCardsAsync(appKey.AppKey, token, user.Id);
    }
    
    public async Task<List<TrelloWorkspaceSecrets>> GetSecretsAsync(string email)
    {
        var userTeams = _appDbContext.Teams.Include(x => x.Users).Where(x => x.Users.Select(x => x.Email).Contains(email)).Select(x => x.Id).ToList();
        return await _appDbContext.TrelloWorkspaceSecrets.Where(x => userTeams.Contains(x.TeamId)).ToListAsync();
    }

    // not sure, maybe will be necessary in the future. Decided to live while front isn't ready
    
    /* private async Task<IEnumerable<TrelloCard>> UserCardsListExtended()
    {
        var user = await _trelloHttpClient.GetCurrentUserAsync(AppKey, AppToken);
        var boards = await _trelloHttpClient.ListUserBoardsAsync(AppKey, AppToken);

        IEnumerable<TrelloCard> userCards = new List<TrelloCard>();
        foreach (var board in boards)
        {
            var cards = await _trelloHttpClient.ListBoardCardsAsync(AppKey, AppToken, board.ShortLink);
            userCards = userCards.Concat(cards.Where(c => c.IdMembers.Contains(user.Id)));
        }

        await UserCardsList();

        return userCards;
    }*/
}