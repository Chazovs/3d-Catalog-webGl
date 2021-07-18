
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Item : Interactive
{
    public int? categoryId;
    public string? description;
    public string? imagePath;
    public int itemId;
    public string name;
    public float? price;
    public string? modelPath;

    public void KeyRActio()
    {
        Main.client.DeleteItemFromBasket(itemId);
    }

    public void KeyXAction()
    {
        if (null == modelPath) {
            Debug.Log("Путь к модели пустой");
            return;
        }

        ModelStore.reset();
        ModelStore.setModel(modelPath);
        SceneManager.LoadScene("Object");
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