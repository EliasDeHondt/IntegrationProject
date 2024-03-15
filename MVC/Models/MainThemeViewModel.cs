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
    [MaxLength(50)]
    public string Subject { get; set; }
    [Required]
    public IEnumerable<Flow> Flows { get; set; }
    [Required]
    public IEnumerable<SubTheme> Themes { get; set; }
}