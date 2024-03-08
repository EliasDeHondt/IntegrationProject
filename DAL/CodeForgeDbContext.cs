using System.Diagnostics;
using Domain.Accounts;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data_Access_Layer;

public class CodeForgeDbContext : IdentityDbContext<User>
{
    
    public DbSet<Flow> Flows { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<InformationStep> InformationSteps { get; set; }
    public DbSet<CombinedStep<object>> CombinedSteps { get; set; }
    public DbSet<QuestionStep<object>> QuestionSteps { get; set; }
    public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
    public DbSet<OpenQuestion> OpenQuestions { get; set; }
    public DbSet<RangeQuestion> RangeQuestions { get; set; }
    public DbSet<SingleChoiceQuestion> SingleChoiceQuestions { get; set; }
    public DbSet<IInformation> Informations { get; set; }

    
    
    public CodeForgeDbContext(DbContextOptions<CodeForgeDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       if (!optionsBuilder.IsConfigured) optionsBuilder.UseNpgsql();
        optionsBuilder.UseLazyLoadingProxies(false);
        optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Information);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>()
            .HasOne(project => project.MainTheme)
            .WithOne(mainTheme => mainTheme.Project);
        
        modelBuilder.Entity<Flow>()
            .HasMany(flow => flow.Steps)
            .WithOne(step => step.Flow);
        
        modelBuilder.Entity<ProjectOrganizer>()
            .HasOne(p => p.Note)
            .WithMany();
    }
    
    public bool CreateDatabase(bool dropDatabase)
    {
        if (dropDatabase) Database.EnsureDeleted();
        return Database.EnsureCreated();
    }
    
}