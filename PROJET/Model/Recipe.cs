using System.ComponentModel.DataAnnotations;

namespace PROJET.Model;

public class Recipe
{
    [Key] public required int Id { get; set; }

    [Required] public required string Name { get; set; }

    [Required]
    [DataType(DataType.MultilineText)]
    public required string Description { get; set; }

    public IEnumerable<RecipeDiet>? RecipeDiets { get; set; }
    
    public required string ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
}