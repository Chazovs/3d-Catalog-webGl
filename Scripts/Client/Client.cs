using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    public void UploadCatalog()
    {
        StartCoroutine(StartUploadCatalog());
    }

    public void AddItemToBasket(int itemId)
    {
        StartCoroutine(StartAddItemToBasket(itemId));
    }

    public IEnumerator StartUploadCatalog()
    {
        WWWForm form = new WWWForm();
        form.AddField("sessid", Main.bxSessId);
        form.AddField("SITE_ID", Main.siteId);

        UnityWebRequest www = UnityWebRequest.Post(
                "/bitrix/services/main/ajax.php?action=chazov:unimarket.api.catalogcontroller.getcatalog",
                form
                );

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            CatalogResponseWrapper catalogResponseWrapper
               = JsonConvert.DeserializeObject<CatalogResponseWrapper>(www.downloadHandler.text);

            MarketService marketService = new MarketService();

            if (catalogResponseWrapper.data.success == true)
            {
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
            BasketResponseWrapper basketResponseWrapper
               = JsonConvert.DeserializeObject<BasketResponseWrapper>(www.downloadHandler.text);

            ObjectService objectService = GameObject.Find("ObjectService").GetComponent<ObjectService>();

            if (basketResponseWrapper.data.success == true)
            {
                objectService.UpdateBasketInfo(basketResponseWrapper.data);
            }
        }
    }

    internal void GetImageSprite(string imagePath, GameObject itemObject)
    {
        StartCoroutine(LoadPhoto(imagePath, itemObject));
    }

    private IEnumerator LoadPhoto(string imagePath, GameObject itemObject)
    {
        string url = "http://unimarket.local" + imagePath; //TODO delete test adress
        Texture2D texture;
        texture = new Texture2D(4, 4, TextureFormat.DXT1, false);
        WWW www = new WWW(url);

        yield return www;

        www.LoadImageIntoTexture(texture);

        itemObject.transform.Find("Canvas").transform.Find("RawImage").GetComponent<RawImage>().texture = texture;

        //GetComponent<Renderer>().material.mainTexture = tex;
    }
}
