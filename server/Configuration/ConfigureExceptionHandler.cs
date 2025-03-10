﻿using Microsoft.AspNetCore.Diagnostics;
using NLog;
using System.Net;

namespace server.Configuration
{
    public static class ConfigureExceptionHandler
    {
        public static void ConfigureExceptions(this WebApplication app)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseExceptionHandler(
                    options =>
                    {
                        options.Run(
                            async context =>
                            {
                                //context.Response.StatusCode = (int)(HttpStatusCode.InternalServerError);
                                var ex = context.Features.Get<IExceptionHandlerFeature>();
                                if (ex != null)
                                {
                                    await context.Response.WriteAsync(ex.Error.Message);
                                }
                            }
                            );
                    });
            }
        }
    }
}
