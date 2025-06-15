using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("catalog", "Catalog API")
            {
                Scopes = { "calalog_fullpermission" }
            },
            new ApiResource("photo", "Photo API")
            {
                Scopes = { "photo_fullpermission" }
            }
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("calalog_fullpermission","Full Permision For Catalog Api"),
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
                    "calalog_fullpermission",
                    "photo_fullpermission",
                    IdentityServerConstants.LocalApi.ScopeName
                },
            }
           
        };
}
