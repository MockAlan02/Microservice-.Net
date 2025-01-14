using Identity.Service.EventHandler.Responses;
using MediatR;
using Service.Common.ApiResponse.ApiResponse;
using System.ComponentModel.DataAnnotations;
namespace Identity.Service.EventHandler.Commands
{
    public class UserLoginCommand : IRequest<ApiResponse<IdentityAccess>>
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        [RegularExpression(@"^(?=.*[!@#$%^&*(),.?""{}|<>])[a-zA-Z0-9!@#$%^&*(),.?""{}|<>_]+$",
            ErrorMessage = "The password must contain at least one special character.")]
        public string Password { get; set; } = string.Empty;
    }
}
