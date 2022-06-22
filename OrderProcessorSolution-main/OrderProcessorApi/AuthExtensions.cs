using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OrderProcessorApi;

public static class AuthExtensions
{
    public static void AddAuthForKeycloak(
            this IServiceCollection services,
            string authority,
            string audience,
            IWebHostEnvironment env
        )
    {
        services.AddAuthentication(options =>
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            if (env.IsDevelopment())
            {
                o.RequireHttpsMetadata = false; // Do not require https during development.
            }

            o.Authority = authority;
            o.Audience = audience;
            o.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
            };
            o.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = c =>
                {
                    c.NoResult();
                    c.Response.StatusCode = 500;
                    c.Response.ContentType = "text/plain";
                    if (env.IsDevelopment())
                    {
                        return c.Response.WriteAsync(c.Exception.ToString());
                    }
                    else
                    {
                        return c.Response.WriteAsync("there was an error in authenticating your request");
                    }
                }
            };
        });
    }


    public static string? GetSub(this ClaimsPrincipal principal)
    {
        return principal.GetClaimFrom("sub");
    }

    public static string? GetPreferredUserName(this ClaimsPrincipal principal)
    {
        return principal.GetClaimFrom("preferred_username");
    }

    public static string? GetClaimFrom(this ClaimsPrincipal principal, string claimType)
    {
        return principal.Claims.Where(c => c.Type == claimType).Select(c => c.Value).FirstOrDefault();
    }
}

