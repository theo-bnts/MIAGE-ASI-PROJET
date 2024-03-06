using System.ComponentModel.DataAnnotations;

namespace PROJET.Model;

public class Diet
{
    [Key] public required int Id { get; set; }

    [Required] public required string Name { get; set; }

    public ICollection<Recipe>? Recipes { get; set; }

    public int RecipesCout
    {
        get
        {
            if (Recipes != null) return Recipes.Count;

            return -1;
        }
    }
}