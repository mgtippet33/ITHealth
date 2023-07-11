namespace ITHealth.Domain.Exceptions;

public class TrelloSecretsException : Exception
{
    public TrelloSecretsException() : base("Can't find trello secrets")
    {
    }
}