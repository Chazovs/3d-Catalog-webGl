using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour
{
    public void UploadCatalog()
    {
        StartCoroutine(StartUploadCatalog());
    }

    public new void AddItemToBasket(int itemId)
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
            Debug.Log("товар добавлен");
        }
    }
}
