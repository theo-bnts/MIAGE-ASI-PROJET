using System.ComponentModel.DataAnnotations;

namespace PROJET.Model;

public class ApplicationUserActivity
{
    [Key] public int Id { get; set; }

    [Required] public required string ApplicationUserId { get; set; } = default!;
    public ApplicationUser? ApplicationUser { get; set; }

    [Required] public required int ActivityGroupId { get; set; }
    public ActivityGroup? ActivityGroup { get; set; }
}