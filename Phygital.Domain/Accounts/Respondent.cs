/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.ProjectLogics;
using Domain.WebApp;
using Microsoft.AspNetCore.Identity;

namespace Domain.Accounts;

public class Respondent 
{
    public long RespondentId { get; set; }
    [MaxLength(320)]
    public string Email { get; set; }
    public Participation Participation { get; set; }
    public Respondent(string email,Participation participation) 
    {
        Email = email;
        Participation = participation;
    }
    public Respondent()
    {
        Email = "none";
        Participation = new Participation();
    }
    
}