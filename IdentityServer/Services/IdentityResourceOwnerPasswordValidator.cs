using Duende.IdentityServer.Validation;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            // Find user by email
            var user = await _userManager.FindByEmailAsync(context.UserName);
            
            if (user == null)
            {
                context.Result = new GrantValidationResult(
                    Duende.IdentityServer.Models.TokenRequestErrors.InvalidGrant, 
                    "Invalid username or password");
                return;
            }

            // Validate password
            var passwordCheck = await _userManager.CheckPasswordAsync(user, context.Password);
            
            if (!passwordCheck)
            {
                context.Result = new GrantValidationResult(
                    Duende.IdentityServer.Models.TokenRequestErrors.InvalidGrant, 
                    "Invalid username or password");
                return;
            }

            // Get user claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            
            // Add standard identity claims
            var claims = new List<Claim>
            {
                new Claim("sub", user.Id),
                new Claim("name", user.UserName ?? ""),
                new Claim("email", user.Email ?? ""),
                new Claim("email_verified", user.EmailConfirmed.ToString().ToLower()),
                new Claim("username", user.UserName ?? "")
            };

            // Add custom claims
            claims.Add(new Claim("country", "Turkey"));
            claims.Add(new Claim("subscriptionlevel", "premium"));
            
            // Add user roles as claims
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim("role", role));
            }

            // Add existing user claims
            claims.AddRange(userClaims);

            // If validation succeeds, create the result with user claims
            context.Result = new GrantValidationResult(
                user.Id.ToString(),
                "password",
                claims);
        }
    }
}
