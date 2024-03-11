/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;

namespace Domain.FacilitatorFunctionality;

public class Note
{
    [Key]
    public long Id { get; set; }
    [Required]
    [MaxLength(15000)]
    public String Textfield { get; set; }

    public Note()
    {
        Id = default;
        Textfield = string.Empty;
    }
}