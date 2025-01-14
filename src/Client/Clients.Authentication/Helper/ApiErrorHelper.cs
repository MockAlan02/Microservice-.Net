using Api.Gateway.Domain.Identity.Responses;
using Clients.Authentication.ApiResponse;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.Common.ApiResponse.ApiResponse;
using System.Net;
using System.Text.Json;

namespace Clients.Authentication.Helper
{
    public static class ApiErrorHelper
    {
        public static async Task AddErrorsToModelState(this HttpResponseMessage response, ModelStateDictionary modelState)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ApiResponse<IdentityAccess>? apiResponse = JsonSerializer.Deserialize<ApiResponse<IdentityAccess>>(errorContent, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

                if (apiResponse?.Errors != null)
                {
                    foreach (var error in apiResponse.Errors)
                    {
                            modelState.AddModelError(string.Empty,error);   
                    }
                }
            }
            else if (!response.IsSuccessStatusCode)
            {
                modelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
            }
        }
    }
}
