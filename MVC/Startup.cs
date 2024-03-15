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
using Domain.ProjectLogics;
using Microsoft.EntityFrameworkCore;

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

        var options = new CloudStorageOptions
        {
            BucketName = Configuration["codeforge-bucket-videos"]
        };
        
        if (Configuration["codeforge-bucket-videos"] is not null) options.ObjectName = Configuration["codeforge-bucket-videos"];
        
        
        //dependency injection
        services.AddDbContext<CodeForgeDbContext>();
        services.AddScoped<FlowManager, FlowManager>();
        services.AddScoped<ProjectManager, ProjectManager>();
        services.AddScoped<StepRepository, StepRepository>();
        services.AddScoped<StepManager, StepManager>();
        services.AddScoped<UnitOfWork, UnitOfWork>();
        services.AddSingleton(options);
        
        using var serviceScope = services.BuildServiceProvider().CreateScope();
        
        //init dbcontext
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<CodeForgeDbContext>();
        var uow = serviceScope.ServiceProvider.GetRequiredService<UnitOfWork>();
        if (dbContext.CreateDatabase(true))
        {
            uow.BeginTransaction();
            DataSeeder.Seed(dbContext);
            uow.Commit();
        }
        
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

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}