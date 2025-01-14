using System.ComponentModel.DataAnnotations;
namespace Api.Gateway.Domain.Identity.Commands
{
    public class UserLoginCommand 
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
