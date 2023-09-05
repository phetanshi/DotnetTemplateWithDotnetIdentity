using DotnetTemplateWithDotnetIdentity.Api.Authorization;
using DotnetTemplateWithDotnetIdentity.Api.Authorization.Admin;
using DotnetTemplateWithDotnetIdentity.Api.Authorization.Default;
using DotnetTemplateWithDotnetIdentity.Api.Authorization.Support;
using DotnetTemplateWithDotnetIdentity.Api.Services;
using DotnetTemplateWithDotnetIdentity.Api.Services.Definitions;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;

namespace DotnetTemplateWithDotnetIdentity.Api.AppStart
{
    public static class ApplicationObjects
    {
        public static IServiceCollection AddApplicationObjects(this IServiceCollection services)
        {
            services.AddServiceDependencies();
            services.AddOthes();
            return services;
        }

        private static void AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAppConfigService, AppConfigService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IRoleMenuService, RoleMenuService>();
            services.AddScoped<IAppLogService, AppLogService>();
        }
        private static void AddOthes(this IServiceCollection services)
        {
            
            services.AddScoped<IAuthorizationHandler, DefaultAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, SupportAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
