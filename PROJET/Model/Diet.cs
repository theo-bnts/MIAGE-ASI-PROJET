using System.ComponentModel.DataAnnotations;

namespace PROJET.Model;

public class Diet
{
    [Key] public int Id { get; set; }

    [Required] public required string Name { get; set; }

    public IEnumerable<RecipeDiet>? RecipesDiet { get; set; }

    public int RecipesCout
    {
        get
        {
            if (RecipesDiet != null) return RecipesDiet.Count();

            return -1;
        }
    }
}