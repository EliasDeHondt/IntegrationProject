/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.ProjectLogics;

namespace MVC.Models;

public class MainThemeViewModel
{
    [Key]
    public long Id { get; set; }

    [MaxLength(50)] public string Subject { get; set; } = string.Empty;

    [Required] public IEnumerable<Flow> Flows { get; set; } = new List<Flow>();
    [Required] public IEnumerable<SubTheme> Themes { get; set; } = new List<SubTheme>();
}