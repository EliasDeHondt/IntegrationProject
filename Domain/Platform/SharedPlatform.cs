/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.ProjectLogics;

namespace Domain.Platform;

public class SharedPlatform
{
    [Key]
    public long Id { get; set; }
    [Required]
    [MaxLength(150)]
    public string Logo { get; set; }
    [Required]
    [MaxLength(150)]
    public string PrivacyLink { get; set; }
    [Required]
    [MaxLength(150)]
    public string OrganisationLink { get; set; }
    [Required]
    [MaxLength(150)]
    public string OrganisationName { get; set; }
    [Required]
    public ICollection<Project> Projects { get; set; }

    public SharedPlatform(string logo, string privacyLink, string organisationLink, string organisationName, ICollection<Project> projects, long id = 0): this(organisationName, id)
    {
        Logo = logo;
        PrivacyLink = privacyLink;
        OrganisationLink = organisationLink;
        Projects = projects;
    }

    public SharedPlatform(string logo, string privacyLink, string organisationLink, string organisationName, long id = 0): this(organisationName, id)
    {
        Logo = logo;
        PrivacyLink = privacyLink;
        OrganisationLink = organisationLink;
    }
    
    public SharedPlatform(string organisationName, long id = 0)
    {
        OrganisationName = organisationName;
        Id = id;
        Logo = string.Empty;
        PrivacyLink = string.Empty;
        OrganisationLink = string.Empty;
        Projects = new List<Project>();
    }

    public SharedPlatform()
    {
        Id = default;
        OrganisationName = string.Empty;
        Logo = string.Empty;
        PrivacyLink = string.Empty;
        OrganisationLink = string.Empty;
        Projects = new List<Project>();
    }
    
}