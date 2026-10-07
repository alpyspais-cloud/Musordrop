using CS2Skins.Data;
using Microsoft.EntityFrameworkCore;
using CS2Skins.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var skins = db.Skins.ToList();

    foreach (var skin in skins)
    {
        if (skin.ImageUrl.StartsWith("wwwroot/"))
        {
            skin.ImageUrl = skin.ImageUrl.Replace("wwwroot/", "/");
        }

        if (skin.Name.Contains("AK-47", StringComparison.OrdinalIgnoreCase))
        {
            skin.ImageUrl = "/images/ak47-redline.jpg";
        }
        else if (skin.Name.Contains("AWP", StringComparison.OrdinalIgnoreCase))
        {
            skin.ImageUrl = "/images/monster.jpg";
        }
        else if (skin.Name.Contains("M4A1", StringComparison.OrdinalIgnoreCase))
        {
            skin.ImageUrl = "/images/M4A1-S.jpg";
        }
    }

    if (!db.Skins.Any())
    {
        db.Skins.AddRange(
            new Skin
            {
                Name = "AK-47 | Redline",
                Weapon = "AK-47",
                Price = 25.00m,
                ImageUrl = "/images/ak47-redline.jpg",
                Stock = 10
            },

            new Skin
            {
                Name = "AWP | Asiimov",
                Weapon = "AWP",
                Price = 80.00m,
                ImageUrl = "/images/monster.jpg",
                Stock = 5
            },

            new Skin
            {
                Name = "M4A1-S | Printstream",
                Weapon = "M4A1-S",
                Price = 120.00m,
                ImageUrl = "/images/M4A1-S.jpg",
                Stock = 3
            }
        );
    }

    db.SaveChanges();
}

app.Run();

