using CS2Skins.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CS2Skins.Controllers;

public class ShopController : Controller
{
    private readonly ApplicationDbContext _context;

    public ShopController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var skins = await _context.Skins.ToListAsync();

        return View(skins);
    }

    public async Task<IActionResult> Details(int id)
    {
        var skin = await _context.Skins
            .FirstOrDefaultAsync(s => s.Id == id);

        if (skin == null)
        {
            return NotFound();
        }

        return View(skin);
    }
}