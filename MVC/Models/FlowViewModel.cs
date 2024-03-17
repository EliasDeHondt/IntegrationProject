using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;

namespace MVC.Models;

public class FlowViewModel
{
    [Key]
    public long Id { get; set; }
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FlowType FlowType { get; set; }
    [Required]
    public IEnumerable<StepBase> Steps { get; set; }
    [Required]
    public IEnumerable<Participation> Participations { get; set; }
}