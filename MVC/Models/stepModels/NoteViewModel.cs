using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class NoteViewModel
{
    public long Id { get; set; }
    [MaxLength(15000)]
    public string Textfield { get; set; }
}