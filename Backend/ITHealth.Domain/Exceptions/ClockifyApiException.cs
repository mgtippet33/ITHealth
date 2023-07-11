using ITHealth.Domain.Resources;

namespace ITHealth.Domain.Exceptions
{
    public class ClockifyApiException : Exception
    {
        public ClockifyApiException() : base(ExceptionResource.Clockify_API)
        {
        }
    }
}
