using System.ComponentModel.DataAnnotations;
using Domain.ProjectLogics;

namespace MVC.Models;

public class FlowListViewModel
{
    [Required]
    [Key]
    public List<Flow> Flows { get; set; }

    public FlowListViewModel(List<Flow> flows)
    {
        Flows = flows;
    }
}