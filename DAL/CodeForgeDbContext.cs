using System.Diagnostics;
using Domain.Accounts;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer;

public class CodeForgeDbContext : IdentityDbContext<User>
{
    
    public DbSet<Flow> Flows { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ISteps> Steps { get; set; }
    public DbSet<IQuestion<object>> Questions { get; set; }
    public DbSet<IInformation> Informations { get; set; }

    
    
    public CodeForgeDbContext(DbContextOptions<CodeForgeDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       // if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlite("Data Source=../GameDb.sqlite");
        optionsBuilder.UseLazyLoadingProxies(false);
        optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>()
            .HasOne(project => project.Theme);
        
        modelBuilder.Entity<Flow>()
            .HasMany(flow => flow.Steps);
        
    }
    
    public bool CreateDatabase(bool dropDatabase)
    {
        if (dropDatabase) Database.EnsureDeleted();
        return Database.EnsureCreated();
    }
    
}