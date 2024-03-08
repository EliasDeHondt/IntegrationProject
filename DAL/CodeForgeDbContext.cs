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
    public DbSet<MainTheme> MainThemes { get; set; }
    public DbSet<SubTheme> SubThemes { get; set; }
    public DbSet<InformationStep> InformationSteps { get; set; }
    public DbSet<CombinedStep> CombinedSteps { get; set; }
    public DbSet<QuestionStep> QuestionSteps { get; set; }
    public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
    public DbSet<OpenQuestion> OpenQuestions { get; set; }
    public DbSet<RangeQuestion> RangeQuestions { get; set; }
    public DbSet<SingleChoiceQuestion> SingleChoiceQuestions { get; set; }
    public DbSet<IInformation> Informations { get; set; }
    public DbSet<Participation> Participations { get; set; }
    public DbSet<Answer> Answers { get; set; }


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
            .WithOne(mainTheme => mainTheme.Project)
            .HasForeignKey("FK_MainTheme_MainThemeId");

        modelBuilder.Entity<MainTheme>()
            .HasMany(mainTheme => mainTheme.Themes)
            .WithOne(theme => theme.MainTheme)
            .HasForeignKey("FK_Theme_MainThemeId");

        modelBuilder.Entity<Flow>()
            .HasOne(flow => flow.Theme)
            .WithMany(theme => theme.Flows)
            .HasForeignKey("FK_Theme_ThemeId");
        
        modelBuilder.Entity<Flow>()
            .HasMany(flow => flow.Steps)
            .WithOne(step => step.Flow)
            .HasForeignKey("FK_Step_FlowId");

        modelBuilder.Entity<InformationStep>()
            .HasOne(step => step.Information)
            .WithOne()
            .HasForeignKey("FK_Information_InformationId");

        modelBuilder.Entity<QuestionStep>()
            .HasOne(step => step.Question)
            .WithOne()
            .HasForeignKey("FK_Question_QuestionId");
        
        modelBuilder.Entity<CombinedStep>()
            .HasOne(step => step.Question)
            .WithOne()
            .HasForeignKey("FK_Question_QuestionId");
        
        modelBuilder.Entity<CombinedStep>()
            .HasOne(step => step.Information)
            .WithOne()
            .HasForeignKey("FK_Information_InformationId");
            
        modelBuilder.Entity<IInformation>()
            .HasOne(information => information.Step)
            .WithOne()
            .HasForeignKey("FK_Step_StepId");

        modelBuilder.Entity<Participation>()
            .HasOne(participation => participation.Flow)
            .WithMany(flow => flow.Participations)
            .HasForeignKey("FK_Flow_FlowId");
        
        modelBuilder.Entity<MultipleChoiceQuestion>()
            .HasOne(question => question.Step)
            .WithOne()
            .HasForeignKey("FK_Step_StepId");
        
        modelBuilder.Entity<SingleChoiceQuestion>()
            .HasOne(question => question.Step)
            .WithOne()
            .HasForeignKey("FK_Step_StepId");
        
        modelBuilder.Entity<RangeQuestion>()
            .HasOne(question => question.Step)
            .WithOne()
            .HasForeignKey("FK_Step_StepId");
        
        modelBuilder.Entity<OpenQuestion>()
            .HasOne(question => question.Step)
            .WithOne()
            .HasForeignKey("FK_Step_StepId");
        
        modelBuilder.Entity<Answer>()
            .HasOne(answer => answer.Question)
            .WithMany(question => question.Answers)
            .HasForeignKey("FK_Answer_AnswerId");

        modelBuilder.Entity<Project>().HasKey(project => project.Id);
        modelBuilder.Entity<MainTheme>().HasKey(mainTheme => mainTheme.Id);
        modelBuilder.Entity<SubTheme>().HasKey(subTheme => subTheme.Id);
        modelBuilder.Entity<Flow>().HasKey(flow => flow.Id);
        modelBuilder.Entity<Participation>().HasKey(participation => participation.Id);
        modelBuilder.Entity<InformationStep>().HasKey(step => step.Id);
        modelBuilder.Entity<CombinedStep>().HasKey(step => step.Id);
        modelBuilder.Entity<QuestionStep>().HasKey(step => step.Id);
        modelBuilder.Entity<MultipleChoiceQuestion>().HasKey(question => question.Id);
        modelBuilder.Entity<SingleChoiceQuestion>().HasKey(question => question.Id);
        modelBuilder.Entity<RangeQuestion>().HasKey(question => question.Id);
        modelBuilder.Entity<OpenQuestion>().HasKey(question => question.Id);
        modelBuilder.Entity<Answer>().HasKey(answer => answer.Id);


    }

    public bool CreateDatabase(bool dropDatabase)
    {
        if (dropDatabase) Database.EnsureDeleted();
        return Database.EnsureCreated();
    }
}