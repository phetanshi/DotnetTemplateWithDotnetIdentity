using DotnetTemplateWithDotnetIdentity.Api.Constants;

namespace DotnetTemplateWithDotnetIdentity.Api.AppStart
{
    public static class AppServices
    {
        public static WebApplicationBuilder AddAppServices(this WebApplicationBuilder builder)
        {
            builder.AddAppIdentity();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: AppConstants.ALLOW_SPECIFIC_ORG,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000", "https://localhost:44459")
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
                      });
            });
            builder.Services.AddControllers();
            builder.Services.AddApplicationObjects();
            builder.Services.AddAppAuthenticationSchemes(builder.Configuration);
            builder.Services.AddAppAuthorization(builder.Configuration);
            builder.Services.AddSwaggerWithAutherization(builder.Configuration);

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            return builder;
        }
    }
}
