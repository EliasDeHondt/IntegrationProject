using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class QrCodeModel
{
    [Display]
    public string Text { get; set; }
}