namespace MVC.Models;

public class FileUpload
{
    public string Name { get; set; }
    public string Type { get; set; }
    public byte[] File { get; set; }
}