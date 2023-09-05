using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.Identity.Web;

namespace DotnetTemplateWithDotnetIdentity.Api.AppStart
{
    public static class AuthenticationService
    {
        public static IServiceCollection AddAppAuthenticationSchemes(this IServiceCollection services, IConfiguration config)
        {
            services.AddAzureAd(config);
            services.AddFormsAuthentication(config);
            services.AddWindowsAuthentication(config);
            return services;
        }
        private static void AddFormsAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:44459";
                    options.Audience = "DotnetTemplateWithDotnetIdentity.ApiAPI";
                })
                .AddIdentityServerJwt();
        }
        private static void AddAzureAd(this IServiceCollection services, IConfiguration config)
        {
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApi(config.GetSection("AzureAd"));
        }
        private static void AddWindowsAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
        }
    }
}
