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
    public long Id { get; set; }
    [MaxLength(15000)]
    public string Textfield { get; set; }
    
    public Note(string textfield, long id = 0)
    {
        Id = id;
        Textfield = textfield;
    }

    public Note()
    {
        Id = default;
        Textfield = string.Empty;
    }
}