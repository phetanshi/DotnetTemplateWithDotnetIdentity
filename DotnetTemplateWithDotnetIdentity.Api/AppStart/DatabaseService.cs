﻿using DotnetTemplateWithDotnetIdentity.Data;

namespace DotnetTemplateWithDotnetIdentity.Api.AppStart
{
    public static class DatabaseService
    {
        public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDatabase(builder.Configuration);
            return builder;
        }
    }
}
