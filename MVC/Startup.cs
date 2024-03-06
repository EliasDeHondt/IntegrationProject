using System.Text.Json.Serialization;
using Business_Layer;
using Domain.ProjectLogics;

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

        services.AddScoped<FlowManager, FlowManager>();
        services.AddScoped<IManager<Project>, ProjectManager>();
        
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