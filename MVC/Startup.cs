using System.Text.Json.Serialization;
using Business_Layer;
using Data_Access_Layer;
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
        // TODO: implementeer dependency injection

        services.AddDbContext<CodeForgeDbContext>(options =>
            //"Host=34.77.23.244;Port=5432;Database=codeforge;Username=admin;Password=123"
            options.UseNpgsql("bin/CodeForge.db"));
        services.AddScoped<FlowManager, FlowManager>();
        services.AddScoped<ProjectManager, ProjectManager>();
        
        
        using var serviceScope = services.BuildServiceProvider().CreateScope();
        
        // TODO: Initializeer dbcontext
        
        services.AddControllersWithViews().AddXmlSerializerFormatters().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
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