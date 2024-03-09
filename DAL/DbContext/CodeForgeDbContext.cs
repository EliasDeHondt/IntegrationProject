using System.Collections;
using System.Diagnostics;
using Domain.Accounts;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data_Access_Layer.DbContext;

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
    public DbSet<Text> Texts { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<Participation> Participations { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Choice> Choices { get; set; }

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
            .HasOne(project => project.MainTheme);

        modelBuilder.Entity<MainTheme>()
            .HasMany(mainTheme => mainTheme.Themes)
            .WithOne(theme => theme.MainTheme)
            .HasForeignKey("FK_Theme_Id");

        modelBuilder.Entity<MainTheme>()
            .HasMany(mainTheme => mainTheme.Flows)
            .WithOne()
            .HasForeignKey("FK_Flow_Id");
        
        modelBuilder.Entity<Flow>()
            .HasMany(flow => (ICollection<InformationStep>)flow.Steps)
            .WithOne(step => step.Flow)
            .HasForeignKey("FK_Step_FlowId");
        
        modelBuilder.Entity<Flow>()
            .HasMany(flow => (ICollection<QuestionStep>)flow.Steps)
            .WithOne(step => step.Flow)
            .HasForeignKey("FK_Step_FlowId");
        
        modelBuilder.Entity<Flow>()
            .HasMany(flow => (ICollection<CombinedStep>)flow.Steps)
            .WithOne(step => step.Flow)
            .HasForeignKey("FK_Step_FlowId");

        modelBuilder.Entity<Flow>()
            .HasOne(flow => (MainTheme)flow.Theme)
            .WithMany(theme => theme.Flows)
            .HasForeignKey("FK_Theme_Id");

        modelBuilder.Entity<Flow>()
            .HasOne(flow => (SubTheme)flow.Theme)
            .WithMany(theme => theme.Flows)
            .HasForeignKey("FK_Theme_Id");
        
        modelBuilder.Entity<Participation>()
            .HasOne(participation => participation.Flow)
            .WithMany(flow => flow.Participations)
            .HasForeignKey("FK_Flow_Id");
        
        modelBuilder.Entity<Answer>()
            .HasOne(answer => (SingleChoiceQuestion)answer.Question)
            .WithMany(question => question.Answers)
            .HasForeignKey("FK_Answer_Id");

        modelBuilder.Entity<Answer>()
            .HasOne(answer => (MultipleChoiceQuestion)answer.Question)
            .WithMany(question => question.Answers)
            .HasForeignKey("FK_Answer_Id");
        
        modelBuilder.Entity<Answer>()
            .HasOne(answer => (RangeQuestion)answer.Question)
            .WithMany(question => question.Answers)
            .HasForeignKey("FK_Answer_Id");
        
        modelBuilder.Entity<Answer>()
            .HasOne(answer => (OpenQuestion)answer.Question)
            .WithMany(question => question.Answers)
            .HasForeignKey("FK_Answer_Id");

        modelBuilder.Entity<CombinedStep>()
            .HasOne(step => (Text)step.Information)
            .WithOne()
            .HasForeignKey("FK_Text_Id");

        modelBuilder.Entity<CombinedStep>()
            .HasOne(step => (Image)step.Information)
            .WithOne()
            .HasForeignKey("FK_Step_Id");

        modelBuilder.Entity<CombinedStep>()
            .HasOne(step => (Video)step.Information)
            .WithOne()
            .HasForeignKey("FK_Step_Id");
        
        modelBuilder.Entity<CombinedStep>()
            .HasOne(step => (MultipleChoiceQuestion)step.Question)
            .WithOne()
            .HasForeignKey("FK_Step_Id");

        modelBuilder.Entity<CombinedStep>()
            .HasOne(step => (RangeQuestion)step.Question)
            .WithOne()
            .HasForeignKey("FK_Step_Id");
        
        modelBuilder.Entity<CombinedStep>()
            .HasOne(step => (SingleChoiceQuestion)step.Question)
            .WithOne()
            .HasForeignKey("FK_Step_Id");
        
        modelBuilder.Entity<CombinedStep>()
            .HasOne(step => (OpenQuestion)step.Question)
            .WithOne()
            .HasForeignKey("FK_Step_Id");
        
        modelBuilder.Entity<InformationStep>()
            .HasOne(step => (Video)step.Information)
            .WithOne()
            .HasForeignKey("FK_Step_Id");

        modelBuilder.Entity<InformationStep>()
            .HasOne(step => (Image)step.Information)
            .WithOne()
            .HasForeignKey("FK_Step_Id");

        modelBuilder.Entity<InformationStep>()
            .HasOne(step => (Text)step.Information)
            .WithOne()
            .HasForeignKey("FK_Step_Id");
        
        modelBuilder.Entity<InformationStep>()
            .HasOne(step => (MultipleChoiceQuestion)step.Question)
            .WithOne()
            .HasForeignKey("FK_Step_Id");
        
        modelBuilder.Entity<InformationStep>()
            .HasOne(step => (RangeQuestion)step.Question)
            .WithOne()
            .HasForeignKey("FK_Step_Id");
        
        modelBuilder.Entity<InformationStep>()
            .HasOne(step => (OpenQuestion)step.Question)
            .WithOne()
            .HasForeignKey("FK_Step_Id");
        
        modelBuilder.Entity<InformationStep>()
            .HasOne(step => (SingleChoiceQuestion)step.Question)
            .WithOne()
            .HasForeignKey("FK_Step_Id");
        
        modelBuilder.Entity<QuestionStep>()
            .HasOne(step => (Video)step.Information)
            .WithOne()
            .HasForeignKey("FK_Step_Id");

        modelBuilder.Entity<QuestionStep>()
            .HasOne(step => (Image)step.Information)
            .WithOne()
            .HasForeignKey("FK_Step_Id");

        modelBuilder.Entity<QuestionStep>()
            .HasOne(step => (Text)step.Information)
            .WithOne()
            .HasForeignKey("FK_Step_Id");
        
        modelBuilder.Entity<QuestionStep>()
            .HasOne(step => (MultipleChoiceQuestion)step.Question)
            .WithOne()
            .HasForeignKey("FK_Step_Id");
        
        modelBuilder.Entity<QuestionStep>()
            .HasOne(step => (RangeQuestion)step.Question)
            .WithOne()
            .HasForeignKey("FK_Step_Id");
        
        modelBuilder.Entity<QuestionStep>()
            .HasOne(step => (OpenQuestion)step.Question)
            .WithOne()
            .HasForeignKey("FK_Step_Id");
        
        modelBuilder.Entity<QuestionStep>()
            .HasOne(step => (SingleChoiceQuestion)step.Question)
            .WithOne()
            .HasForeignKey("FK_Step_Id");
        
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