/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.Diagnostics;
using Domain.Accounts;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;
using Domain.ProjectLogics.Steps.Questions.Answers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data_Access_Layer.DbContext;

public class CodeForgeDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Flow> Flows { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<MainTheme> MainThemes { get; set; }
    public DbSet<SubTheme> SubThemes { get; set; }
    public DbSet<ChoiceQuestionBase> ChoiceQuestions { get; set; }
    public DbSet<InformationStep> InformationSteps { get; set; }
    public DbSet<CombinedStep> CombinedSteps { get; set; }
    public DbSet<QuestionStep> QuestionSteps { get; set; }
    public DbSet<OpenQuestion> OpenQuestions { get; set; }
    public DbSet<Text> Texts { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<Participation> Participations { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Choice> Choices { get; set; }
    public DbSet<Selection> Selections { get; set; }
    
    public DbSet<Respondent> Respondents { get; set; }
    public DbSet<Facilitator> Facilitators { get; set; }
    public DbSet<SpAdmin> SpAdmins { get; set; }
    public DbSet<SystemAdmin> SystemAdmins { get; set; }
    public DbSet<SharedPlatform> SharedPlatforms { get; set; }

    public CodeForgeDbContext(DbContextOptions<CodeForgeDbContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            switch (environment)
            {
                case "Development":
                    optionsBuilder.UseSqlite(@"Data Source=../CodeForge.db");
                    break;
                case "Production":
                {
                    var host = Environment.GetEnvironmentVariable("ASPNETCORE_POSTGRES_HOST");
                    var port = Environment.GetEnvironmentVariable("ASPNETCORE_POSTGRES_PORT");
                    var database = Environment.GetEnvironmentVariable("ASPNETCORE_POSTGRES_DATABASE");
                    var username = Environment.GetEnvironmentVariable("ASPNETCORE_POSTGRES_USER");
                    var password = Environment.GetEnvironmentVariable("ASPNETCORE_POSTGRES_PASSWORD");
                    
                    string connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";
                    optionsBuilder.UseNpgsql(connectionString);
                    break;
                }
            }
        }
        optionsBuilder.UseLazyLoadingProxies(false);
        optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MainTheme>().HasBaseType<ThemeBase>();
        modelBuilder.Entity<SubTheme>().HasBaseType<ThemeBase>();

        modelBuilder.Entity<Image>().HasBaseType<InformationBase>();
        modelBuilder.Entity<Text>().HasBaseType<InformationBase>();
        modelBuilder.Entity<Video>().HasBaseType<InformationBase>();

        modelBuilder.Entity<ChoiceQuestionBase>().HasBaseType<QuestionBase>();
        modelBuilder.Entity<MultipleChoiceQuestion>().HasBaseType<ChoiceQuestionBase>();
        modelBuilder.Entity<SingleChoiceQuestion>().HasBaseType<ChoiceQuestionBase>();
        modelBuilder.Entity<RangeQuestion>().HasBaseType<ChoiceQuestionBase>();
        modelBuilder.Entity<OpenQuestion>().HasBaseType<QuestionBase>();

        modelBuilder.Entity<QuestionStep>().HasBaseType<StepBase>();
        modelBuilder.Entity<InformationStep>().HasBaseType<StepBase>();
        modelBuilder.Entity<CombinedStep>().HasBaseType<StepBase>();

        modelBuilder.Entity<ChoiceAnswer>().HasBaseType<Answer>();
        modelBuilder.Entity<OpenAnswer>().HasBaseType<Answer>();
        
        // Reflects domain configuration.
        modelBuilder.Entity<Note>(entity => entity.Property(e => e.Textfield).IsRequired().HasMaxLength(15000));
        modelBuilder.Entity<Image>(entity => entity.Property(e => e.Base64).IsRequired().HasMaxLength(900000));
        modelBuilder.Entity<Text>(entity => entity.Property(e => e.InformationText).IsRequired().HasMaxLength(600));
        modelBuilder.Entity<Video>(entity => entity.Property(e => e.FilePath).IsRequired().HasMaxLength(200));
        modelBuilder.Entity<OpenQuestion>(entity => entity.Property(e => e.TextField).IsRequired().HasMaxLength(600));
        modelBuilder.Entity<QuestionBase>(entity => entity.Property(e => e.Question).IsRequired().HasMaxLength(600));
        modelBuilder.Entity<QuestionBase>(entity => entity.Property(e => e.Question).IsRequired().HasMaxLength(600));
        modelBuilder.Entity<Choice>(entity => entity.Property(e => e.Text).IsRequired().HasMaxLength(600));
        modelBuilder.Entity<ThemeBase>(entity => entity.Property(e => e.Subject).IsRequired().HasMaxLength(600));
        
        modelBuilder.Entity<Project>()
            .HasOne(project => project.MainTheme);

        modelBuilder.Entity<MainTheme>()
            .HasMany(mainTheme => mainTheme.Themes)
            .WithOne(theme => theme.MainTheme)
            .HasForeignKey("FK_MainTheme_Id");

        modelBuilder.Entity<MainTheme>()
            .HasMany(theme => theme.Flows)
            .WithOne(flow => (MainTheme)flow.Theme);

        modelBuilder.Entity<Flow>()
            .HasMany(flow => flow.Steps)
            .WithOne(step => step.Flow)
            .HasForeignKey("FK_Step_Id");
        
        modelBuilder.Entity<SubTheme>()
            .HasMany(theme => theme.Flows)
            .WithOne(flow => (SubTheme)flow.Theme);
        
        modelBuilder.Entity<Participation>()
            .HasOne(participation => participation.Flow)
            .WithMany(flow => flow.Participations)
            .HasForeignKey("FK_Flow_Id");

        modelBuilder.Entity<ChoiceAnswer>()
            .HasMany(a => a.Answers)
            .WithOne(s => s.ChoiceAnswer)
            .HasForeignKey("FK_Selection_AnswerId");

        modelBuilder.Entity<Choice>()
            .HasMany(c => c.Selections)
            .WithOne(s => s.Choice)
            .HasForeignKey("FK_Selection_ChoiceId");

        modelBuilder.Entity<SharedPlatform>()
            .HasMany<Project>()
            .WithOne(project => project.SharedPlatform);
        
        modelBuilder.Entity<Project>().HasKey(project => project.Id);
        modelBuilder.Entity<ThemeBase>().HasKey(theme => theme.Id);
        modelBuilder.Entity<QuestionBase>().HasKey(question => question.Id);
        modelBuilder.Entity<InformationBase>().HasKey(information => information.Id);
        modelBuilder.Entity<StepBase>().HasKey(step => step.Id);
        modelBuilder.Entity<Flow>().HasKey(flow => flow.Id);
        modelBuilder.Entity<Participation>().HasKey(participation => participation.Id);
        modelBuilder.Entity<Answer>().HasKey(answer => answer.Id);
        modelBuilder.Entity<Selection>().HasKey("FK_Selection_AnswerId", "FK_Selection_ChoiceId");

        modelBuilder.Entity<Respondent>().HasKey(respondent =>  respondent.RespondentId);
    }

    public bool CreateDatabase(bool dropDatabase)
    {
        if (dropDatabase) Database.EnsureDeleted();
        return Database.EnsureCreated();
    }
}