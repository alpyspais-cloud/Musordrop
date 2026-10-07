using CS2Skins.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CS2Skins.Controllers;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;

    public CartController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var cartIds = HttpContext.Session.GetString("Cart");

        if (string.IsNullOrEmpty(cartIds))
        {
            return View(new List<CS2Skins.Models.Skin>());
        }

        var ids = cartIds
            .Split(',')
            .Where(x => int.TryParse(x, out _))
            .Select(int.Parse)
            .ToList();

        var skins = await _context.Skins
            .Where(s => ids.Contains(s.Id))
            .ToListAsync();

        return View(skins);
    }

    public IActionResult Add(int id)
    {
        var cart = HttpContext.Session.GetString("Cart");

        if (string.IsNullOrEmpty(cart))
        {
            cart = id.ToString();
        }
        else
        {
            cart += "," + id;
        }

        HttpContext.Session.SetString("Cart", cart);

        return RedirectToAction("Index");
    }

    public IActionResult Remove(int id)
    {
        var cart = HttpContext.Session.GetString("Cart");

        if (!string.IsNullOrEmpty(cart))
        {
            var ids = cart
                .Split(',')
                .Where(x => x != id.ToString())
                .ToList();

            HttpContext.Session.SetString(
                "Cart",
                string.Join(",", ids)
            );
        }

        return RedirectToAction("Index");
    }
}