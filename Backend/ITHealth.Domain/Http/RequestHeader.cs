namespace ITHealth.Domain.Http
{
    public class RequestHeader
    {
        public string Name { get; set; } = null!;

        public string Value { get; set; } = null!;

        public RequestHeader(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
