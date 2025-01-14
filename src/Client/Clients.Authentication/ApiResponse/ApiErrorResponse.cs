namespace Clients.Authentication.ApiResponse
{
    public class ApiErrorResponse
    {
        public string Title { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public Dictionary<string, string[]> Errors { get; set; } = [];
    }
}
