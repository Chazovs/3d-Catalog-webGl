using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public class Client
{
    public IEnumerator UploadCatalog()
    {
        WWWForm form = new WWWForm();
        form.AddField("myField", "myData");
        Debug.Log("z1");
        form.AddField("sessid", Main.bxSessId);
        Debug.Log("z2");
        form.AddField("SITE_ID", "s1");

        UnityWebRequest www = UnityWebRequest.Post("/bitrix/services/main/ajax.php?action=chazov:unimarket.api.catalogcontroller.getcatalog", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Success " + www.downloadHandler.text);
            byte[] results = www.downloadHandler.data;
            Debug.Log("Second Success");
        }
    }
}
