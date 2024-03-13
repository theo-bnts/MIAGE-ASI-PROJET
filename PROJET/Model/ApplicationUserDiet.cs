using System.ComponentModel.DataAnnotations;

namespace PROJET.Model;

public class ApplicationUserDiet
{
    [Key] public int Id { get; set; }

    [Required] public required string ApplicationUserId { get; set; } = default!;
    public ApplicationUser? ApplicationUser { get; set; }

    [Required] public required int DietId { get; set; }
    public Diet? Diet { get; set; }
}