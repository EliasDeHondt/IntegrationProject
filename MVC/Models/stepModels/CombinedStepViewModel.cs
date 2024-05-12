/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class CombinedStepViewModel : StepViewModel
{
    [Required]
    public InformationViewModel InformationViewModel { get; set; }
}