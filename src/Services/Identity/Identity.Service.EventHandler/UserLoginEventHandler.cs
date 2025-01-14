using Identity.Domain;
using Identity.Service.EventHandler.Commands;
using Identity.Service.EventHandler.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Identity.Service.EventHandler.Helper;
using Service.Common.ApiResponse.ApiResponse;


namespace Identity.Service.EventHandler
{
    public class UserLoginEventHandler(ILogger<UserLoginEventHandler> logger,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration) : IRequestHandler<UserLoginCommand, ApiResponse<IdentityAccess>>
    {
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILogger<UserLoginEventHandler> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        public async Task<ApiResponse<IdentityAccess>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting user login process for Email: {Email}", request.Email);

            var result = new ApiResponse<IdentityAccess>();

            // Find user by email
            _logger.LogInformation("Attempting to find user with Email: {Email}", request.Email);
            var user = await _userManager.FindByNameAsync(request.Email);
           
            if (user is null)
            {
                _logger.LogWarning("Login failed: No user found with Email: {Email}", request.Email);
                result.Message = "Login failed";
                result.Errors.Add("Email or password is wrong");
                result.Data!.Succeeded = false;
                return result;
            }

            // Validate password
            _logger.LogInformation("Validating password for user with Email: {Email}", request.Email);
            var response = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            
            if (!response.Succeeded)
            {
                _logger.LogWarning("Login failed: Invalid password for user with Email: {Email}", request.Email);
              
                result.Message = "Login failed";
                result.Errors.Add("Email or password is wrong");
                result.Data!.Succeeded = false;
                return result;
            }

            // Generate token
            _logger.LogInformation("Generating authentication token for user with Email: {Email}", request.Email);
            await TokenHelper.GenerateToken(_configuration, _userManager, user, result.Data!);
            result.Data!.Succeeded = true;
           
            if (result.Data!.Succeeded)
            {
                _logger.LogInformation("Login successful for user with Email: {Email}", request.Email);
            }
            else
            {
                _logger.LogWarning("Token generation failed for user with Email: {Email}", request.Email);
            }

            return result;
        }
    }
}
