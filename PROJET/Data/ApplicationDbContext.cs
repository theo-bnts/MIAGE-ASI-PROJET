using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROJET.Model;

namespace PROJET.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Diet> Diet { get; set; } = default!;

    public DbSet<Recipe> Recipe { get; set; } = default!;

    public DbSet<RecipeDiet> RecipeDiet { get; set; } = default!;
}