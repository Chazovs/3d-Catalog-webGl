using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static string bxSessId = "";
    internal static string siteId = "";
    public static TestClient client;
    public static string confirmOrderUrl = "/personal/order/make/";

    public void SetConfirmOrderUrl(string confirmOrderUrl)
    {
        confirmOrderUrl = confirmOrderUrl;
    }

    public void SetSiteId(string siteId)
    {
        siteId = siteId;
    }

    public void SetBxSessId(string sessId)
    {
        bxSessId = sessId;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*Client client = GameObject.Find("Client").GetComponent<Client>();*/
        client = GameObject.Find("TestClient").GetComponent<TestClient>();
        client.UploadCatalog();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
