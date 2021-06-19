using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

internal class TestClient
{
    public IEnumerator UploadCatalog()
    {
        WWWForm form = new WWWForm();
        form.AddField("myField", "myData");
        form.AddField("sessid", "e74ecfe9f8c86e3d8227db9fa09c6649");
        form.AddField("SITE_ID", "s1");

        UnityWebRequest www = UnityWebRequest.Post("http://unimarket.local/bitrix/services/main/ajax.php?action=chazov:unimarket.api.catalogcontroller.getcatalog", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;
            /*string responseText = "{" +
                "\"status\":\"success\"," +
                "\"data\":" +
                "{" +
                "\"catalogs\":{" +
                "\"2\":{" +
                "\"categories\":{" +
                "\"7\":{" +
                "\"name\":\"\\u041f\\u043b\\u0430\\u0442\\u044c\\u044f\"," +
                "\"id\":7," +
                "\"parentId\":null," +
                "\"picture\":\"\\/upload\\/iblock\\/607\\/60769a86fc7a85e32f0e3cb7d3640929.jpg\"," +
                "\"depthLevel\":1," +
                "\"parentSection\":null," +
                "\"items\":[]" +
                "}" +
                "}," +
                "\"code\":\"clothes\"," +
                "\"imagePath\":\"\\/upload\\/iblock\\/b1e\\/b1e30121f2fc3e9cd06458a896a399ae.gif\"}}," +
                "\"success\":true," +
                "\"errMsg\":[]}," +
                "\"errors\":[]}";*/

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
}