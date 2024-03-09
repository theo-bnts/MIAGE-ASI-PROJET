using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROJET.Model;

namespace PROJET.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Diet> Diet { get; set; } = default!;

    public DbSet<Recipe> Recipe { get; set; } = default!;

    public DbSet<RecipeDiet> RecipeDiet { get; set; } = default!;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<RecipeDiet>()
            .HasKey(rd => new { rd.RecipeId, rd.DietId });

        modelBuilder.Entity<RecipeDiet>()
            .HasOne(rd => rd.Recipe)
            .WithMany(r => r.RecipeDiets)
            .HasForeignKey(rd => rd.RecipeId);

        modelBuilder.Entity<RecipeDiet>()
            .HasOne(rd => rd.Diet)
            .WithMany(d => d.RecipesDiet)
            .HasForeignKey(rd => rd.DietId);
    }
}