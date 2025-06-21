using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System.ComponentModel;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("catalog", "Catalog API")
            {
                Scopes = { "catalog_fullpermission" }
            },
            new ApiResource("photo", "Photo API")
            {
                Scopes = { "photo_fullpermission" }
            }
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource( name:"roles", displayName:"User roles", userClaims:new List<string> { "role" }),
            new IdentityResource("country", "User country", new List<string> { "country" }),
            new IdentityResource("subscriptionlevel", "Subscription level", new List<string> { "subscriptionlevel" })
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("catalog_fullpermission","Full Permision For Catalog Api"),
            new ApiScope("photo_fullpermission", "Full Permission For Photo Api"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName, "Full Permission For Identity Server Api")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientName = "Asp.Net Core",
                ClientId = "WebClient",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes =
                {
                    "catalog_fullpermission",
                    "photo_fullpermission",
                    IdentityServerConstants.LocalApi.ScopeName
                },
            },
            new Client
            {
                ClientName = "Asp.Net Core",
                ClientId = "WebClientResourceOwner",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowOfflineAccess = true,
                AllowedScopes =
                {
                    "catalog_fullpermission",
                    "photo_fullpermission",
                    "roles",
                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    
                },
                AccessTokenLifetime = 3600, // 1 hour
                RefreshTokenExpiration = TokenExpiration.Absolute,
                AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                RefreshTokenUsage = TokenUsage.ReUse,
            }
           
        };
}
