using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Diets;

[Authorize]
public class ChoicesModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public ChoicesModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Diet> Diets { get; set; } = default!;

    [BindProperty] public required int[] SelectedDiets { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Diets = _context.Diets.ToList();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        SelectedDiets = await _context.ApplicationUsersDiets
            .Where(ud => ud.ApplicationUserId == userId)
            .Select(ud => ud.DietId)
            .ToArrayAsync();

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return await OnGetAsync();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var existingUserDiets = await _context.ApplicationUsersDiets
            .Where(ud => ud.ApplicationUserId == userId)
            .ToListAsync();

        _context.ApplicationUsersDiets.RemoveRange(existingUserDiets);

        foreach (var dietId in SelectedDiets)
        {
            var userDiet = new ApplicationUserDiet
            {
                ApplicationUserId = userId!,
                DietId = dietId
            };
            _context.ApplicationUsersDiets.Add(userDiet);
        }

        await _context.SaveChangesAsync();

        return RedirectToPage("../Recipes/Index");
    }
}