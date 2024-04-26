/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Information;

public class Image : InformationBase
{
    [MaxLength(65000)]
    public string Base64 { get; set; }

    public Image(string pathOrBase64, long id = 0) : base(id)
    {
        var buffer = new Span<byte>(new byte[pathOrBase64.Length]);
        Base64 = Convert.TryFromBase64String(pathOrBase64, buffer, out _) ? pathOrBase64 : GenerateBase64(pathOrBase64);
    }

    public Image()
    {
        Base64 = string.Empty;
    }

    public override string GetInformation()
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