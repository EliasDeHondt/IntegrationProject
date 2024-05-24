/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.Text.Json.Serialization;
using Business_Layer;
using Data_Access_Layer;
using Data_Access_Layer.DbContext;
using Domain.Accounts;
using Domain.FacilitatorFunctionality;
using Domain.Platform;
using Microsoft.AspNetCore.Identity;

namespace MVC;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        string bucketName = Environment.GetEnvironmentVariable("BUCKET_NAME_VIDEO") ?? "codeforge-video-bucket-20240514045342";

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "../service-account-key.json");

        //REMOVE AFTER TESTING
        Environment.SetEnvironmentVariable("ASPNETCORE_EMAIL", "codeforge.noreply@gmail.com");
        Environment.SetEnvironmentVariable("ASPNETCORE_EMAIL_PASSWORD", "evqb lztz oqvu kgwc");


        var googleCloudOptions = new CloudStorageOptions
        {
            BucketName = bucketName
        };

        var emailOptions = new EmailOptions()
        {
            Email = Environment.GetEnvironmentVariable("ASPNETCORE_EMAIL")!,
            Password = Environment.GetEnvironmentVariable("ASPNETCORE_EMAIL_PASSWORD")!
        };

        //dependency injection
        services.AddDbContext<CodeForgeDbContext>();
        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<CodeForgeDbContext>();

        services.AddDataProtection();
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("admin",
                policy => policy.RequireRole(UserRoles.PlatformAdmin, UserRoles.SystemAdmin));
            options.AddPolicy("systemAdmin",
                policy => policy.RequireRole(UserRoles.SystemAdmin));
        });

        services.ConfigureApplicationCookie(cfg =>
        {
            cfg.Events.OnRedirectToLogin += ctx =>
            {
                if (ctx.Request.Path.StartsWithSegments("/api"))
                {
                    ctx.Response.StatusCode = 401;
                }

                return Task.CompletedTask;
            };

            cfg.Events.OnRedirectToAccessDenied += ctx =>
            {
                if (ctx.Request.Path.StartsWithSegments("/api"))
                {
                    ctx.Response.StatusCode = 403;
                }

                return Task.CompletedTask;
            };
        });

        services.AddScoped<FlowRepository>();
        services.AddScoped<FlowManager>();

        services.AddScoped<ProjectManager>();

        services.AddScoped<StepRepository>();
        services.AddScoped<StepManager>();

        services.AddScoped<QuestionManager>();
        services.AddScoped<QuestionRepository>();

        services.AddScoped<AnswerManager>();
        services.AddScoped<AnswerRepository>();

        services.AddScoped<ThemeRepository>();
        services.AddScoped<ThemeManager>();

        services.AddScoped<SharedPlatformRepository>();
        services.AddScoped<SharedPlatformManager>();

        services.AddScoped<CustomUserManager>();
        services.AddScoped<UserRepository>();

        services.AddScoped<ProjectManager>();
        services.AddScoped<ProjectRepository>();

        services.AddScoped<EmailManager>();

        services.AddScoped<FeedRepository>();
        services.AddScoped<FeedManager>();
        
        services.AddScoped<IdeaRepository>();
        services.AddScoped<IdeaManager>();

        services.AddScoped<ReactionRepository>();
        services.AddScoped<ReactionManager>();
        
        services.AddScoped<UnitOfWork, UnitOfWork>();
        services.AddSingleton(googleCloudOptions);
        services.AddSingleton(emailOptions);

        services.AddControllersWithViews().AddXmlSerializerFormatters().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddSignalR();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        using var serviceScope = app.ApplicationServices.CreateScope();

        //init dbcontext
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<CodeForgeDbContext>();
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var uow = serviceScope.ServiceProvider.GetRequiredService<UnitOfWork>();

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        switch (environment)
        {
            case "Development":
            {
                if (dbContext.CreateDatabase(true))
                {
                    SeedDatabase(uow, userManager, roleManager, dbContext);
                }
                break;
            }
            case "Production":
            {
                if (!dbContext.IsEmpty()) // TODO: Check if this is correct
                {
                    dbContext.CreateDatabase(true);
                    SeedDatabase(uow, userManager, roleManager, dbContext);
                }

                break;
            }
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllerRoute(name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapHub<FacilitatorHub>("/hub");
        });
    }

    async Task SeedUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var sharedPlatformAdminHenk = new SpAdmin
        {
            Id = "HenkId",
            Email = "Henk@CodeForge.com",
            UserName = "Bab",
            EmailConfirmed = true,
            SharedPlatform = new SharedPlatform()
        };

        var sharedPlatformAdminCodeForge = new SpAdmin
        {
            Id = "CodeForgeId",
            Email = "CodeForge.noreply@gmail.com",
            UserName = "Bub",
            EmailConfirmed = true,
            SharedPlatform = new SharedPlatform()
        };

        var systemAdminBob = new SystemAdmin
        {
            Id = "BobId",
            Email = "Bob@CodeForge.com",
            UserName = "Bob",
            EmailConfirmed = true
        };
        
        var facilitatorTom = new Facilitator
        {
            Id = "TomId",
            Email = "Tom@CodeForge.com",
            UserName = "Tom",
            EmailConfirmed = true
        };
        
        var facilitatorFred = new Facilitator
        {
            Id = "FredId",
            Email = "Fred@kdg.be",
            UserName = "Fred",
            EmailConfirmed = true
        };
        
        var sharedPlatformAdminThomas = new SpAdmin
        {
            Id = "ThomasId",
            Email = "Thomas@kdg.be",
            UserName = "Thomas",
            EmailConfirmed = true,
            SharedPlatform = new SharedPlatform()
        };
        
        var sharedPlatformAdminKdg = new SpAdmin
        {
            Id = "KdgId",
            Email = "kdg.noreply@kdg.be",
            UserName = "Kdg",
            EmailConfirmed = true,
            SharedPlatform = new SharedPlatform()
        };

        var webAppUserBib = new WebAppUser
        {
            Id = "BibId",
            Email = "Bib@CodeForge.com",
            UserName = "Bib",
            EmailConfirmed = true
        };
            
        await roleManager.CreateAsync(new IdentityRole(UserRoles.Facilitator));
        await roleManager.CreateAsync(new IdentityRole(UserRoles.PlatformAdmin));
        await roleManager.CreateAsync(new IdentityRole(UserRoles.SystemAdmin));
        await roleManager.CreateAsync(new IdentityRole(UserRoles.Respondent));

        await roleManager.CreateAsync(new IdentityRole(UserRoles.UserPermission));
        await roleManager.CreateAsync(new IdentityRole(UserRoles.ProjectPermission));

        await userManager.CreateAsync(sharedPlatformAdminHenk, "Henk!123");
        await userManager.CreateAsync(sharedPlatformAdminCodeForge, "Codeforge!123");
        await userManager.CreateAsync(systemAdminBob, "Bob!123");
        await userManager.CreateAsync(facilitatorTom, "Tom!123");
        await userManager.CreateAsync(facilitatorFred, "Fred!123");
        await userManager.CreateAsync(sharedPlatformAdminThomas, "Thomas!123");
        await userManager.CreateAsync(sharedPlatformAdminKdg, "Kdg!123");
        await userManager.CreateAsync(webAppUserBib, "Bib!123");

        await userManager.AddToRoleAsync(sharedPlatformAdminHenk, UserRoles.PlatformAdmin);
        await userManager.AddToRoleAsync(sharedPlatformAdminHenk, UserRoles.UserPermission);
        await userManager.AddToRoleAsync(sharedPlatformAdminHenk, UserRoles.ProjectPermission);
        
        await userManager.AddToRoleAsync(sharedPlatformAdminThomas, UserRoles.PlatformAdmin);
        await userManager.AddToRoleAsync(sharedPlatformAdminThomas, UserRoles.UserPermission);
        await userManager.AddToRoleAsync(sharedPlatformAdminThomas, UserRoles.ProjectPermission);

        await userManager.AddToRoleAsync(sharedPlatformAdminCodeForge, UserRoles.PlatformAdmin);
        
        await userManager.AddToRoleAsync(sharedPlatformAdminKdg, UserRoles.PlatformAdmin);
        
        await userManager.AddToRoleAsync(systemAdminBob, UserRoles.SystemAdmin);

        await userManager.AddToRoleAsync(facilitatorTom, UserRoles.Facilitator);

        await userManager.AddToRoleAsync(facilitatorFred, UserRoles.Facilitator);

        await userManager.AddToRoleAsync(webAppUserBib, UserRoles.Respondent);
    }

    void SeedDatabase(UnitOfWork uow, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
        CodeForgeDbContext dbContext)
    {
        uow.BeginTransaction();
        SeedUsers(userManager, roleManager).Wait();
        DataSeeder.Seed(dbContext);
        uow.Commit();
    }
}