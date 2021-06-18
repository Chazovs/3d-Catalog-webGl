using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Client
{
    public IEnumerator UploadCatalog()
    {
        WWWForm form = new WWWForm();
        form.AddField("myField", "myData");
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
            CatalogResponse catalogResponse 
                = JsonConvert.DeserializeObject<CatalogResponse>(www.downloadHandler.text);

            MarketService marketService = new MarketService();

            if(catalogResponse.success == true)
            {
                Debug.Log("success");
                marketService.CreateMarket(catalogResponse.catalogs);
            }
        }
    }
}
