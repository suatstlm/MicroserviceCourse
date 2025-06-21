using IdentityServer;
using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    Log.Information("Building application...");
    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    Log.Information("Running database migrations...");
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            dbContext.Database.Migrate();
            Log.Information("Database migrations completed successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error during database migration");
            throw;
        }

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        try
        {
            if (!userManager.Users.Any())
            {
                Log.Information("Creating default user...");
                var result = await userManager.CreateAsync(
                    new ApplicationUser
                    {
                        UserName = "Suat",
                        Email = "suatstlm41@gmail.com",
                    }, "1234");
                
                if (result.Succeeded)
                {
                    Log.Information("Default user created successfully");
                }
                else
                {
                    Log.Error("Failed to create default user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                Log.Information("Users already exist in database");
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error during user creation");
            throw;
        }
    }

    // this seeding is only for the template to bootstrap the DB and users.
    // in production you will likely want a different approach.
    //if (args.Contains("/seed"))
    //{
    //    Log.Information("Seeding database...");
    //    SeedData.EnsureSeedData(app);
    //    Log.Information("Done seeding database. Exiting.");
    //    return;
    //}

    Log.Information("Starting application...");
    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Unhandled exception during startup");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}