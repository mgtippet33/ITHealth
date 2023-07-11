using ITHealth.Domain.Resources;

namespace ITHealth.Domain.Exceptions;

public class TrelloApiException : Exception
{
    public TrelloApiException() : base(ExceptionResource.Trello_API)
    {
    }
}