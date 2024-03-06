using System.ComponentModel.DataAnnotations;

namespace PROJET.Model;

public class RecipeDiet
{
    [Key] public int Id { get; set; }

    [Required] public required int RecipeId { get; set; }
    public Recipe? Recipe { get; set; }

    [Required] public required int DietId { get; set; }
    public Diet? Diet { get; set; }
}