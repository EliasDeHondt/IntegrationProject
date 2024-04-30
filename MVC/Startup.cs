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
        // REVERT AFTER TESTING
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "../service-account-key.json");
        
        var options = new CloudStorageOptions
        {
            BucketName = bucketName
        };

        //dependency injection
        services.AddDbContext<CodeForgeDbContext>();
        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<CodeForgeDbContext>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("admin", policy => policy.RequireRole(UserRoles.PlatformAdmin, UserRoles.SystemAdmin));
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
        
        services.AddScoped<UnitOfWork, UnitOfWork>();
        services.AddSingleton(options);
        
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
        if (dbContext.CreateDatabase(true))
        {
            uow.BeginTransaction();
            SeedUsers(userManager, roleManager).Wait();
            DataSeeder.Seed(dbContext);
            uow.Commit();
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

        await roleManager.CreateAsync(new IdentityRole(UserRoles.Facilitator));
        await roleManager.CreateAsync(new IdentityRole(UserRoles.PlatformAdmin));
        await roleManager.CreateAsync(new IdentityRole(UserRoles.SystemAdmin));

        await roleManager.CreateAsync(new IdentityRole(UserRoles.UserPermission));
        await roleManager.CreateAsync(new IdentityRole(UserRoles.ProjectPermission));
        await roleManager.CreateAsync(new IdentityRole(UserRoles.StatisticPermission));
        
        await userManager.CreateAsync(sharedPlatformAdminHenk, "Henk!123");
        
        await userManager.AddToRoleAsync(sharedPlatformAdminHenk, UserRoles.PlatformAdmin);
        await userManager.AddToRoleAsync(sharedPlatformAdminHenk, UserRoles.UserPermission);
        await userManager.AddToRoleAsync(sharedPlatformAdminHenk, UserRoles.ProjectPermission);
        await userManager.AddToRoleAsync(sharedPlatformAdminHenk, UserRoles.StatisticPermission);
    }

}