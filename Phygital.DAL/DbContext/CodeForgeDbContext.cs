/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.Diagnostics;
using Domain.Accounts;
using Domain.FacilitatorFunctionality;
using Domain.Platform;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;
using Domain.ProjectLogics.Steps.Questions.Answers;
using Domain.WebApp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data_Access_Layer.DbContext;

public class CodeForgeDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Flow> Flows { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<MainTheme> MainThemes { get; set; } = null!;
    public DbSet<SubTheme> SubThemes { get; set; } = null!;
    public DbSet<StepBase> Steps { get; set; } = null!;
    public DbSet<ChoiceQuestionBase> ChoiceQuestions { get; set; } = null!;
    public DbSet<InformationStep> InformationSteps { get; set; } = null!;
    public DbSet<CombinedStep> CombinedSteps { get; set; } = null!;
    public DbSet<QuestionStep> QuestionSteps { get; set; } = null!;
    public DbSet<OpenQuestion> OpenQuestions { get; set; } = null!;
    public DbSet<Text> Texts { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
    public DbSet<Video> Videos { get; set; } = null!;
    public DbSet<Hyperlink> Hyperlinks { get; set; } = null!;
    public DbSet<Participation> Participations { get; set; } = null!;
    public DbSet<Answer> Answers { get; set; } = null!;
    public DbSet<Choice> Choices { get; set; } = null!;
    public DbSet<Selection> Selections { get; set; } = null!;

    public DbSet<Respondent> Respondents { get; set; } = null!;
    public DbSet<SharedPlatform> SharedPlatforms { get; set; } = null!;
    public DbSet<ProjectOrganizer> ProjectOrganizers { get; set; } = null!;
    public DbSet<InformationBase> Information { get; set; } = null!;
    public DbSet<QuestionBase> Questions { get; set; } = null!;
    public DbSet<Feed> Feeds { get; set; } = null!;
    public DbSet<Idea> Ideas { get; set; } = null!;
    public DbSet<Like> Likes { get; set; } = null!;
    public DbSet<Reaction> Reactions { get; set; } = null!;
    public DbSet<LongValue> LongValues { get; set; } = null!;
    
    public CodeForgeDbContext(DbContextOptions<CodeForgeDbContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            switch (environment)
            {
                case "Development":
                    optionsBuilder.UseSqlite("Data Source=../CodeForge.db");
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<ChoiceQuestionBase>().HasBaseType<QuestionBase>();
        builder.Entity<MultipleChoiceQuestion>().HasBaseType<ChoiceQuestionBase>();
        builder.Entity<SingleChoiceQuestion>().HasBaseType<ChoiceQuestionBase>();
        builder.Entity<RangeQuestion>().HasBaseType<ChoiceQuestionBase>();
        
        builder.Entity<Note>(entity => entity.Property(e => e.Textfield).IsRequired().HasMaxLength(15000));
        builder.Entity<Image>(entity => entity.Property(e => e.Base64).IsRequired().HasMaxLength(10485759));
        builder.Entity<Text>(entity => entity.Property(e => e.InformationText).IsRequired().HasMaxLength(600));
        builder.Entity<Video>(entity => entity.Property(e => e.FilePath).IsRequired().HasMaxLength(200));
        builder.Entity<OpenQuestion>(entity => entity.Property(e => e.TextField).IsRequired().HasMaxLength(600));
        builder.Entity<QuestionBase>(entity => entity.Property(e => e.Question).IsRequired().HasMaxLength(600));
        builder.Entity<Choice>(entity => entity.Property(e => e.Text).IsRequired().HasMaxLength(150));
        builder.Entity<ThemeBase>(entity => entity.Property(e => e.Subject).IsRequired().HasMaxLength(600));
        builder.Entity<SharedPlatform>(entity => entity.Property(e => e.Logo).IsRequired().HasMaxLength(10485759));
        builder.Entity<SharedPlatform>(entity => entity.Property(e => e.PrivacyLink).IsRequired().HasMaxLength(150));
        builder.Entity<SharedPlatform>(entity => entity.Property(e => e.OrganisationLink).IsRequired().HasMaxLength(150));
        builder.Entity<SharedPlatform>(entity => entity.Property(e => e.OrganisationName).IsRequired().HasMaxLength(150));
        builder.Entity<Project>(entity => entity.Property(e => e.Title).IsRequired().HasMaxLength(50));
        builder.Entity<Project>(entity => entity.Property(e => e.Description).IsRequired().HasMaxLength(600));
        builder.Entity<Project>(entity => entity.Property(e => e.Image).HasMaxLength(10485759));
        builder.Entity<OpenAnswer>(entity => entity.Property(e => e.Answer).IsRequired().HasMaxLength(300));
        builder.Entity<Hyperlink>(entity => entity.Property(e => e.URL).IsRequired().HasMaxLength(600));
        builder.Entity<Respondent>(entity => entity.Property(e => e.Email).IsRequired().HasMaxLength(320));
        builder.Entity<Idea>(entity => entity.Property(e => e.Text).IsRequired().HasMaxLength(600));
        builder.Entity<Reaction>(entity => entity.Property(e => e.Text).IsRequired().HasMaxLength(300));
        
        builder.Entity<ChoiceAnswer>()
            .HasMany(a => a.Answers)
            .WithOne(s => s.ChoiceAnswer)
            .HasForeignKey("FK_Selection_AnswerId");

        builder.Entity<Choice>()
            .HasMany(c => c.Selections)
            .WithOne(s => s.Choice)
            .HasForeignKey("FK_Selection_ChoiceId");
        
        builder.Entity<ProjectOrganizer>()
            .HasOne(organizer => organizer.Project)
            .WithMany(project => project.Organizers)
            .HasForeignKey("FK_ProjectOrganizer_ProjectId");
        
        builder.Entity<ProjectOrganizer>()
            .HasOne(organizer => organizer.Facilitator)
            .WithMany(facilitator => facilitator.ManagedProjects)
            .HasForeignKey("FK_ProjectOrganizer_FacilitatorId");
        
        builder.Entity<Like>()
            .HasOne(like => like.Idea)
            .WithMany(idea => idea.Likes)
            .HasForeignKey("FK_Like_IdeaId");
        
        builder.Entity<Like>()
            .HasOne(like => like.WebAppUser)
            .WithMany(user => user.Likes)
            .HasForeignKey("FK_Like_WebAppUserId");
            
        builder.Entity<Project>()
            .HasOne(project => project.Feed)
            .WithOne(feed => feed.Project)
            .HasForeignKey<Feed>("FK_Feed_ProjectId").IsRequired();
        
        builder.Entity<Selection>().HasKey("FK_Selection_AnswerId", "FK_Selection_ChoiceId");
        builder.Entity<ProjectOrganizer>().HasKey("FK_ProjectOrganizer_ProjectId", "FK_ProjectOrganizer_FacilitatorId");
        builder.Entity<Like>().HasKey("FK_Like_IdeaId", "FK_Like_WebAppUserId");
    }

    public bool CreateDatabase(bool dropDatabase)
    {
        if (dropDatabase) Database.EnsureDeleted();
        return Database.EnsureCreated();
    }

    public bool IsEmpty()
    {
        Database.EnsureCreated();
        return !Users.Any() && !SharedPlatforms.Any();
    }

    
}