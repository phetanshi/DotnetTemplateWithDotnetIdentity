using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DotnetTemplateWithDotnetIdentity.Api.AppStart
{
    public static class AuthenticationService
    {
        public static IServiceCollection AddAppAuthenticationSchemes(this IServiceCollection services, IConfiguration config)
        {
            //services.AddAzureAd(config);
            services.AddFormsAuthentication(config);
            services.AddWindowsAuthentication(config);
            return services;
        }
        private static void AddFormsAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:44459";
                    options.Audience = "DotnetTemplateWithDotnetIdentity.ApiAPI";
                })
                .AddIdentityServerJwt();
        }
        private static void AddAzureAd(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("FormsAuthenticationScheme", options =>
                {
                    var authority = "https://localhost:44459";

                    options.Audience = "DotnetTemplateWithDotnetIdentity.ApiAPI";
                    options.Authority = authority; // Replace with your first authority URL
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = authority, // Replace with your first authority's issuer
                        ValidAudience = "DotnetTemplateWithDotnetIdentity.ApiAPI", // Replace with your API's audience
                    };

                }).AddMicrosoftIdentityWebApi(config.GetSection("AzureAd"));
        }
        private static void AddWindowsAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
        }
    }
}
