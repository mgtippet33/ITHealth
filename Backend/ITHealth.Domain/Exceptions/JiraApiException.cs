using ITHealth.Domain.Resources;

namespace ITHealth.Domain.Exceptions;

public class JiraApiException : Exception
{
    public JiraApiException() : base(ExceptionResource.Jira_API)
    {
    }
}