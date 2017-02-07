using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace AccessControl
{
    public class Config
    {
        internal class Clients
        {
            public static IEnumerable<Client> Get()
            {
                return new List<Client> {
                    new Client {
                        ClientId = "oauthClient",
                        ClientName = "Example Client Credentials Client Application",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets = new List<Secret> {
                            new Secret("superSecretPassword".Sha256())},
                        AllowedScopes = new List<string> {"customAPI.read"}
                    },
                    new Client {
                        ClientId = "openIdConnectClient",
                        ClientName = "Example Implicit Client Application",
                        AllowedGrantTypes = GrantTypes.Implicit,
                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.Email,
                            "role",
                            "customAPI"
                        },
                        RedirectUris = new List<string> {"http://localhost:50975/signin-oidc"},
                        PostLogoutRedirectUris = new List<string> { "http://localhost:50975/" }
                    },
                    new Client
                        {
                            ClientId = "js",
                            ClientName = "JavaScript Client",
                            AllowedGrantTypes = GrantTypes.Implicit,
                            AllowAccessTokensViaBrowser = true,

                            RedirectUris =           { "http://localhost:5003/callback.html" },
                            PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                            AllowedCorsOrigins =     { "http://localhost:5003" },

                            AllowedScopes =
                            {
                                IdentityServerConstants.StandardScopes.OpenId,
                                IdentityServerConstants.StandardScopes.Profile,
                                IdentityServerConstants.StandardScopes.Email,
                                "dataEventRecords",
                                "dataeventrecordsscope",
                                "securedFiles",
                                "securedfilesscope",
                            }
                        }
                };
            }
        }
  
        internal class Resources
        {
            public static IEnumerable<IdentityResource> GetIdentityResources()
            {
                return new List<IdentityResource> {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
                    new IdentityResources.Email(),
                    new IdentityResource {
                        Name = "role",
                        UserClaims = new List<string> {"role"}
                    },
                    new IdentityResource("dataeventrecordsscope",new []{ "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin" , "dataEventRecords.user" } ),
                    new IdentityResource("securedfilesscope",new []{ "role", "admin", "user", "securedFiles", "securedFiles.admin", "securedFiles.user"} )
                };
            }

            public static IEnumerable<ApiResource> GetApiResources()
            {
                return new List<ApiResource> {
                    new ApiResource {
                        Name = "customAPI",
                        DisplayName = "Custom API",
                        Description = "Custom API Access",
                        UserClaims = new List<string> {"role"},
                        ApiSecrets = new List<Secret> {new Secret("scopeSecret".Sha256())},
                        Scopes = new List<Scope> {
                            new Scope("customAPI.read"),
                            new Scope("customAPI.write")
                        }
                      },
                    new ApiResource("dataEventRecords")
                    {
                        ApiSecrets =
                        {
                            new Secret("dataEventRecordsSecret".Sha256())
                        },
                        Scopes =
                        {
                            new Scope
                            {
                                Name = "dataeventrecordsscope",
                                DisplayName = "Scope for the dataEventRecords ApiResource"
                            }
                        },
                        UserClaims = { "role", "admin", "user", "dataEventRecords" }
                    },
                    new ApiResource("securedFiles")
                    {
                        ApiSecrets =
                        {
                            new Secret("securedFilesSecret".Sha256())
                        },
                        Scopes =
                        {
                            new Scope
                            {
                                Name = "securedfilesscope",
                                DisplayName = "Scope for the securedFiles ApiResource"
                            }
                        },
                        UserClaims = { "role", "admin", "user", "securedFiles" }
                    }

                };
            }
        }

        internal class Users
        {
            public static List<TestUser> Get()
            {
                return new List<TestUser> {
            new TestUser {
                SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                Username = "scott",
                Password = "password",
                Claims = new List<Claim> {
                    new Claim(JwtClaimTypes.Email, "scott@scottbrady91.com"),
                    new Claim(JwtClaimTypes.Role, "admin")
                }
            }
        };
            }
        }
    }
}
