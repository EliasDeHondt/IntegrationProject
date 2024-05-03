/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.Text.Json;
using System.Text.Json.Serialization;
using Business_Layer;
using Data_Access_Layer;
using Data_Access_Layer.DbContext;
using Domain.Accounts;
using Domain.Platform;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
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
        string bucketName = Environment.GetEnvironmentVariable("BUCKET_NAME_VIDEO") ?? "codeforge-video-bucket";

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "../service-account-key.json");

        //REMOVE AFTER TESTING
        Environment.SetEnvironmentVariable("ASPNETCORE_EMAIL", "codeforge.noreply@gmail.com");
        Environment.SetEnvironmentVariable("ASPNETCORE_EMAIL_PASSWORD", "evqb lztz oqvu kgwc");


        var options = new CloudStorageOptions
        {
            BucketName = bucketName
        };

        var emailOptions = new EmailOptions()
        {
            Email = Environment.GetEnvironmentVariable("ASPNETCORE_EMAIL"),
            Password = Environment.GetEnvironmentVariable("ASPNETCORE_EMAIL_PASSWORD")
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

        services.AddScoped<UnitOfWork, UnitOfWork>();
        services.AddSingleton(options);
        services.AddSingleton(emailOptions);

        services.AddControllersWithViews().AddXmlSerializerFormatters().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
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
                    seedDatabase(uow, userManager, roleManager, dbContext);
                }
                break;
            }
            case "Production":
            {
                if (dbContext.IsEmpty())
                {
                    seedDatabase(uow, userManager, roleManager, dbContext);
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
        });
    }

    public async Task SeedUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var sharedPlatformAdminHenk = new SpAdmin
        {
            Id = "HenkId",
            Email = "Henk@CodeForge.com",
            UserName = "Henk",
            EmailConfirmed = true,
            SharedPlatform = new SharedPlatform()
        };

        var sharedPlatformAdminCodeForge = new SpAdmin()
        {
            Id = "CodeForgeId",
            Email = "CodeForge.noreply@gmail.com",
            UserName = "CodeForge",
            EmailConfirmed = true,
            SharedPlatform = new SharedPlatform()
        };

        await roleManager.CreateAsync(new IdentityRole(UserRoles.Facilitator));
        await roleManager.CreateAsync(new IdentityRole(UserRoles.PlatformAdmin));
        await roleManager.CreateAsync(new IdentityRole(UserRoles.SystemAdmin));

        await roleManager.CreateAsync(new IdentityRole(UserRoles.UserPermission));
        await roleManager.CreateAsync(new IdentityRole(UserRoles.ProjectPermission));
        await roleManager.CreateAsync(new IdentityRole(UserRoles.StatisticPermission));

        await userManager.CreateAsync(sharedPlatformAdminHenk, "Henk!123");
        await userManager.CreateAsync(sharedPlatformAdminCodeForge, "Codeforge!123");

        await userManager.AddToRoleAsync(sharedPlatformAdminHenk, UserRoles.PlatformAdmin);
        await userManager.AddToRoleAsync(sharedPlatformAdminHenk, UserRoles.UserPermission);
        await userManager.AddToRoleAsync(sharedPlatformAdminHenk, UserRoles.ProjectPermission);
        await userManager.AddToRoleAsync(sharedPlatformAdminHenk, UserRoles.StatisticPermission);

        await userManager.AddToRoleAsync(sharedPlatformAdminCodeForge, UserRoles.PlatformAdmin);
    }

    void seedDatabase(UnitOfWork uow, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
        CodeForgeDbContext dbContext)
    {
        uow.BeginTransaction();
        SeedUsers(userManager, roleManager).Wait();
        DataSeeder.Seed(dbContext);
        uow.Commit();
    }
}