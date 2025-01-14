using Identity.Domain;
using Identity.Service.EventHandler.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Service.Common.ApiResponse.ApiResponse;

namespace Identity.Service.EventHandler
{
    public class UserCreateEventHandler(UserManager<ApplicationUser> userManager, ILogger<UserCreateEventHandler> logger) : IRequestHandler<UserCreateCommand, ApiResponse<IdentityResult>>
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILogger<UserCreateEventHandler> _logger = logger;

        public async Task<ApiResponse<IdentityResult>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting user creation process.");
            var response = new ApiResponse<IdentityResult>();
            // Log de entrada del comando
            _logger.LogInformation("Received UserCreateCommand with Email: {Email}", request.Email);

            _logger.LogInformation("Mapping UserCreateCommand to ApplicationUser.");
            var entry = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email
            };
            _logger.LogInformation("ApplicationUser mapped successfully. Details: FirstName = {FirstName}, LastName = {LastName}, Email = {Email}.",
                entry.FirstName, entry.LastName, entry.Email);

            _logger.LogInformation("Attempting to save the user to the database.");
            var result = await _userManager.CreateAsync(entry, request.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created successfully. User ID: {UserId}", entry.Id);

            }
            else
            {
                _logger.LogWarning("Failed to create user. Errors: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                response.Message = "Failed to create user";
                response.Errors.Add("Failed to create user");
            }

            return new ApiResponse<IdentityResult>()
            {
                Data = result
            };
        }
    }
}
