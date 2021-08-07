using System.Net;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static string bxSessId = "";
    public static string siteId = "";
    public static string serverName = "";
    public static Client client;
    public static string confirmOrderUrl = "/personal/order/make/";

    public void SetServerName(string serverName)
    {
        serverName = serverName;
    }

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
        Debug.Log(Dns.GetHostName()); //TODO убрать потом
        /*Client client = GameObject.Find("Client").GetComponent<Client>();*/
        client = GameObject.Find("Client").GetComponent<Client>();
        client.UploadCatalog();
        client.GetBasket();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
