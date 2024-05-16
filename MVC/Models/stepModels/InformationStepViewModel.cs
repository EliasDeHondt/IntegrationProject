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
    public ICollection<InformationViewModel> InformationViewModel { get; set; }
}