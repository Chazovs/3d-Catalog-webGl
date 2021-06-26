
using UnityEngine;
using UnityEngine.UI;

public class Item : Interactive
{
    public int? categoryId;
    public string? description;
    public string? imagePath;
    public int itemId;
    public string name;
    public float? price;

    public void KeyRActio()
    {
        Main.client.DeleteItemFromBasket(itemId);
    }

    public void MouseOneDown()
    {
        Transform transform = GameObject.Find("LeftCanva").transform;

        transform.Find("Title").GetComponent<Text>().text = name;
        transform.Find("Description").GetComponent<Text>().text = description;
        transform.Find("PriceTitle").GetComponent<Text>().text = "Цена";
        transform.Find("Summ").GetComponent<Text>().text = price.ToString() + " руб.";
    }

    public void MouseZeroDown()
    {
        Main.client.AddItemToBasket(itemId);
    }
}