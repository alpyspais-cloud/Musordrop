namespace CS2Skins.Models;

public class Skin
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Weapon { get; set; } = "";

    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = "";

    public int Stock { get; set; }
}