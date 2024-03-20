/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class QuestionStepViewModel
{
    
    [Required]
    [Range(0, int.MaxValue)]
    public int StepNumber { get; set; }
    [Key]
    public long Id { get; set; }

    public QuestionViewModel QuestionViewModel { get; set; }
}