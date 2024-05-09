/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class InformationStepViewModel : StepViewModel
{
    [Required]
    [Range(0, int.MaxValue)]
    public int StepNumber { get; set; }
    [Key]
    public long Id { get; set; }
    [Required]
    public ICollection<InformationViewModel> InformationViewModel { get; set; }
}