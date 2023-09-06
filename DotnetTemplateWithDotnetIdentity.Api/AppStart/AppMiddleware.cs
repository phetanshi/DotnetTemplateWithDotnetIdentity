using DotnetTemplateWithDotnetIdentity.Api.Areas.Identity.Data;
using DotnetTemplateWithDotnetIdentity.Api.Constants;
using DotnetTemplateWithDotnetIdentity.Api.Identity;
using DotnetTemplateWithDotnetIdentity.Data.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetTemplateWithDotnetIdentity.Api.AppStart
{
    public static class AppMiddleware
    {
        public static void AddMiddlewares(this WebApplication app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "DotnetTemplateWithDotnetIdentity";
                c.OAuthClientId(configuration["SwaggerAzureAd:ClientId"]);
                c.OAuthUsePkce();
                c.OAuthScopeSeparator(" "); //It is requried only when there are more then one scope exists. in our case we have only one scope and not requried

            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(AppConstants.ALLOW_SPECIFIC_ORG);

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.MapControllers();

            app.MapRazorPages();

            app.Services.SeedIdentity();
            app.Services.SeedAppDatabase();

            app.UseMiddleware(typeof(AppLogHandlerMiddleware));

            app.MapFallbackToFile("index.html");
            app.Run();

            
        }
    }
}
