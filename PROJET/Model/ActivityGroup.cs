using System.ComponentModel.DataAnnotations;

namespace PROJET.Model;

public class ActivityGroup
{
    [Key] public int Id { get; set; }

    [Required] public required string Name { get; set; }

    public IEnumerable<Activity>? Activities { get; set; }
}