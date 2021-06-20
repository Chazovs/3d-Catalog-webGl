
public class Item : Interactive
{
    public int categoryId;
    public string? description;
    public string? imagePath;
    public int itemId;
    public string name;
    public float? price;

    public void MouseZeroDown()
    {
        Main.client.AddItemToBasket(itemId);
    }
}