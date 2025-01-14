namespace Service.Common.ApiResponse.ApiResponse
{
    public class ApiResponse<T> where T:class, new()
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; } = new();

        public List<string> Errors { get; set; } = [];

    }
}
