using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROJET.Model;

namespace PROJET.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; } = default!;

    public DbSet<Diet> Diets { get; set; } = default!;

    public DbSet<Recipe> Recipes { get; set; } = default!;

    public DbSet<RecipeDiet> RecipesDiets { get; set; } = default!;

    public DbSet<ApplicationUserDiet> ApplicationUsersDiets { get; set; } = default!;

    public DbSet<Activity> Activies { get; set; } = default!;

    public DbSet<ActivityGroup> ActivityGroups { get; set; } = default!;

    public DbSet<ApplicationUserActivity> ApplicationUserActivities { get; set; } = default!;
    
    public DbSet<PROJET.Model.SocioProfessionalCategory> SocioProfessionalCategory { get; set; } = default!;

    public DbSet<PROJET.Model.Mood> Mood { get; set; } = default!;

    public DbSet<PROJET.Model.ApplicationUserSocioProfessionalCategory> ApplicationUserSocioProfessionalCategory { get; set; } = default!;

    public DbSet<PROJET.Model.RefMood> RefMood { get; set; } = default!;

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
