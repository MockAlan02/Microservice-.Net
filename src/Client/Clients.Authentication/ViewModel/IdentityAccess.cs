namespace Api.Gateway.Domain.Identity.Responses
{
    public class IdentityAccess
    {
        public bool Succeeded { get; set; }
        public string AccessToken { get; set; } = string.Empty;
    }
}
