using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Information;

public class Image : IInformation
{
    [Key]
    public long Id { get; set; }
    [Required]
    [MaxLength(65000)]
    public string Base64 { get; set; }

    public Image(string pathOrBase64, long id = 0)
    {
        Id = id;
        var buffer = new Span<byte>(new byte[pathOrBase64.Length]);
        Base64 = Convert.TryFromBase64String(pathOrBase64, buffer, out _) ? pathOrBase64 : GenerateBase64(pathOrBase64);
    }

    public Image()
    {
        Id = default;
        Base64 = string.Empty;
    }

    public string GetInformation()
    {
        return Base64;
    }
    
    private string GenerateBase64(string path)
    {
        using MemoryStream ms = new();
        byte[] imageBytes = File.ReadAllBytes(path);
        return Convert.ToBase64String(imageBytes);
    }
    
}