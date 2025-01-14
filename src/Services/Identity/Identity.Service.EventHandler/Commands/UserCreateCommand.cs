using MediatR;
using Microsoft.AspNetCore.Identity;
using Service.Common.ApiResponse.ApiResponse;
using System.ComponentModel.DataAnnotations;

namespace Identity.Service.EventHandler.Commands
{
    public class UserCreateCommand : IRequest<ApiResponse<IdentityResult>>
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
