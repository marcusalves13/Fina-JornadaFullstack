﻿using Fina.Api.Data;
using Fina.Api.Handlers;
using Fina.Core;
using Fina.Core.Handler;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Common.Api;

public static class BuildExtensions
{
    public static void AddConfiguration(this WebApplicationBuilder builder) 
    {
        ApiConfiguration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        Configuration.BackEndUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuration.FrontEndUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }
    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(n=> n.FullName));
    }

    public static void AddDataContext(this WebApplicationBuilder builder) 
    {
        builder.Services.AddDbContext<AppDbContext>(x=> x.UseSqlServer(ApiConfiguration.ConnectionString));
    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder) 
    {
        builder.Services.AddCors(
            options => options.AddPolicy(
                ApiConfiguration.CorsPolicyName, policy => policy.WithOrigins([
                    Configuration.BackEndUrl,
                    Configuration.FrontEndUrl
                ])
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                )
            );
        Console.Clear();
        Console.WriteLine($"Api iniciada com sucesso em: {Configuration.BackEndUrl}");
        Console.WriteLine($"Web iniciada com sucesso em: {Configuration.FrontEndUrl}");
    }

    public static void AddServices(this WebApplicationBuilder builder) 
    {
        builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
        builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
    }


}
