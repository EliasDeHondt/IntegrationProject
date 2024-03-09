using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Information;

public class Video : IInformation
{
    [Key]
    public long Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string FilePath { get; set; }

    public Video(string filePath, long id = 0)
    {
        Id = id;
        FilePath = filePath;
    }

    public Video()
    {
        Id = default;
        FilePath = string.Empty;
    }
    

    public string GetInformation()
    {
        return FilePath;
    }
}