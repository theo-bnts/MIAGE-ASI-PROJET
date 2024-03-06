using System.ComponentModel.DataAnnotations;

namespace PROJET.Model;

public class Recipe
{
    [Key] public required int Id { get; set; }

    [Required] public required string Name { get; set; }

    [Required]
    [DataType(DataType.MultilineText)]
    public required string Description { get; set; }

    public ICollection<Diet>? Diets { get; set; }
}