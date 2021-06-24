using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    public new void UploadCatalog()
    {
        StartCoroutine(StartUploadCatalog());
    }

    public new void AddItemToBasket(int itemId)
    {
        StartCoroutine(StartAddItemToBasket(itemId));
    }

    internal void GetImageSprite(string imagePath, GameObject itemObject)
    {
        StartCoroutine(LoadPhoto(imagePath, itemObject));
    }

    public new IEnumerator StartUploadCatalog()
    {
        WWWForm form = new WWWForm();
        form.AddField("sessid", Main.bxSessId);
        form.AddField("SITE_ID", Main.siteId);

        UnityWebRequest www = UnityWebRequest.Post("/bitrix/services/main/ajax.php?action=chazov:unimarket.api.catalogcontroller.getcatalog", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;

            CatalogResponseWrapper catalogResponseWrapper
                = JsonConvert.DeserializeObject<CatalogResponseWrapper>(responseText);

            MarketService marketService = new MarketService();

            if (catalogResponseWrapper.data.success == true)
            {
                Debug.Log("success");
                marketService.CreateMarket(catalogResponseWrapper.data.catalogs);
            }
        }
    }

    public new IEnumerator StartAddItemToBasket(int itemId)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemId", itemId);
        form.AddField("sessid", Main.bxSessId);
        form.AddField("SITE_ID", Main.siteId);

        UnityWebRequest www = UnityWebRequest.Post(
               "/bitrix/services/main/ajax.php?action=chazov:unimarket.api.basketcontroller.addToBasket",
               form
               );

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;

            BasketResponseWrapper basketResponseWrapper
                = JsonConvert.DeserializeObject<BasketResponseWrapper>(responseText);

            Transform transform = GameObject.Find("LeftCanva").transform;
            string resultMsg = "";

            if (basketResponseWrapper.data.basketItems != null)
            {
                foreach (BasketItem basketItem in basketResponseWrapper.data.basketItems)
                {
                    //разрешенные теги: http://digitalnativestudios.com/textmeshpro/docs/rich-text/
                    resultMsg += "- " + basketItem.name + " | " + basketItem.quantity + "\n";
                }
            }

            transform.Find("Title").GetComponent<Text>().text = "Корзина";
            transform.Find("Description").GetComponent<Text>().text = resultMsg;
            transform.Find("PriceTitle").GetComponent<Text>().text = "Итого";
            transform.Find("Summ").GetComponent<Text>().text = basketResponseWrapper.data.totalPrice.ToString() + " руб.";
        }
    }

    private IEnumerator LoadPhoto(string imagePath, GameObject itemObject)
    {
        string url = imagePath; //TODO delete test adress
        Texture2D texture;
        texture = new Texture2D(4, 4, TextureFormat.DXT1, false);
        WWW www = new WWW(url);

        yield return www;

        www.LoadImageIntoTexture(texture);

        itemObject.transform.Find("Canvas").transform.Find("RawImage").GetComponent<RawImage>().texture = texture;
    }
}